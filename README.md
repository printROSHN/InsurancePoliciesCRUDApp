# Insurance Policies CRUD API

## Overview

The Insurance Policies CRUD API is a .NET Core Web API that allows you to manage insurance policies in a database. This application provides endpoints for creating, reading, updating, and deleting insurance policies.

## Project Structure

- **Controllers**: Contains API controllers to handle HTTP requests.
- **Data**: Contains the repository class for database operations.
- **Models**: Contains the data model representing an insurance policy.

## Database Schema

**InsurancePolicies Table**
- `Id` (int, primary key, auto-increment)
- `PolicyNumber` (nvarchar)
- `PolicyHolderName` (nvarchar)
- `StartDate` (datetime)
- `EndDate` (datetime)
- `Type` (nvarchar)
- `PremiumAmount` (decimal)

## API Routes

- **GET** `/api/insurancepolicies`
  - Retrieves a list of all insurance policies.

- **GET** `/api/insurancepolicies/{id}`
  - Retrieves a specific insurance policy by ID.

- **POST** `/api/insurancepolicies`
  - Creates a new insurance policy. Requires a JSON payload with policy details.

- **PUT** `/api/insurancepolicies/{id}`
  - Updates an existing insurance policy by ID. Requires a JSON payload with updated policy details.

- **DELETE** `/api/insurancepolicies/{id}`
  - Deletes an insurance policy by ID.

## Setup and Running

### Prerequisites

- .NET SDK (6.0 or later)
- SQL Server

### Configuration

1. **Clone the Repository**
   ```bash
   git clone https://github.com/yourusername/InsurancePoliciesCRUDApp.git
   cd InsurancePoliciesCRUDApp
   ```
   ## Setup and Running

1. **Update Connection String**
Edit `appsettings.json` and provide your SQL Server connection string:

```json
"ConnectionStrings": {
    "DefaultConnection": "Server=your_server;Database=InsurancePoliciesDb;Trusted_Connection=True;"
}

```

2. **Run Migrations**
   ```bash
   dotnet ef database update
   ```
1. **Run the Application**
   ```bash
   dotnet run
   ```

   The API will be accessible at `http://localhost:5047`.


## Database Setup

Ensure that the database schema is set up by running the following SQL script to create the InsurancePolicies table:

```sql

CREATE TABLE InsurancePolicies (
    Id INT PRIMARY KEY IDENTITY,
    PolicyNumber NVARCHAR(50),
    PolicyHolderName NVARCHAR(100),
    StartDate DATETIME,
    EndDate DATETIME,
    Type NVARCHAR(50),
    PremiumAmount DECIMAL(18, 2)
);

```

## Testing the API

Use tools like Postman or curl to test the API endpoints. Example curl command to get all policies:

```bash

curl -X GET https://localhost:5057/api/insurancepolicies

```


## Contributors

- Kushal Singh
- Mukul Sharma
- Sidharth Nair
- Roshankumar Pokal