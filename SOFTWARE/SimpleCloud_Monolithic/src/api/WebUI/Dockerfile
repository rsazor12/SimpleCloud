#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/api/WebUI/WebUI.csproj", "src/api/WebUI/"]
COPY ["src/api/Application/Application.csproj", "src/api/Application/"]
COPY ["src/api/Domain/Domain.csproj", "src/api/Domain/"]
COPY ["src/api/Infrastructure/Infrastructure.csproj", "src/api/Infrastructure/"]
RUN dotnet restore "src/api/WebUI/WebUI.csproj"
COPY . .
WORKDIR "/src/src/api/WebUI"
RUN dotnet build "WebUI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebUI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SimpleCloud_Monolithic.WebUI.dll"]