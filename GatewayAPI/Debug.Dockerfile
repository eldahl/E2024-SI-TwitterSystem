﻿FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app

# Swagger
ENV ASPNETCORE_ENVIRONMENT="Development"

EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Debug
WORKDIR /src
COPY ["GatewayAPI/GatewayAPI.csproj", "GatewayAPI/"]
COPY ["AccountService/AccountService.csproj", "AccountService/"]
COPY ["PostsService/PostsService.csproj", "PostsService/"]
COPY ["TimelineService/TimelineService.csproj", "TimelineService/"]
RUN dotnet restore "GatewayAPI/GatewayAPI.csproj"
COPY . .
WORKDIR "/src/GatewayAPI"
RUN dotnet build "GatewayAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM base AS final
WORKDIR /app
COPY --from=build /app/build .
ENTRYPOINT ["dotnet", "GatewayAPI.dll"]
