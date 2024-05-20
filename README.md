# tasksList Web API

This is a .NET Core Web API project for managing tasks lists of users. It includes authentication and authorization for managers, users, and CRUD operations.

## Getting Started

### Prerequisites

- [.NET Core SDK](https://dotnet.microsoft.com/download)
- [Visual Studio](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/)

### Installation

1. Clone this repository:

```bash
git clone https://github.com/ChayaKrashinski/tasks.git
```

2. Navigate to the project directory:

```bash
cd tasks
```

3. Restore dependencies:

```bash
dotnet restore
```

4. Set up the database:

```bash
dotnet ef database update
```

### Configuration

- Configure authentication and authorization settings in `appsettings.json`.
- Update database connection string in `appsettings.json` if needed.

### Usage

Run the application:

```bash
dotnet run
```

By default, the API will be accessible at `https://localhost:10000`.

## Authentication & Authorization

- The API uses JWT (JSON Web Tokens) for authentication.
- Managers can access all endpoints.
- Users can only access specific endpoints related to task management.
- Unauthorized users will receive a 401 Unauthorized response.

## Technologies Used

- .NET Core
- Entity Framework Core
- JWT Authentication
- Swagger (for API documentation)

## Contributing

Contributions are welcome! Please feel free to submit a pull request.
