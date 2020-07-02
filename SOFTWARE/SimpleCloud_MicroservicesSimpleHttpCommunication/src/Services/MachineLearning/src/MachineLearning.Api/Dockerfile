#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/Services/MachineLearning/src/MachineLearning.Api/MachineLearning.Api.csproj", "src/Services/MachineLearning/src/MachineLearning.Api/"]
COPY ["src/Services/MachineLearning/src/Application/Application.csproj", "src/Services/MachineLearning/src/Application/"]
COPY ["src/Services/MachineLearning/src/Domain/Domain.csproj", "src/Services/MachineLearning/src/Domain/"]
COPY ["src/Services/MachineLearning/src/Infrastructure/Infrastructure.csproj", "src/Services/MachineLearning/src/Infrastructure/"]
RUN dotnet restore "src/Services/MachineLearning/src/MachineLearning.Api/MachineLearning.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/MachineLearning/src/MachineLearning.Api"
RUN dotnet build "MachineLearning.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MachineLearning.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SimpleCloud_Monolithic.WebUI.dll"]
