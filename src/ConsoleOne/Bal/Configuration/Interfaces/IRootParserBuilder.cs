namespace MyCompany.MyExamples.SystemCommandLineOne.ConsoleOne.Bal.Configuration.Interfaces
{
    using System.CommandLine.Parsing;

    public interface IRootParserBuilder
    {
        Parser CreateParser();
    }
}
