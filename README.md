#Description

Create an application to demonstrate your skills
Approximate time to spend on the task: 2 hours

**List of things we would like to see demonstrated**:
1) Communication between backend and frontend (WCF, Rest, SOAP) 
2) A fundamental understanding of how to make and organize UI code. Any pattern (MVC, MVVM etc.) and framework (ASP .NET, Xamarin, WPF) can be used
3) Data storage on the backend. E.g. Serialization/Deserialization to file or integration with Sql. Sql script, Entity Framework, Serialization
4) A firm understanding of more advanced .Net concepts. E.g. Reflection, Generics and Linq, Threading
5) Unit tests on all code, or at least, be able to show how all code is unit testable. E.g. NUnit, Moq, Constructor Injection

**Environment**: The demo should run in Visual Studio and only make use of frameworks available through NuGet.

**Guideline**: There is no requirement to the type of demo you choose to make. An example could be a
website where it is possible to enter a series of letters that when send to a web service will be manipulated
and stored. Through the website it will also be possible to retrieve already saved data and display it.
Note: Focus on the things you are already familiar with, to avoid spending too much time on investigations.


#Result

* **Self Hosted** This application is self hosting for both Soap and Rest. So please run VS as Admin. The tests will start up the console automatically. Just run the tests.
* **Visual Studio**: For fun i decided to use / try VS 2017. Haven't made anything for it yet. So it provided a little extra motivation for the exercise.
* **GAIT code base**: I have added some standard code base from my own comapny GAIT (Generic Automation and IT), just to simplify the work. No unit tests are included for the Gait.x code here.
* **Frontend**: I have not made any frontend. The tests will be integration tests the hit a service/host. What ever pattern is used for the FE integration with the backend, a model would be send and/or queried. And the services i have provided allow a command model and / or query result... So I did not see the value in providing any UI when we have u/integration tests.
* **Unit tests** There really was not time enough to create TDD / uTests for everything in this exercise. I have only made integration tests. Which connect to the services Soap and Rest service.
* **Database** I will use an sqlite file db for portability. The migrations are run on startup for portability. **NB** A sqlite file is created at "c:\RikCodeCampDb.db" i try to delete it if you exict, but yo may need to delete manually

##Where to start looking?
* Rik.CodeCamp.Host is the startup project with the services.
Look at:
* Integration tests, BarContractTests -> Soap with Castle Wcf Facilty for client and server (could be any Wcf)
* Integration tests, BraveServiceTests -> Rest with Nancy

**The Unit Test are Green, Try and run :-)**

Topshelf/Host startup sequence:
1.  Program
2.  CodeCampControl
    1.  BootStrapper
        1.  General Registrations (including Soap Wcf)
        2.  NancyBootstrapper
    2.  Worker.Start()

Then both the soap (through Castle) and Rest (Through Nancy) are running and self hosted

##Handlers
There is two Cqrs handlers used by both the rest and soap hosts. Both called "BraveHandler". but one is for queries, the other for a request. They complete the business trom a call to the services.


#Nuget Packages

##Own packages
I have used some of my own packages (links are from nuget, project linkts to github in nuget):
* [Smooth.IoC.Dapper.Repository.UnitOfWork](https://www.nuget.org/packages/Smooth.IoC.Dapper.Repository.UnitOfWork/)
* [Smooth.IoC.Cqrs.Tap](https://www.nuget.org/packages/Smooth.IoC.Cqrs.Tap/)

##Others
* Hosting Service: [Topshelf](https://www.nuget.org/packages/Topshelf/), to run/host the backend application
* IoC: Castle Windsor, I know you use Ninject. Same same, but different. Castle will also be used in regard to the soap client/server
* ORM: [Dapper](https://www.nuget.org/packages/Dapper/) and Dapper.FastCRUD (integrated through Smooth.IoC.Dapper...), I know you use Entity FrameWork, but to be honest i am just not fond of entity (SELECT n+1)
* Tests with, NUnit, [Fluent Assertions](https://www.nuget.org/packages/FluentAssertions/)
* Database: 
    * [Sqlite.Core](https://www.nuget.org/packages/System.Data.SQLite.Core/), for portablility and exampling (in memory)
    * [Simple.Migrations](https://www.nuget.org/packages/Simple.Migrations/), because it was simple to use for inproc migrations.
* Rest Host: [Nancy](https://www.nuget.org/packages/Nancy/1.4.3) with castle windsor integration





