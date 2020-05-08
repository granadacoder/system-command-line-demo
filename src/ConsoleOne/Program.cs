using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using MyCompany.MyExamples.SystemCommandLineOne.ConsoleOne.Bal.Configuration;
using MyCompany.MyExamples.SystemCommandLineOne.ConsoleOne.Bal.Configuration.Interfaces;

#if (NETCOREAPP2_1 || NETSTANDARD2_0)

#endif

#if (NETCOREAPP3_1 || NETSTANDARD2_1)

#endif

using Serilog;

namespace MyCompany.MyExamples.SystemCommandLineOne.ConsoleOne
{
    public static class Program
    {
        public static async Task<int> Main(string[] args)
        {
            /* easy concrete logger that uses a file for demos */
            Serilog.ILogger lgr = new Serilog.LoggerConfiguration()
                .WriteTo.File("MyCompany.MyExamples.SystemCommandLineOne.ConsoleOne.log.txt", rollingInterval: Serilog.RollingInterval.Day)
                .CreateLogger();

            try
            {
                ShowInputArgs(args);

                /* look at the Project-Properties/Debug(Tab) for this environment variable */
                string environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                Console.WriteLine(string.Format("ASPNETCORE_ENVIRONMENT='{0}'", environmentName));
                Console.WriteLine(string.Empty);

                IConfigurationBuilder builder = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json")
                        .AddJsonFile($"appsettings.{environmentName}.json", true, true)
                        .AddEnvironmentVariables();

                IConfigurationRoot configuration = builder.Build();

                IServiceProvider servProv = BuildDi(configuration, lgr);

                await RunJustRootCommandDemo(servProv, args);
                await RunParserDemo(servProv, args);
            }
            catch (Exception ex)
            {
                string flattenMsg = GenerateFullFlatMessage(ex, true);
                Console.WriteLine(flattenMsg);
            }

            Console.WriteLine("Press ENTER to exit");
            Console.ReadLine();

            return 0;
        }

        private static async Task<int> RunJustRootCommandDemo(IServiceProvider servProv, string[] args)
        {
            Console.WriteLine(string.Concat(Enumerable.Repeat(System.Environment.NewLine, 10)));

            ICommandCreator rcc = servProv.GetService<ICommandCreator>();

            /* note , the below is a RootCommand, as the forms demo */
            RootCommand cmd = rcc.CreateRootCommand();

            Console.WriteLine("cmd.GetType='{0}'", cmd.GetType().Name);

            /* Note, I tried .Invoke and the below, both have the same result */
            int returnValue = await cmd.InvokeAsync(args);
            Console.WriteLine(string.Format("RootCommand.InvokeAsync='{0}'", returnValue));

            return returnValue;
        }

        private static async Task<int> RunParserDemo(IServiceProvider servProv, string[] args)
        {
            Console.WriteLine(string.Concat(Enumerable.Repeat(System.Environment.NewLine, 10)));

            IRootParserBuilder rootParserBuilder = servProv.GetService<IRootParserBuilder>();
            Parser prsr = rootParserBuilder.CreateParser();
            int returnValue = await prsr.InvokeAsync(args);
            Console.WriteLine(string.Format("Parser.InvokeAsync='{0}'", returnValue));

            return returnValue;
        }

        private static void ShowInputArgs(string[] args)
        {
            Console.WriteLine($"parameter count = {args.Length}");

            for (int i = 0; i < args.Length; i++)
            {
                Console.WriteLine($"Arg[{i}] = [{args[i]}]");
            }
        }

        private static string GenerateFullFlatMessage(Exception ex)
        {
            return GenerateFullFlatMessage(ex, false);
        }

        private static IServiceProvider BuildDi(IConfiguration configuration, Serilog.ILogger lgr)
        {
            ////setup our DI
            IServiceCollection servColl = new ServiceCollection()
                .AddSingleton(lgr)
                .AddLogging();

            /* need trace to see Oracle.EF statements */
            servColl.AddLogging(loggingBuilder => loggingBuilder.AddConsole().SetMinimumLevel(LogLevel.Trace));

            servColl.AddLogging(blder =>
            {
                blder.SetMinimumLevel(LogLevel.Trace); /* need trace to see Oracle.EF statements */
                blder.AddSerilog(logger: lgr, dispose: true);
            });

            servColl.AddSingleton<ICommandCreator, MyTypeCommandCreator>();
            servColl.AddSingleton<IRootParserBuilder, RootParserBuilder>();

            ServiceProvider servProv = servColl.BuildServiceProvider();

            return servProv;
        }

        private static string GenerateFullFlatMessage(Exception ex, bool showStackTrace)
        {
            string returnValue;

            StringBuilder sb = new StringBuilder();
            Exception nestedEx = ex;

            while (nestedEx != null)
            {
                if (!string.IsNullOrEmpty(nestedEx.Message))
                {
                    sb.Append(nestedEx.Message + System.Environment.NewLine);
                }

                if (showStackTrace && !string.IsNullOrEmpty(nestedEx.StackTrace))
                {
                    sb.Append(nestedEx.StackTrace + System.Environment.NewLine);
                }

                if (ex is AggregateException)
                {
                    AggregateException ae = ex as AggregateException;

                    foreach (Exception aeflatEx in ae.Flatten().InnerExceptions)
                    {
                        if (!string.IsNullOrEmpty(aeflatEx.Message))
                        {
                            sb.Append(aeflatEx.Message + System.Environment.NewLine);
                        }

                        if (showStackTrace && !string.IsNullOrEmpty(aeflatEx.StackTrace))
                        {
                            sb.Append(aeflatEx.StackTrace + System.Environment.NewLine);
                        }
                    }
                }

                nestedEx = nestedEx.InnerException;
            }

            returnValue = sb.ToString();

            return returnValue;
        }
    }
}