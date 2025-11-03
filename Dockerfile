# Use the official .NET SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy solution file and all .csproj files
COPY BookLendingService.sln ./
COPY **/*.csproj ./


# Copy everything else and build
COPY . ./
RUN dotnet publish BookLendingService/BookLendingService.API.csproj -c Release -o /app/out


# Use the ASP.NET runtime image for the final container
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

# Expose port and run
EXPOSE 80
ENTRYPOINT ["dotnet", "BookLendingService.API.dll"]