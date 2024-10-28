# Talabat API Project

##  Overview
A scalable e-commerce platform built with modern architectural patterns and best practices. This platform leverages Entity Framework, LINQ, and follows Clean Architecture principles to provide a robust solution for e-commerce needs.

## Architecture
The project is structured into the following layers:

```
src/
├── Talabat.APIs/           # API controllers and presentation layer
├── Talabat.Core/           # Domain entities, interfaces, business logic
├── Talabat.Repository/     # Data access implementation
├── Talabat.Services/       # Business services implementation
```

### Project Structure Explanation
- **Talabat.APIs**: Contains API controllers, filters, and configuration
- **Talabat.Core**: Houses domain entities, interfaces, and core business logic
- **Talabat.Repository**: Implements data access patterns and database operations
- **Talabat.Services**: Contains business service implementations and external integrations

##  Docker Configuration

### Dockerfile
```dockerfile
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# Copy solution and project files
COPY *.sln .
COPY Talabat.APIs/Talabat.APIs.csproj Talabat.APIs/
COPY Talabat.Core/Talabat.Core.csproj Talabat.Core/
COPY Talabat.Repository/Talabat.Repository.csproj Talabat.Repository/
COPY Talabat.Services/Talabat.Services.csproj Talabat.Services/

# Restore NuGet packages
RUN dotnet restore

# Copy the rest of the code
COPY . .

# Build and publish
WORKDIR /app/Talabat.APIs
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /app/Talabat.APIs/out .
ENTRYPOINT ["dotnet", "Talabat.APIs.dll"]
```

### Docker Compose Configuration
```yaml
version: '3.8'
services:
  talabat-api:
    image: talabat-api
    build:
      context: .
      dockerfile: Dockerfile
    ports:
      - "8080:80"
    depends_on:
      - redis
      - sqlserver
    environment:
      - ConnectionStrings__DefaultConnection=Server=sqlserver;Database=TalabatDb;User=sa;Password=YourStrong!Passw0rd;
      - Redis__ConnectionString=redis:6379

  redis:
    image: redis:6.2
    ports:
      - "6379:6379"

  sqlserver:
    image: mcr.microsoft.com/mssql/server:2019-latest
    environment:
      SA_PASSWORD: "Passw0rd"
      ACCEPT_EULA: "Y"
    ports:
      - "1433:1433"
```

##  Getting Started

### Prerequisites
- .NET 6.0 SDK
- Docker and Docker Compose
- Visual Studio 2022 or VS Code

### Running with Docker
1. Clone the repository:
```bash
git clone https://github.com/Mo7ammedd/Talabat-APIs.git
cd Talabat-APIs
```

2. Build and run the containers:
```bash
docker-compose up --build
```

This will start:
- Talabat API on port 8080
- Redis on port 6379
- SQL Server on port 1433

### Local Development
1. Update the connection strings in `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=TalabatDb;User=sa;Password=YourStrong!Passw0rd;"
  },
  "Redis": {
    "ConnectionString": "localhost:6379"
  }
}
```

2. Run the application:
```bash
dotnet restore
dotnet run --project Talabat.APIs
```

## 🔧 Configuration
The application can be configured through:
- Environment variables in docker-compose.yml
- appsettings.json
- User secrets (for local development)

##  API Documentation
- API documentation available at `http://localhost:8080/swagger`
- Detailed endpoint documentation in the Talabat.APIs project

##  Development Workflow
1. Make changes to the code
2. Build the Docker image: `docker-compose build`
3. Run the containers: `docker-compose up`
4. Access the API at `http://localhost:8080`

##  Testing
```bash
# Run unit tests
dotnet test

# Run the application with Docker
docker-compose up
```

##  Monitoring and Logging
- Health checks available at `/health`
- Logs are written to console and can be viewed using `docker-compose logs`

##  Contributing
1. Fork the repository
2. Create a feature branch
3. Commit your changes
4. Push to the branch
5. Create a pull request

##  License
This project is licensed under the MIT License.

##  Support
For support, please create an issue in the repository.