# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore dependencies
COPY ["ServiceApiTier.csproj", "./"]
RUN dotnet restore "ServiceApiTier.csproj"

# Copy everything else and build
COPY . .
RUN dotnet build "ServiceApiTier.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "ServiceApiTier.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Copy published app
COPY --from=publish /app/publish .

# Run as non-root user for security
USER $APP_UID

ENTRYPOINT ["dotnet", "ServiceApiTier.dll"]