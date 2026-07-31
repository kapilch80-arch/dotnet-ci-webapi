# dotnet-ci-webapi

A minimal **ASP.NET Core (C#) Web API** — a *runnable service*, used to demonstrate
the full CI pipeline including the **publish → container image** phase.

- `src/WeatherApi` — a minimal-API service with `/`, `/health`, and `/forecast/{days}` endpoints.
- `src/WeatherApi/ForecastService.cs` — deterministic forecast logic (unit-tested, Sonar-analyzed).
- `tests/WeatherApi.Tests` — xUnit tests for the service.
- `Dockerfile` — a real multi-stage build; the CI publish phase uses it, so the pushed image runs.

There is intentionally **no CI workflow** — it's meant to have one generated via a pull request.

## Run locally

```bash
dotnet run --project src/WeatherApi
# then browse http://localhost:5000/forecast/5
```

## Build & test

```bash
dotnet restore
dotnet build --configuration Release
dotnet test
```

## Container

```bash
docker build -t weatherapi .
docker run -p 8080:8080 weatherapi   # http://localhost:8080/health
```
