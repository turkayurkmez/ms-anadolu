# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/Basket/ECommerce.Basket.API/ECommerce.Basket.API.csproj", "src/Services/Basket/ECommerce.Basket.API/"]
COPY ["src/Services/Basket/ECommerce.Basket.Infrastructure/ECommerce.Basket.Infrastructure.csproj", "src/Services/Basket/ECommerce.Basket.Infrastructure/"]
COPY ["src/Shared/ECommerce.EventBus/ECommerce.EventBus.csproj", "src/Shared/ECommerce.EventBus/"]
RUN dotnet restore "./src/Services/Basket/ECommerce.Basket.API/ECommerce.Basket.API.csproj"
COPY . .
WORKDIR "/src/src/Services/Basket/ECommerce.Basket.API"
RUN dotnet build "./ECommerce.Basket.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ECommerce.Basket.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ECommerce.Basket.API.dll"]