FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["EdenredApp.API/EdenredApp.API.csproj", "EdenredApp.API/"]
COPY ["EdenredApp.Application/EdenredApp.Application.csproj", "EdenredApp.Application/"]
COPY ["EdenredApp.Infra/EdenredApp.Infra.csproj", "EdenredApp.Infra/"]
COPY ["EdenredApp.Persistance/EdenredApp.Persistance.csproj", "EdenredApp.Persistance/"]
COPY ["EdenredApp.Domain/EdenredApp.Domain.csproj", "EdenredApp.Domain/"]
RUN dotnet restore "EdenredApp.API/EdenredApp.API.csproj"
COPY . .
WORKDIR "/src/EdenredApp.API"
RUN dotnet build "EdenredApp.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "EdenredApp.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EdenredApp.API.dll"]
