CoreFitness - Gym Booking Portal
A full-stack web application built with ASP.NET Core MVC for managing gym memberships and class bookings. 
This project is developed as a final assignment, focusing on Clean Architecture and Domain-Driven Design (DDD) principles.

🏗️ Architecture & Patterns
The solution is divided into four distinct layers to ensure separation of concerns:

Domain: Core entities, aggregates, and domain exceptions.

Application: Services, DTOs (Records), and the Result Pattern for handling operations.

Infrastructure: Data persistence using Entity Framework Core, Repository Pattern, and Unit of Work.

Presentation: ASP.NET Core MVC with responsive design and Identity for authentication.

Tech Stack
Backend: .NET 8 / ASP.NET Core MVC

Database: SQL Server (Production) & InMemory (Testing/Development)

Security: ASP.NET Core Identity with Role-based authorization (Admin/Member)

Frontend: HTML5, CSS (Flexbox/Responsive), and JavaScript

The project is configured to use an EF Core In-Memory database by default for development and evaluation. This means no local SQL Server setup is required to run the application.

Clone the repository:
git clone [https://github.com/your-username/aspnet-rasmus-pieplow.git](https://github.com/your-username/aspnet-rasmus-pieplow.git)

Open the project:
Open the solution file (.sln) in Visual Studio 2022.

Run the App:
Simply press F5 or click the Start button. The application will start and seed the in-memory database with test data automatically.

Testing:
Open the Test Explorer to run the unit and integration tests. These also utilize the In-Memory provider to verify system functionality.


dmin Credentials:
To access administrative features (Create/Delete classes), use the following seeded account:

Email: admin@corefitness.com

Password: Admin123!


