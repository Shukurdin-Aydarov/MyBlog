#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Services/Location/Location.Api/Location.Api.csproj", "Services/Location/Location.Api/"]
RUN dotnet restore "Services/Location/Location.Api/Location.Api.csproj"
COPY . .
WORKDIR "/src/Services/Location/Location.Api"
RUN dotnet build "Location.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Location.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Location.Api.dll"]
