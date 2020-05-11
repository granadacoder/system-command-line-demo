namespace MyCompany.MyExamples.SystemCommandLineOne.ConsoleOne.Bal.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.CommandLine;

    using Microsoft.Extensions.Logging;

    using MyCompany.MyExamples.SystemCommandLineOne.ConsoleOne.Bal.Configuration.Interfaces;

    public class RootCommandBuilder : IRootCommandBuilder
    {
        public const string ErrorMessageILoggerFactoryIsNull = "ILoggerFactory is null";
        public const string ErrorMessageICommandCreatorIsNull = "ICommandCreator is null";

        public const string LogMessageAddingICommandCreator = "Adding ICommandCreator. (Command Description=\"{0}\")";

        private readonly ILogger<RootCommandBuilder> logger;
        private readonly IEnumerable<ICommandCreator> commandCreators;

        public RootCommandBuilder(ILoggerFactory loggerFactory, IEnumerable<ICommandCreator> commandCreators)
        {
            if (null == loggerFactory)
            {
                throw new ArgumentNullException(ErrorMessageILoggerFactoryIsNull, (Exception)null);
            }

            this.logger = loggerFactory.CreateLogger<RootCommandBuilder>();

            this.commandCreators = commandCreators ?? throw new ArgumentNullException(ErrorMessageICommandCreatorIsNull, (Exception)null);
        }

        public RootCommand CreateRootCommand(string description)
        {
            RootCommand returnItem = new RootCommand(description);

            foreach (ICommandCreator icc in this.commandCreators)
            {
                Command cmd = icc.CreateCommand();
                this.logger.LogInformation(string.Format(LogMessageAddingICommandCreator, returnItem.Description, cmd.Description));
                returnItem.AddCommand(cmd);
            }

            return returnItem;
        }
    }
}