namespace MyCompany.MyExamples.SystemCommandLineOne.ConsoleOne.Bal.Configuration
{
    using System;
    using System.CommandLine;
    using System.CommandLine.Invocation;

    using Microsoft.Extensions.Logging;

    using MyCompany.MyExamples.SystemCommandLineOne.ConsoleOne.Bal.Configuration.Interfaces;
    using MyCompany.MyExamples.SystemCommandLineOne.ConsoleOne.Domain;

    public class MyTypeCommandCreator : ICommandCreator
    {
        public const int ExceptionExitCode = 40001;

        public const string ErrorMessageILoggerFactoryIsNull = "ILoggerFactory is null";

        private readonly ILogger<MyTypeCommandCreator> logger;

        public MyTypeCommandCreator(ILoggerFactory loggerFactory)
        {
            if (null == loggerFactory)
            {
                throw new ArgumentNullException(ErrorMessageILoggerFactoryIsNull, (Exception)null);
            }

            this.logger = loggerFactory.CreateLogger<MyTypeCommandCreator>();
        }

        public Command CreateCommand()
        {
            Command mytypeoneCommand = new Command("mytypeone", "mytypeone_description")
            {
                new Option<string>("-a"),
                new Option<string>("-b"),
                new Option<string>("-c"),
                new Option<string>("-d"),
                new Option<string>("-e"),
                new Option<string>("-f"),
                new Option<string>("-g"),
                new Option<string>("-h"),
                new Option<string>("-i"),
            };

            mytypeoneCommand.Handler = CommandHandler.Create<MyType>((MyType myTypeInstance) =>
            {
                try
                {
                    Console.WriteLine("the value of A is '{0}'", myTypeInstance.A);
                    Console.WriteLine("the value of B is '{0}'", myTypeInstance.B);
                    Console.WriteLine("the value of C is '{0}'", myTypeInstance.C);
                    Console.WriteLine("the value of D is '{0}'", myTypeInstance.D);
                    Console.WriteLine("the value of E is '{0}'", myTypeInstance.E);
                    Console.WriteLine("the value of F is '{0}'", myTypeInstance.F);
                    Console.WriteLine("the value of G is '{0}'", myTypeInstance.G);
                    Console.WriteLine("the value of H is '{0}'", myTypeInstance.H);
                    Console.WriteLine("the value of I is '{0}'", myTypeInstance.I);

                    return 0;
                }
                catch (Exception ex)
                {
                    this.logger.Log(LogLevel.Error, ex, ex.Message);
                    return ExceptionExitCode;
                }
            });

            return mytypeoneCommand;
        }
    }
}
