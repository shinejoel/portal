# About

Stack:
- .NET 6
- Entity Framework Core 6
- NLog
- Angular 13

## Architecture overview

### Domain

This will contain all entities, enums, exceptions, interfaces, types, and logic specific to the project domain field.

### Application
This layer contains all *application* logic. It is dependent on the domain layer but has no dependencies on any other layer or project. 

This layer defines interfaces that are implemented by all other layers (except Domain). For example, if the application needs to run some special functions (send a mail, etc.), a special interface will be created in the App project and implementation would be created within the infrastructure level.

### Infrastructure
This layer contains classes for accessing external resources such as file systems, web services, SMTP, and so on. These classes should be based on interfaces defined within the application layer.

### WebApi
Web API project based on ASP .NET Core (5.0) with Angular SPA. This layer depends on both the Application and Infrastructure layers, however, the dependency on Infrastructure is only to support dependency injection. Therefore only Startup.cs should reference Infrastructure.