From mcr.microsoft.com/dotnet/sdk:10.0 as build 
WORKDIR /src 

COPY FlowAISystem.WebApp/*.csproj FlowAISystem.WebApp/
COPY FlowAISystem.Data/*.csproj FlowAISystem.Data/
COPY FlowAISystem.Core/*.csproj FlowAISystem.Core/
COPY FlowAISystem.slnx .

Run dotnet restore FlowAISystem.slnx

COPY . . 

Run dotnet publish FlowAISystem.WebApp/FlowAISystem.WebApp.csproj -c Release -o /app/publish


From mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT [ "dotnet", "FlowAISystem.WebApp.dll" ]