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
- .NET 8, C#
- Centralized package management and build management

## Third-Party NuGet Packages
- MediatR: For implementing the CQRS pattern.
- FluentValidation.AspNetCore: For request validation.
- Serilog: For structured logging.
- NSubstitute: For creating mock objects in tests.
- FluentAssertions: For expressive assertions in unit tests.

## Architecture
The project follows a **_clean architecture pattern_** with the following main components:
1. Presentation Layer (Web UI)
2. Application Layer (Use Cases)
3. Domain Layer (Business Logic)
4. Persistence (Data Access) and Infrastructure Layer (External Concerns like email, etc.)

## Key Design Decisions
1. CQRS Pattern: Separation of command and query responsibilities for better scalability.
2. Mediator Pattern: Used MediatR for decoupling request/response logic.
3. Repository Pattern: Abstraction of data persistence logic.
4. Domain-Driven Design: Rich domain models with encapsulated business logic.
5. Conventional commnit

## Data Model
The core entities in the system are:
1. Account: Represents a financial account with properties like AccountNumber, AccountHolder, and Balance.
2. Transaction: Represents individual financial transactions with properties like Date, Amount, Description, and Category.

## Scaling the System
To handle millions of transactions across thousands of accounts:
1. **Database Optimization**:
    - Use efficient bulk insert methods (`COPY`, `Merge` in PostgreSQL).
    - Implement effective indexing strategies on frequently queried columns like AccountId, Date, and Category.
    - Partition transaction tables by date for improved query performance.
    - Utilize read replicas to offload read operations from the primary server.
2. **Caching**:
    - Implement distributed caching solutions (e.g., Redis) for frequently accessed data like monthly reports.
3. **Asynchronous Processing**:
    - Integrate message queues (e.g., RabbitMQ, Azure Service Bus) for decoupling and processing tasks asynchronously.
    - Use background job libraries (e.g., Hangfire, Quartz) for scheduled tasks and long-running operations.
4. **Content Delivery Network (CDN)**:
    - Serve static assets via a CDN to reduce server load and improve response times.
5. **Elastic Infrastructure**:
    - Containerize the application using Docker.
    - Use orchestration tools (e.g., Kubernetes) to manage and scale containerized workloads dynamically.

## Data Privacy and Security

1. **Encryption**:
    - Use AES-256 for encrypting sensitive data at rest, especially storing and retrieving. Use an encryption service to encrypt and decrypt using a key which is securely stored at the KeyVault.
    - Implement TLS to secure data in transit.

2. **Access Control**:
    - Implement Role-Based Access Control (RBAC) to restrict data access based on user roles like Admin, Customer, Super User, etc., at the controller and UI level.
    - Use JWT (JSON Web Token) authentication for secure access to endpoints.

3. **Compliance**:
    - **GDPR Compliance**:
        - Collect only necessary data and anonymize wherever possible.
        - Provide mechanisms, like an endpoint, to capture user consents and non-consents for data access and erasure requests.
        - Use background jobs to anonymize the account number, name, and personal number after a certain period.

4. **Auditing**:
    - Implement comprehensive logging for user actions, system events, and security incidents with Serilog (Audit Trails).
    - Maintain audit trails for critical operations, enabling traceability and accountability.
    - Use log management solutions (e.g., ELK stack or Grafana, Loki, Prometheus stack) for security monitoring and incident response.

## Getting Started
1. Clone the repository from https://github.com/sathishsuresh04/CreditManagement
2. Open it in using Vs code ,Visual studio or Jetbrains Rider
2. Update the connection string in `appsettings.json`:

```json
  "PostgresDbOptions": {
    "ConnectionString": "<your-connection-string>",
    "DefaultSchema": "credit_management",
    "UseInMemory": false,
    "CommandTimeoutInSeconds": 300,
    "ExecuteRawSql": true
  }
```

3. Run the application.
4. Upload a JSON file containing transaction data. [AccountsTransactions.json](Data/AccountsTransactions.json)
5. View the account details and monthly reports.

## License

This project is licensed under the MIT License.