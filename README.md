# HMCTS Task Manager Backend API

This is the backend API for the **Task Manager** application.  
It provides an endpoint to create new tasks and persists them in a **SQL Server** database.

- **Framework:** ASP.NET Core Web API
- **Language:** C#
- **Database:** SQL Server
- **ORM:** Entity Framework Core
- **Architecture:** Controller + Service (Layered)
- **API Documentation:** Swagger / OpenAPI
- **Testing:** xUnit + EF Core InMemory
- **Frontend:** React frontend [here](https://github.com/jefsamo/hmcts-fe)

---

## Features

- Create a new task with:
  - Title (required)
  - Description (optional)
  - Status (`Todo`, `InProgress`, `Done`)
  - Due date & time (required)
- Input validation using data annotations
- Centralized error handling
- SQL Server persistence with EF Core migrations
- Unit tests for the service layer
- Auto-generated Swagger API documentation

---

## Project Structure

```txt
TaskApi/
  Controllers/
    TasksController.cs
  Data/
    AppDbContext.cs
  Dtos/
    CreateTaskRequest.cs
    TaskResponse.cs
  Models/
    TaskItem.cs
    TaskStatus.cs
  Services/
    ITaskService.cs
    TaskService.cs
  Program.cs
  appsettings.json

TaskApi.Tests/
  TaskServiceTests.cs
