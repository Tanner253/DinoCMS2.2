
# Project Dino CMS

---------------------------------
## We are deployed on Azure!
https://prehistoricrealism.azurewebsites.net/
cant fund anymore had to take down temporarily :( will be back up when the game gets updated **COMING SOON**

---------------------------------
## Web Application
Stores data for each dino that is available in the game - making it available to the players.

---------------------------------

## Tools Used
Microsoft Visual Studio Community 2019

- C#
- ASP.Net Core
- Entity Framework
- MVC
- xUnit
- Bootstrap
- Azure


---------------------------------

## Recent Updates
Initial build

---------------------------

## Getting Started

Clone this repository to your local machine.
```
$ git clone https://github.com/Tanner253/DinoCMS.git
```
Once downloaded, you can either use the dotnet CLI utilities or Visual Studio 2017 (or greater) to build the web application. The solution file is located in the AmandaFE subdirectory at the root of the repository.
```
cd DinoCMS/Scaffold
dotnet build
```
The dotnet tools will automatically restore any NuGet dependencies. Before running the application, the provided code-first migration will need to be applied to the SQL server of your choice configured in the /AmandaFE/AmandaFE/appsettings.json file. This requires the Microsoft.EntityFrameworkCore.Tools NuGet package and can be run from the NuGet Package Manager Console:
```
Update-Database
```
Once the database has been created, the application can be run. Options for running and debugging the application using IIS Express or Kestrel are provided within Visual Studio. From the command line, the following will start an instance of the Kestrel server to host the application:
```
cd YourRepo/YourProject
dotnet run
```

---------------------------------

## Usage


### Overview of Recent Posts
![Home](/DinoCMS/DinoCMS/Data/Readme/Home.JPG)

### Creating a Post
![Post Creation](/DinoCMS/DinoCMS/Data/Readme/Create.JPG)

### Enriching a Post
![Enriching Post](/DinoCMS/DinoCMS/Data/Readme/DIndex.JPG)

### Viewing Post Details
![Details of Post](/DinoCMS/DinoCMS/Data/Readme/Details.JPG)

---------------------------

---------------------------

### Overall Project Schema

---------------------------
## Model Properties and Requirements
---------------------------

## Change Log
1.0: Launch (to come)
1.1: CRUD Works
1.2: Crud cleaned up
1.3: Working on Identity and authorization / Security

------------------------------

## Authors
Tanner Percival

------------------------------
