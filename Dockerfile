# Multi-stage build for the WeatherApi ASP.NET Core service.
# The CI pipeline detects this Dockerfile and uses it (instead of a generated
# default) for the publish → container-image phase, so the pushed image runs.
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore
RUN dotnet publish src/WeatherApi/WeatherApi.csproj -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .
# .NET 8 container images listen on 8080 by default.
EXPOSE 8080
ENTRYPOINT ["dotnet", "WeatherApi.dll"]
