# Contact App

Contact App is a web-based application that allows users to manage contact information using a .NET Core 8 backend and an Angular frontend. The application follows the Clean Architecture pattern and leverages the CQRS (Command Query Responsibility Segregation) pattern for effective separation of read and write operations. This approach ensures scalability, maintainability, and a clean codebase structure.


### Table of Contents



* [Technologies Used](https://github.com/navalkbhatt/Contact-App#technologies-used)
* [Project Structure](https://github.com/navalkbhatt/Contact-App#project-structure)
* [Getting Started](https://github.com/navalkbhatt/Contact-App#getting-start)
   - [Prerequisites](https://github.com/navalkbhatt/Contact-App#prerequisites)
   - [Backend Setup](https://github.com/navalkbhatt/Contact-App#backend-setup)
   - [Frontend Setup](https://github.com/navalkbhatt/Contact-App#frontend-setup)
* [Running the Application](https://github.com/navalkbhatt/Contact-App#getting-start)
* [Core Features](https://github.com/navalkbhatt/Contact-App#getting-start)
* [API Documentation](https://github.com/navalkbhatt/Contact-App#getting-start)
* [Troubleshooting](https://github.com/navalkbhatt/Contact-App#getting-start)

----------
## Technologies Used
* Backend: .NET Core 8
* Frontend: Angular with Bootstrap 
* CQRS Pattern: Implemented using the MediatR library
* Architecture: Clean Architecture principles
* Database: Json Files
* API Documentation: Swagger for interactive documentation

## Project Structure
  
```bash
ContactApp/
  ├──Contant-App #Angular Front End
  ├── Server
       ├── Application  # Application layer (CQRS commands/queries)
          ├── ContactApp.Application.Dto
          ├── ContactApp.Application.UseCases 
              ├── Behaviours
              ├── Commons
              ├── Contacts
                  ├── Commands
                  ├── Queries
                  ├──    
              ├── Exceptions
              ├── Mapping

       ├── Domain  #Domain layer (Entities, interfaces, core logic)
         ├── Entities 
       ├── Service
         ├── ContactAppApi # API project (Presentation layer)
```
------          
## Getting Started
### Prerequisites

Before you begin, ensure you have the following installed

* [.NET SDK 8.0](https://dotnet.microsoft.com/en-us/download)
* [Node.js and npm](https://nodejs.org/en)
* [Angular CLI](https://angular.dev/tools/cli/setup-local)
* [Docker](https://www.docker.com/products/docker-desktop)
### Backend Setup
```bash
git clone https://github.com/navalkbhatt/Contact-App
cd Contact-App\Server
docker build -t contactapi:v.1.0 .
docker run --user root -e ASPNETCORE_ENVIRONMENT=Development -d -p 8080:5000 contactapi:v1.0  # Run from root or give the contact.json file read/write permission
http://localhost:8080/swagger/index.html
```
### Frontend Setup
```bash
cd Contact-App\contactApp
docker build -t contact-client-app:v1.0 .
docker run -d -p 8000:80 --name contactclientApp contact-client-app:v1.0
Browse the application  http://localhost:8000
```
### Running the Application
Client and Application are running on Docker Container. These contaniner can be host on PODS in EKS/AKS Cluster. 
   
