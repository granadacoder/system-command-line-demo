# system-command-line-demo


Shows how to bind arguments to a POCO object.

Includes two "forks", not just one.  RootCommand with 2 Commands is the technical explanation.


Open up .sln

    \src\Solutions\MyCompany.MyExamples.SystemCommandLineOne.sln

---

Set this to startup project

    \src\ConsoleOne\MyCompany.MyExamples.SystemCommandLineOne.ConsoleOne.csproj


Working examples.

             example one
             MyCompany.MyExamples.SystemCommandLineOne.ConsoleOne.exe mytypeone -a valuea -b valueb -c valuec -d valued -e valuee -f valuef -g valueg -h valueh -i valuei
             
            example two
            MyCompany.MyExamples.SystemCommandLineOne.ConsoleOne.exe showdatetime --includedate false --dateformat "MM/dd/yyyy" --includetime
            
You can also change these in VS developer mode.

Go to properties of (same) csproj, and the "Debug" tab.

Application Arguments currently set to :

    mytypeone -a valuea -b valueb -c valuec -d valued -e valuee -f valuef -g valueg -h valueh -i valuei

or 

    showdatetime --includedate false --dateformat "MM/dd/yyyy" --includetime
