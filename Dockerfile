#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Checkout.PaymentGateway.Api/Checkout.PaymentGateway.Api.csproj", "Checkout.PaymentGateway.Api/"]
COPY ["PaymentGateway.Service/Checkout.PaymentGateway.Application.csproj", "PaymentGateway.Service/"]
COPY ["PaymentGateway.Domain/Checkout.PaymentGateway.Domain.csproj", "PaymentGateway.Domain/"]
COPY ["PaymentGateway.Helper/Checkout.PaymentGateway.Helper.csproj", "PaymentGateway.Helper/"]
COPY ["Bank.PaymentProcessor/Bank.PaymentProcessor.csproj", "Bank.PaymentProcessor/"]
COPY ["PaymentGateway.Infrastructure/Checkout.PaymentGateway.Infrastructure.csproj", "PaymentGateway.Infrastructure/"]
RUN dotnet restore "Checkout.PaymentGateway.Api/Checkout.PaymentGateway.Api.csproj"
COPY . .
WORKDIR "/src/Checkout.PaymentGateway.Api"
RUN dotnet build "Checkout.PaymentGateway.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Checkout.PaymentGateway.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Checkout.PaymentGateway.Api.dll"]