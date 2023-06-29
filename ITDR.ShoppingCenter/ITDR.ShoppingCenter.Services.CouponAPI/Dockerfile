FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ITDR.ShoppingCenter.Services.CouponAPI/ITDR.ShoppingCenter.Services.CouponAPI.csproj", "ITDR.ShoppingCenter.Services.CouponAPI/"]
RUN dotnet restore "ITDR.ShoppingCenter.Services.CouponAPI/ITDR.ShoppingCenter.Services.CouponAPI.csproj"
COPY . .
WORKDIR "/src/ITDR.ShoppingCenter.Services.CouponAPI"
RUN dotnet build "ITDR.ShoppingCenter.Services.CouponAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ITDR.ShoppingCenter.Services.CouponAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ITDR.ShoppingCenter.Services.CouponAPI.dll"]
