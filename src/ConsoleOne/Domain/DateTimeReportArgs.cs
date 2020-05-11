namespace MyCompany.MyExamples.SystemCommandLineOne.ConsoleOne.Domain
{
    public class DateTimeReportArgs
    {
        public bool IncludeDate { get; set; } = false;

        public string DateFormat { get; set; } = "dddd, dd MMMM yyyy";

        public bool IncludeTime { get; set; } = false;
    }
}
