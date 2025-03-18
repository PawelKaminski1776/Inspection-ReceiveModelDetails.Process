# Use the official .NET SDK image for building the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env

# Set the working directory inside the container
WORKDIR /app

# Copy the solution file and project files
COPY Inspection-ReceiveModelDetails.Process.sln ./  
COPY Inspection-ReceiveModelDetails.Channel/*.csproj ./Inspection-ReceiveModelDetails.Channel/
COPY Inspection-ReceiveModelDetails.Controllers/*.csproj ./Inspection-ReceiveModelDetails.Controllers/
COPY Inspection-ReceiveModelDetails.Messages/*.csproj ./Inspection-ReceiveModelDetails.Messages/
COPY Inspection-ReceiveModelDetails.Handlers/*.csproj ./Inspection-ReceiveModelDetails.Handlers/
COPY Inspection-ReceiveModelDetails.Process/*.csproj ./Inspection-ReceiveModelDetails.Process/

# Copy appsettings.json file (ensure it exists in the root of your project)
COPY appsettings.json /app/appsettings.json

# Restore dependencies
RUN dotnet restore

# Copy the rest of the source code
COPY . ./  

# Set environment to Production
ENV ASPNETCORE_ENVIRONMENT=Production

# Build the application
RUN dotnet publish -c Release -o /out

# Use the official .NET runtime image for running the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0

# Set the working directory inside the runtime container
WORKDIR /app

# Copy the built application from the build stage
COPY --from=build-env /out .

# Copy the appsettings.json file to the container
COPY appsettings.json /app/appsettings.json

# Default Service Port
EXPOSE 5009

# Set the entry point to the application
ENTRYPOINT ["dotnet", "Inspection-ReceiveModelDetails.Process.dll"]
