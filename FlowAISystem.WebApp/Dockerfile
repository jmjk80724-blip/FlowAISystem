# Base Image
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080

# Build Image
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy Projects
COPY ["FlowAISystem.WebApp/FlowAISystem.WebApp.csproj", "FlowAISystem.WebApp/"]
COPY ["FlowAISystem.Core/FlowAISystem.Core.csproj", "FlowAISystem.Core/"]
COPY ["FlowAISystem.Data/FlowAISystem.Data.csproj", "FlowAISystem.Data/"]

# Restore
RUN dotnet restore "FlowAISystem.WebApp/FlowAISystem.WebApp.csproj"

# Copy All
COPY . .

# Build
WORKDIR "/src/FlowAISystem.WebApp"
RUN dotnet build -c Release -o /app/build

# Publish
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

# Final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FlowAISystem.WebApp.dll"]