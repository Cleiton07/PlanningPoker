#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Application/PlanningPoker.Application.API/PlanningPoker.Application.API.csproj", "src/Application/PlanningPoker.Application.API/"]
RUN dotnet restore "src/Application/PlanningPoker.Application.API/PlanningPoker.Application.API.csproj"
COPY . .
WORKDIR "/src/src/Application/PlanningPoker.Application.API"
RUN dotnet build "PlanningPoker.Application.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PlanningPoker.Application.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PlanningPoker.Application.API.dll"]