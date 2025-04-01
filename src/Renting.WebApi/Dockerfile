# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

COPY *.sln ./
COPY src/Renting.Domain/Renting.Domain.csproj ./src/Renting.Domain/
COPY src/Renting.Application/Renting.Application.csproj ./src/Renting.Application/
COPY src/Renting.Infrastructure/Renting.Infrastructure.csproj ./src/Renting.Infrastructure/
COPY src/Renting.WebApi/Renting.WebApi.csproj ./src/Renting.WebApi/
COPY tests/Renting.UnitTests/Renting.UnitTests.csproj ./tests/Renting.UnitTests/

RUN dotnet restore

COPY src/ ./src/
COPY tests/ ./tests/
WORKDIR /app/src/Renting.WebApi
RUN dotnet publish -c Release -o out

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app
COPY --from=build /app/src/Renting.WebApi/out ./
EXPOSE 5000
ENTRYPOINT ["dotnet", "Renting.WebApi.dll"]
