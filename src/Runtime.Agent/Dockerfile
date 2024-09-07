FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["Runtime.Agent.csproj", "Runtime.Agent/"]
RUN dotnet restore "Runtime.Host/Runtime.Agent.csproj"

COPY [".", "Runtime.Agent/"]

WORKDIR "/src/Runtime.Agent"

RUN dotnet build "Runtime.Agent.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Runtime.Agent.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Runtime.Agent.dll"]
