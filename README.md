# BlogManager with Hexagonal Architecture

## Description

An Example of Hexagonal Architecture.
Project layers are 
 # src project folder
    - BlogManager.Core
    - BlogManager.Adapter.Api
    - BlogManager.Adapter.Application
    - BlogManager.Adapter.PostgreSQL
    - BlogManater.Adapter.Logger
 # test project folder
    - BlogManager.Core.Test
    - BlogManager.Adapter.Api.Test
    - BlogManager.Adapter.PostgreSQL.Test
    - BlogManater.Adapter.Logger.Test


Proper to Hexagonal Architecture design core project is independent from other projects.
Core project has no dependency to other projects.
Application ClassLibrary project is the entry point of the application.
Therefore Application project is dependent to all projects.
The reason of to create an application project as an entry point to remove dependency of Api Project to PostgreSQL repositories

Driver side of the application is Api project.
Driven side of the application is PostgreSQL project and logger project.

Application has 90 percent of test code covearage
Repository tests and core tests are EntityFramework db in memory tests. 
By this way we can test our application without any dependency to database.

For exect



![](/Users/enginkisalar/Desktop/BlogManagerHexagonalArchitecture.png)


## Requirements

- Docker
- Docker Compose

## Installation & Usage

### Clone the Repository

```bash
git clone https://github.com/yourusername/yourrepositoryname.git
cd yourrepositoryname
