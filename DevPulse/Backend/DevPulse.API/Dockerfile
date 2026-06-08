FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /app

# Copy project files
COPY DevPulse.API.csproj ./DevPulse.API/
COPY ../DevPulse.Data/DevPulse.Data.csproj ./DevPulse.Data/
COPY ../DevPulse.Core/DevPulse.Core.csproj ./DevPulse.Core/

# Restore dependencies
RUN dotnet restore DevPulse.API/DevPulse.API.csproj

# Copy source code
COPY . ./

# Build application
RUN dotnet build DevPulse.API/DevPulse.API.csproj -c Release -o /app/build

FROM mcr.microsoft.com/dotnet/aspnet:7.0

WORKDIR /app

COPY --from=build /app/build .

EXPOSE 8080

ENTRYPOINT ["dotnet", "DevPulse.API.dll"]
