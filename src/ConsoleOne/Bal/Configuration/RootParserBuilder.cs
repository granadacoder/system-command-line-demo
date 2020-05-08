namespace MyCompany.MyExamples.SystemCommandLineOne.ConsoleOne.Bal.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.CommandLine;
    using System.CommandLine.Builder;
    using Microsoft.Extensions.Logging;
    using MyCompany.MyExamples.SystemCommandLineOne.ConsoleOne.Bal.Configuration.Interfaces;

    public class RootParserBuilder : IRootParserBuilder
    {
        public const string ErrorMessageILoggerFactoryIsNull = "ILoggerFactory is null";
        public const string ErrorMessageICommandCreatorIsNull = "ICommandCreator is null";

        public const string LogMessageAddingICommandCreator = "Adding ICommandCreator. (Command Description=\"{0}\")";

        private readonly ILogger<RootParserBuilder> logger;
        private readonly IEnumerable<ICommandCreator> commandCreators;

        public RootParserBuilder(ILoggerFactory loggerFactory, IEnumerable<ICommandCreator> commandCreators)
        {
            if (null == loggerFactory)
            {
                throw new ArgumentNullException(ErrorMessageILoggerFactoryIsNull, (Exception)null);
            }

            this.logger = loggerFactory.CreateLogger<RootParserBuilder>();

            this.commandCreators = commandCreators ?? throw new ArgumentNullException(ErrorMessageICommandCreatorIsNull, (Exception)null);
        }

        public Parser CreateParser()
        {
            CommandLineBuilder clb = new CommandLineBuilder();

            foreach (ICommandCreator icc in this.commandCreators)
            {
                RootCommand cmd = icc.CreateRootCommand();
                this.logger.LogInformation(string.Format(LogMessageAddingICommandCreator, cmd.Description));
                clb.AddCommand(cmd);
            }

            Parser prsr = clb
                .UseDefaults()
                .Build();

            return prsr;
        }
    }
}