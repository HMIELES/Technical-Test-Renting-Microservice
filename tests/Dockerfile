# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

COPY *.sln .
COPY src/ ./src/
COPY tests/ ./tests/

WORKDIR /app/tests/Renting.UnitTests
RUN dotnet restore

# Ejecutar tests
RUN dotnet test --verbosity normal

CMD ["dotnet", "test", "--verbosity", "normal"]
