#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Memorandum.WebApplication/Memorandum.WebApplication.csproj", "Memorandum.WebApplication/"]
COPY ["Memorandum.Common/Memorandum.Common.csproj", "Memorandum.Common/"]
COPY ["Memorandum.Service/Memorandum.Service.csproj", "Memorandum.Service/"]
COPY ["Memorandum.Repository/Memorandum.Repository.csproj", "Memorandum.Repository/"]
RUN dotnet restore "./Memorandum.WebApplication/./Memorandum.WebApplication.csproj"
COPY . .
WORKDIR "/src/Memorandum.WebApplication"
RUN dotnet build "./Memorandum.WebApplication.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Memorandum.WebApplication.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Memorandum.WebApplication.dll"]