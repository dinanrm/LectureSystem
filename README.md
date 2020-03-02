# LectureSystem
Backend application that created to manage lecture activities virtually. Created using ASP .NET Core Web API + Razor Page, the database uses SQL Server and the application is currently hosted on Azure Cloud.

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes. See deployment for notes on how to deploy the project on a live system.

### Prerequisites

What things you need to install the software and how to install them

### 1) .NET Core 3.1

.NET Core is a cross-platform version of .NET for building websites, services, and console apps. There are no fees or licensing costs, including for commercial use.
You can install it by following the insctructions in this [page](https://dotnet.microsoft.com/download)

### 2) Microsoft Visual Studio

Full-featured IDE to code, debug, test, and deploy to any platform.
You can install it by following the insctructions in this [page](https://visualstudio.microsoft.com/vs/)

### 3) Microsoft Visual Studio Code

Visual Studio Code is a lightweight but powerful source code editor which runs on your desktop and is available for Windows, macOS and Linux.
You can install it by following the insctructions in this [page](https://code.visualstudio.com/download)

### 4) Microsoft SQL Server Management Studio (only windows, another OS in no 5)

SQL Server Management Studio (SSMS) is an integrated environment for managing any SQL infrastructure, and we used it to store and manage data. 
You can install it by following the insctructions in this [page](https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver15)

### 5) Azure Data Studio (alternative of no 4)

Azure Data Studio is a cross-platform database tool for data professionals using the Microsoft family of on-premises and cloud data platforms on Windows, MacOS, and Linux. 
You can install it by following the insctructions in this [page](https://docs.microsoft.com/en-us/sql/azure-data-studio/what-is?view=sql-server-ver15)

### Setup the project

1) Set connection string
In ASP.NET Core the configuration system is very flexible, and the connection string could be stored in appsettings.json, an environment variable, the user secret store, or another configuration source. 
The following example shows the connection string stored in `appsettings.json`.

```
{
  "ConnectionStrings": {
    "BloggingDatabase": "Server=(localdb)\\mssqllocaldb;Database=EFGetStarted.ConsoleApp.NewDb;Trusted_Connection=True;"
  },
}
```

2) Modify the values
- Change `BloggingDatabase` with your database server information.
- Also change the name in the `startup.cs` file.

```
public void ConfigureServices(IServiceCollection services)
{
    services.AddDbContext<BloggingContext>(options =>
        options.UseSqlServer(Configuration.GetConnectionString("BloggingDatabase")));
}
```

## Run the app
a. If you are using Visual Studio IDE, just press `Ctrl+F5` to run without the debugger.
Visual Studio displays the following dialog:

![alt text](https://docs.microsoft.com/en-us/aspnet/core/getting-started/_static/trustcert.png?view=aspnetcore-3.1)

Select Yes if you trust the IIS Express SSL certificate.
The following dialog is displayed:

![alt text](https://docs.microsoft.com/en-us/aspnet/core/getting-started/_static/cert.png?view=aspnetcore-3.1)

Select Yes if you agree to trust the development certificate.

b. If you are not using Visual Studio IDE, first trust the HTTPS development certificate by running the following command:
```
dotnet dev-certs https --trust
```
The preceding command displays the following dialog:

![alt text](https://docs.microsoft.com/en-us/aspnet/core/getting-started/_static/cert.png?view=aspnetcore-3.1)

Select Yes if you agree to trust the development certificate.
Press Ctrl-F5 to run without the debugger.

## Deployment

You can deploy it by following the insctructions in this [page](https://docs.microsoft.com/en-us/aspnet/core/tutorials/publish-to-azure-webapp-using-vs?view=aspnetcore-3.1)

## Built With

* [Dropwizard](http://www.dropwizard.io/1.0.2/docs/) - The web framework used
* [Maven](https://maven.apache.org/) - Dependency Management
* [ROME](https://rometools.github.io/rome/) - Used to generate RSS Feeds

## Contributing

Please read [CONTRIBUTING.md](https://gist.github.com/PurpleBooth/b24679402957c63ec426) for details on our code of conduct, and the process for submitting pull requests to us.

## Versioning

We use [SemVer](http://semver.org/) for versioning. For the versions available, see the [tags on this repository](https://github.com/your/project/tags).

## Documentation
![alt text](https://raw.githubusercontent.com/dinanrm/LectureSystem/develop/ALE%20-%20Lecture%20System%2C%20v22.png)

## Authors

* **Dinan Rangga Maulana**
[GitHub](https://github.com/dinanrm) | [GitLab](https://gitlab.com/dinanrm) | [LinkedIn](https://www.linkedin.com/in/dinanrm/) 
