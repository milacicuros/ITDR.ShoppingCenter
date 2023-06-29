FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ITDR.ShoppingCenter.Services.ShoppingCartAPI/ITDR.ShoppingCenter.Services.ShoppingCartAPI.csproj", "ITDR.ShoppingCenter.Services.ShoppingCartAPI/"]
RUN dotnet restore "ITDR.ShoppingCenter.Services.ShoppingCartAPI/ITDR.ShoppingCenter.Services.ShoppingCartAPI.csproj"
COPY . .
WORKDIR "/src/ITDR.ShoppingCenter.Services.ShoppingCartAPI"
RUN dotnet build "ITDR.ShoppingCenter.Services.ShoppingCartAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ITDR.ShoppingCenter.Services.ShoppingCartAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ITDR.ShoppingCenter.Services.ShoppingCartAPI.dll"]
