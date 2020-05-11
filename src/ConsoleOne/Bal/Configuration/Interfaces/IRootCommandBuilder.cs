namespace MyCompany.MyExamples.SystemCommandLineOne.ConsoleOne.Bal.Configuration.Interfaces
{
    using System.CommandLine;

    public interface IRootCommandBuilder
    {
        RootCommand CreateRootCommand(string description);
    }
}
