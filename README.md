# system-command-line-demo

Open up .sln

    \src\Solutions\MyCompany.MyExamples.SystemCommandLineOne.sln

---

Set this to startup project

    \src\ConsoleOne\MyCompany.MyExamples.SystemCommandLineOne.ConsoleOne.csproj

Go to properties of (same) csproj, and the "Debug" tab.

Application Arguments currently set to :

    --mytypeone -a valuea -b valueb -c valuec -d valued -e valuee -f valuef -g valueg -h valueh -i valuei

---

Find this method in Program.cs (and set breakpoint)

    RunJustRootCommandDemo

---


Points of Interest:

The class:

    MyCompany.MyExamples.SystemCommandLineOne.ConsoleOne.Bal.Configuration.MyTypeCommandCreator

is coded exactly as the online demo, as far a I can tell.

----

csproj

    <PackageReference Include="System.CommandLine.Experimental" Version="0.3.0-alpha.19577.1" />

---