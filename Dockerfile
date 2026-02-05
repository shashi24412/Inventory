# # STEP 1: Build stage using .NET 9 SDK
# # This image includes everything needed to compile and build .NET 9 applications.
# # If this image is not already available locally, Docker will pull it from Microsoft's registry.
# FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

# # Set the working directory in the container where all commands will run
# WORKDIR /app

# # Copy only the .csproj file(s) to leverage Docker layer caching
# # This helps avoid restoring packages every time the code changes
# COPY *.csproj .

# # Restore NuGet packages based on .csproj file
# RUN dotnet restore

# # Copy the rest of the project files into the container
# COPY . .

# # Build and publish the app in Release mode to the /app/out folder
# RUN dotnet publish -c Release -o out

# # STEP 2: Runtime stage using ASP.NET Core Runtime 9
# # This image is smaller than the SDK and optimized for running ASP.NET Core apps
# # If not available locally, Docker will pull it too.
# FROM mcr.microsoft.com/dotnet/aspnet:9.0

# # Set the working directory for the runtime container
# WORKDIR /app

# # Copy the published build output from the SDK stage
# COPY --from=build /app/out .

# # Expose port 80 for the application
# # EXPOSE 80
# # Define the entry point for the container to run the application
# ENTRYPOINT ["dotnet", "InventoryAPI.dll"]

# ---------- BUILD STAGE ----------
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
# Set working directory
WORKDIR /app

# Copy solution file
COPY Inventory.sln .

# Copy project files (important for restore caching)
# format: COPY <source> <destination>
COPY InventoryAPI/InventoryAPI.csproj InventoryAPI/
COPY Business/Business.csproj Business/
COPY Data/Data.csproj Data/

# Restore dependencies
RUN dotnet restore InventoryAPI/InventoryAPI.csproj

# Copy remaining source code
COPY . .

# Publish API project
RUN dotnet publish InventoryAPI/InventoryAPI.csproj -c Release -o /app/publish

# ---------- RUNTIME STAGE ----------
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 5000

ENTRYPOINT ["dotnet", "InventoryAPI.dll"]

# To run the container command format: 
# docker run -d -p <host_port>:<container_port> --name <container_name> <image_name>
# docker run -d -p 5000:5000 --name inventoryapi inventoryapi



# docker push shashiprasad91/inventoryapi:latest