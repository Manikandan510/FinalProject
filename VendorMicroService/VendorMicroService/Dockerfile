FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["VendorMicroService/VendorMicroService.csproj", "VendorMicroService/"]
RUN dotnet restore "VendorMicroService/VendorMicroService.csproj"
COPY . .
WORKDIR "/src/VendorMicroService"
RUN dotnet build "VendorMicroService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "VendorMicroService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VendorMicroService.dll"]
