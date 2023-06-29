FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ITDR.ShoppingCenter.Services.Identity/ITDR.ShoppingCenter.Services.Identity.csproj", "ITDR.ShoppingCenter.Services.Identity/"]
RUN dotnet restore "ITDR.ShoppingCenter.Services.Identity/ITDR.ShoppingCenter.Services.Identity.csproj"
COPY . .
WORKDIR "/src/ITDR.ShoppingCenter.Services.Identity"
RUN dotnet build "ITDR.ShoppingCenter.Services.Identity.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ITDR.ShoppingCenter.Services.Identity.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ITDR.ShoppingCenter.Services.Identity.dll"]
