#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/Services/Payment/src/Payment.Api/Payment.Api.csproj", "src/Services/Payment/src/Payment.Api/"]
COPY ["src/Services/Payment/src/Application/Application.csproj", "src/Services/Payment/src/Application/"]
COPY ["src/Services/Payment/src/Domain/Domain.csproj", "src/Services/Payment/src/Domain/"]
COPY ["src/Services/Payment/src/Infrastructure/Infrastructure.csproj", "src/Services/Payment/src/Infrastructure/"]
RUN dotnet restore "src/Services/Payment/src/Payment.Api/Payment.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/Payment/src/Payment.Api"
RUN dotnet build "Payment.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Payment.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Payment_SimpleCloud_MicroservicesMessageBroker.WebUI.dll"]
