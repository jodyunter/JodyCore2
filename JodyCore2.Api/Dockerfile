#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["JodyCore2.Api/JodyCore2.Api.csproj", "JodyCore2.Api/"]
COPY ["JodyCore2.Service/JodyCore2.Service.csproj", "JodyCore2.Service/"]
COPY ["JodyCore2.Data/JodyCore2.Data.csproj", "JodyCore2.Data/"]
COPY ["JodyCore2.Domain/JodyCore2.Domain.csproj", "JodyCore2.Domain/"]
RUN dotnet restore "JodyCore2.Api/JodyCore2.Api.csproj"
COPY . .
WORKDIR "/src/JodyCore2.Api"
RUN dotnet build "JodyCore2.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "JodyCore2.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "JodyCore2.Api.dll"]