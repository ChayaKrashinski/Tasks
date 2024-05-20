Got it! Here's the HTML code converted to Markdown from level 2 ('Navigate to the project directory:') until the end of the README:

```markdown
## Getting Started

### Prerequisites

- [.NET Core SDK](https://dotnet.microsoft.com/download)
- [Visual Studio](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/)

### Installation

1. Clone this repository:

```bash
git clone https://github.com/your-username/task-manager-api.git
```

2. Navigate to the project directory:

```bash
cd task-manager-api
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

By default, the API will be accessible at `https://localhost:5001`.

## Authentication & Authorization

- The API uses JWT (JSON Web Tokens) for authentication.
- Managers can access all endpoints.
- Users can only access specific endpoints related to task management.
- Unauthorized users will receive a 401 Unauthorized response.

## Endpoints

- **GET /api/tasks**: Get all tasks.
- **GET /api/tasks/{id}**: Get a specific task by ID.
- **POST /api/tasks**: Create a new task.
- **PUT /api/tasks/{id}**: Update an existing task.
- **DELETE /api/tasks/{id}**: Delete a task by ID.

## Technologies Used

- .NET Core
- Entity Framework Core
- JWT Authentication
- Swagger (for API documentation)

## Contributing

Contributions are welcome! Please feel free to submit a pull request.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
```

This Markdown code should now be ready to be copied into your README.md file. Let me know if you need further assistance!
