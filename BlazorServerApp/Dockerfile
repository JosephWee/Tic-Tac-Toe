#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

#FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
#WORKDIR /app
#EXPOSE 80
#EXPOSE 443

#FROM mcr.microsoft.com/dotnet/sdk:6.0 AS buildstage
#ARG BUILD_CONFIGURATION=Release
#WORKDIR /src
#COPY ["BlazorServerApp/BlazorServerApp.csproj", "BlazorServerApp/"]
#COPY ["TicTacToe.Distribution/TicTacToe.Distribution.csproj", "TicTacToe.Distribution/"]
#RUN dotnet restore "./BlazorServerApp/./BlazorServerApp.csproj"
#COPY . .
#WORKDIR "/src/BlazorServerApp"
#RUN dotnet build "./BlazorServerApp.csproj" -c $BUILD_CONFIGURATION -o /app/build

#FROM buildstage AS publishstage
#ARG BUILD_CONFIGURATION=Release
#RUN dotnet publish "./BlazorServerApp.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

#FROM base AS finalstage
#WORKDIR /app
#COPY --from=publishstage /app/publish .
#ENTRYPOINT ["dotnet", "BlazorServerApp.dll"]

#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS publishstage
ARG BUILD_CONFIGURATION=Debug
WORKDIR /src
COPY ["BlazorServerApp/BlazorServerApp.csproj", "BlazorServerApp/"]
COPY ["TicTacToe.Distribution/TicTacToe.Distribution.csproj", "TicTacToe.Distribution/"]
RUN dotnet restore "./BlazorServerApp/./BlazorServerApp.csproj"
COPY . .
WORKDIR "/src/BlazorServerApp"
RUN dotnet build "./BlazorServerApp.csproj" -c $BUILD_CONFIGURATION -o /build/build
#RUN dotnet publish "./BlazorServerApp.csproj" -c $BUILD_CONFIGURATION -o /build/publish /p:UseAppHost=false
RUN dotnet publish -p:PublishProfile=FolderProfile

FROM base AS finalstage
ARG BUILD_CONFIGURATION=Debug
EXPOSE 8080
EXPOSE 8443
WORKDIR /app
COPY --from=publishstage /src/BlazorServerApp/bin/$BUILD_CONFIGURATION/net6.0/publish /app
#ENTRYPOINT ["dotnet", "BlazorServerApp.dll"]
ENTRYPOINT "/app/BlazorServerApp"