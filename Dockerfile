FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY WebhookService.sln .
COPY src/WebhookService.Api/WebhookService.Api.csproj src/WebhookService.Api/
RUN dotnet restore

COPY src/ src/
WORKDIR /src/src/WebhookService.Api
RUN dotnet publish -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine
WORKDIR /app
COPY --from=build /app .
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "WebhookService.Api.dll"]