# Credit Management System

## Description

The Credit Management System is a web-based application designed to process financial transactions, categorize them, and provide insightful reports. It allows users to upload transaction data in JSON format, view account details, and generate monthly reports.

## Features

1. JSON File Upload: Users can upload JSON files containing account and transaction data.
2. Account Management: View and manage multiple accounts.
3. Transaction Processing: Automatically categorize and flag anomalous transactions.
4. Monthly Reports: Generate detailed monthly reports for each account.
5. Web Dashboard: A user-friendly interface to interact with the system.

## Tech Stack

- Clean Architecture
- Domain-Driven Design (DDD)
- Repository Pattern
- Result Object Pattern
- Command Query Responsibility Segregation (CQRS) Pattern with MediatR library
- ASP.NET Core MVC
- Entity Framework Core
- Postgres DB
- Structured Logging with Serilog
- Unit Testing with NSubstitute
- .NET 8
- Centralized package management and build management

## Third-Party NuGet Packages
- MediatR: For implementing the CQRS pattern.
- FluentValidation.AspNetCore: For request validation.
- Serilog: For structured logging.
- NSubstitute: For creating mock objects in tests.
- FluentAssertions: For expressive assertions in unit tests.

## Architecture
The project follows  **_clean architecture pattern_** with the following main components:
1. Presentation Layer (Web UI)
2. Application Layer (Use Cases)
3. Domain Layer (Business Logic)
4. Infrastructure Layer (Data Access)

## Key Design Decisions
1. CQRS Pattern: Separation of command and query responsibilities for better scalability.
2. Mediator Pattern: Used MediatR for decoupling request/response logic.
3. Repository Pattern: Abstraction of data persistence logic.
4. Domain-Driven Design: Rich domain models with encapsulated business logic.

## Data Model
The core entities in the system are:
1. Account: Represents a financial account with properties like AccountNumber, AccountHolder, and Balance.
2. Transaction: Represents individual financial transactions with properties like Date, Amount, Description, and Category.

## Scaling the System
To handle millions of transactions across thousands of accounts:
1. Database Optimization:
   - Use Postgres Merge to bulk insert the transaction data.
   - Use read replicas for heavy read operations.
   - Implement efficient indexing strategies.
   - Partitioning the transaction data by  date range can also improve the performance.
2. Caching:
   - Implement a distributed caching system (e.g., Redis) to reduce database load.
3. Asynchronous Processing:
   - Use message queues (e.g., RabbitMQ, ServiceBus Blob Trigger) for processing uploads and generating reports asynchronously.
4. Microservices Architecture:
   - Split the monolithic application into microservices(Read and write seprated) for better scalability and maintainability in long run.
5. CDN Integration:
   - Use a Content Delivery Network for static assets to improve load times.

## Data Privacy and Security
1. Encryption:
   - Use strong encryption (e.g., AES-256) for sensitive data at rest.
   - Implement TLS for data in transit.
2. Access Control:
Implement role-based access control (RBAC) to restrict data access.
   - Use OAuth 2.0 and OpenID Connect for secure authentication and authorization.
3. Compliance:
   - GDPR Compliance:
    - Implement data minimization principles.
    - Provide mechanisms for data subject rights (e.g., right to erasure).
   - PSD2 Compliance:
    - Implement strong customer authentication (SCA) for financial transactions.
    - Ensure secure communication channels for open banking APIs.
4. Auditing:
   - Implement comprehensive logging and auditing mechanisms.

## Getting Started
1. Clone the repository https://github.com/sathishsuresh04/CreditManagement 
2. Update the connection string in appsettings.json
 
` "PostgresDbOptions": {
    "ConnectionString": "",
    "DefaultSchema": "credit_management",
    "UseInMemory": false,
    "CommandTimeoutInSeconds": 300,
    "ExecuteRawSql": true
  }
  `
3. Run the application.
4. Upload a JSON file containing transaction data.
4. View the account details and monthly reports.

## License

This project is licensed under the MIT License.
