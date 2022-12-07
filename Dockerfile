FROM mcr.microsoft.com/dotnet/sdk:5.0.407 AS build-env

WORKDIR /app

# Copy csproj and restore as distinct layers
COPY Apollo.Bp.Net.Card.sln ./
COPY Apollo.Bp.Net.Card.Api/*.csproj ./Apollo.Bp.Net.Card.Api/
COPY Apollo.Bp.Net.Card.Core/*.csproj ./Apollo.Bp.Net.Card.Core/
COPY Apollo.Bp.Net.Card.Data/*.csproj ./Apollo.Bp.Net.Card.Data/
COPY Apollo.Bp.Net.Card.Infrastructure/*.csproj ./Apollo.Bp.Net.Card.Infrastructure/

# Copy everything else and build
COPY . ./
RUN dotnet publish Apollo.Bp.Net.Card.sln -c Release -o out --no-restore

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:5.0-alpine


ENV ASPNETCORE_URLS=http://*:8080
ENV ASPNETCORE_FORWARDEDHEADERS_ENABLED=true


USER 1000
WORKDIR /app

COPY --from=build-env /app/out .

EXPOSE 8080
ENTRYPOINT ["dotnet", "Apollo.Bp.Net.Card.Api.dll"]
