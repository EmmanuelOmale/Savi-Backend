#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR "/Savi.Api"
COPY ["Savi.Api/Savi.Api.csproj", "Savi.Api/"]
#COPY ["Savi.Core/Savi.Core.csproj", "Savi.Core/"]
#COPY ["Savi.Data/Savi.Data.csproj", "Savi.Data/"]
RUN dotnet restore "Savi.Api/Savi.Api.csproj"
COPY . .
WORKDIR "/savi.Api/Savi.Api"
RUN dotnet build "Savi.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Savi.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR "/Savi.Api/Savi.Api"
COPY --from=publish /app/publish .
CMD ASPNETCORE_URLS=http://*:$PORT dotnet Savi.Api.dll
#ENTRYPOINT ["dotnet", "Savi.Api.dll"]