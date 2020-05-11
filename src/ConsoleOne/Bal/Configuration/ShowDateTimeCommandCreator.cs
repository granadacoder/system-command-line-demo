using System;
using System.CommandLine;
using System.CommandLine.Invocation;

using Microsoft.Extensions.Logging;
using MyCompany.MyExamples.SystemCommandLineOne.ConsoleOne.Bal.Configuration.Interfaces;
using MyCompany.MyExamples.SystemCommandLineOne.ConsoleOne.Domain;

namespace MyCompany.MyExamples.SystemCommandLineOne.ConsoleOne.Bal.Configuration
{
    public class ShowDateTimeCommandCreator : ICommandCreator
    {
        public const int ExceptionExitCode = 40002;

        public const string ErrorMessageILoggerFactoryIsNull = "ILoggerFactory is null";

        private readonly ILogger<ShowDateTimeCommandCreator> logger;

        public ShowDateTimeCommandCreator(ILoggerFactory loggerFactory)
        {
            if (null == loggerFactory)
            {
                throw new ArgumentNullException(ErrorMessageILoggerFactoryIsNull, (Exception)null);
            }

            this.logger = loggerFactory.CreateLogger<ShowDateTimeCommandCreator>();
        }

        public Command CreateCommand()
        {
            Command returnItem = new Command("showdatetime")
            {
                new Option<bool>("--includedate"),
                new Option<string>("--dateformat"),
                new Option<bool>("--includetime"),
            };

            returnItem.Handler = CommandHandler.Create<DateTimeReportArgs>((DateTimeReportArgs args) =>
            {
                try
                {
                    Console.WriteLine("DateTimeReportArgs.IncludeDate is '{0}'", args.IncludeDate);
                    Console.WriteLine("DateTimeReportArgs.DateFormat is '{0}'", args.DateFormat);
                    Console.WriteLine("DateTimeReportArgs.IncludeTime is '{0}'", args.IncludeTime);

                    if (args.IncludeDate)
                    {
                        Console.WriteLine("Functionality (IncludeDate, DateTime.Now with format) -> '{0}'", DateTime.Now.ToString(args.DateFormat));
                    }

                    if (args.IncludeTime)
                    {
                        Console.WriteLine("Functionality (IncludeTime, DateTime.Now.ToLongTimeString) -> '{0}'", DateTime.Now.ToLongTimeString());
                    }

                    return 0;
                }
                catch (Exception ex)
                {
                    this.logger.Log(LogLevel.Error, ex, ex.Message);
                    return ExceptionExitCode;
                }
            });

            return returnItem;
        }
    }
}
