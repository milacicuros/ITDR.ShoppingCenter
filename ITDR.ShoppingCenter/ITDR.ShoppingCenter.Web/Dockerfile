FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ITDR.ShoppingCenter.Web/ITDR.ShoppingCenter.Web.csproj", "ITDR.ShoppingCenter.Web/"]
RUN dotnet restore "ITDR.ShoppingCenter.Web/ITDR.ShoppingCenter.Web.csproj"
COPY . .
WORKDIR "/src/ITDR.ShoppingCenter.Web"
RUN dotnet build "ITDR.ShoppingCenter.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ITDR.ShoppingCenter.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ITDR.ShoppingCenter.Web.dll"]
