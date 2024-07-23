#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["StudyWire.API/StudyWire.API.csproj", "StudyWire.API/"]
COPY ["Shared/Shared.csproj", "Shared/"]
COPY ["StudyWire.Application/StudyWire.Application.csproj", "StudyWire.Application/"]
COPY ["StudyWire.Domain/StudyWire.Domain.csproj", "StudyWire.Domain/"]
COPY ["StudyWire.Infrastructure/StudyWire.Infrastructure.csproj", "StudyWire.Infrastructure/"]
RUN dotnet restore "StudyWire.API/StudyWire.API.csproj"
COPY . .
WORKDIR "/src/StudyWire.API"
RUN dotnet build "StudyWire.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "StudyWire.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StudyWire.API.dll"]