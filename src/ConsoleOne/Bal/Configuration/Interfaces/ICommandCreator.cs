using System.CommandLine;

namespace MyCompany.MyExamples.SystemCommandLineOne.ConsoleOne.Bal.Configuration.Interfaces
{
    public interface ICommandCreator
    {
        RootCommand CreateRootCommand();
    }
}
