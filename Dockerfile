########## Build stage ##########
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /sln

# Copy the full dir structure from this level into the image
# Would normally be more selective than this, copying in solution,
# src tree and any custom NuGet.config for local package repos
COPY . .

# Restore packages, reporting on versions
RUN dotnet restore MartenExperiments.sln
RUN dotnet --info

# Build and test the solution.
RUN dotnet build MartenExperiments.sln -c Release --no-restore
RUN dotnet test MartenExperiments.sln

########## Publish stage ##########
FROM build AS publish

ENV ASPNETCORE_ENVIRONMENT=Production

# Generate the published output into /publish
RUN dotnet restore
RUN dotnet publish -c Release ./src/Domain/Api/Api.csproj -o /publish/ --no-build --no-restore

########### Runtime stage. Create a release image ##########
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app

# Create non root user in the 10k range (for collision avoidance).
RUN addgroup --gid 10000 --system dotnetapp && \
    adduser --system --uid 10000 --gid 10000 dotnetapp

USER dotnetapp

# Copy published  files here owned by dotnetapp user.
COPY --chown=dotnetapp:dotnetapp --from=publish /publish .

WORKDIR /app

# Set recommended environment variables.
ENV DOTNET_RUNNING_IN_CONTAINER=true
ENV DOTNET_CLI_TELEMETRY_OPTOUT=1

# Opt out of diagnostic pipeline so we can run readonly. Not sure if this is still relevant but was a thing in earlier dotnet core versions
ENV COMPlus_EnableDiagnostics=0

# Label image with some meta data on team and project
LABEL "team"="martenexperiments"
LABEL "project"="martenexperimentsapi"

########## Final stage. Setup the final image that will be pushed to the container repository. ##########
FROM runtime AS final

# Environment variables for runtime. Run on port well away from standard services for collision avoidance
ENV ASPNETCORE_URLS=http://+:51770

# Poke a hole
EXPOSE 51770/tcp

WORKDIR /app

ENTRYPOINT ["dotnet", "Api.dll"]

USER dotnetapp
