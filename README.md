# senthoor-dotnet

A production-ready .NET 9 Web API base project built from scratch using clean separation of concerns and standard industry design patterns.

## Architecture

This solution follows a layered architecture structured as follows:

*   **`BaseDotnet.Domain`**: Contains core abstractions, base entities, custom Result types, and domain models. Independent of all other projects and external libraries.
*   **`BaseDotnet.Application`**: Defines service interfaces (abstraction layer), DTOs, AutoMapper mapping profiles, and validation rules.
*   **`BaseDotnet.Infrastructure`**: Handles persistence via EF Core, repository implementations, third-party service implementations, and auditable metadata interceptors.
*   **`BaseDotnet.API`**: Entry point of the web service, standard MVC controllers, middleware, and OpenAPI (Scalar) documentation.
*   **`BaseDotnet.Processor`**: A background console/worker scheduler project for running background jobs.
