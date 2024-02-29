#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS publishstage
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["WebApi/WebApi.csproj", "WebApi/"]
COPY ["TicTacToe.Distribution/TicTacToe.Distribution.csproj", "TicTacToe.Distribution/"]
COPY ["TicTacToeBL/TicTacToeBL.csproj", "TicTacToeBL/"]
COPY ["TicTacToeML/TicTacToeML.csproj", "TicTacToeML/"]
RUN dotnet restore "./WebApi/./WebApi.csproj"
COPY . .
WORKDIR "/src/WebApi"
RUN dotnet build "./WebApi.csproj" -c $BUILD_CONFIGURATION -o /build/build
RUN dotnet publish "./WebApi.csproj" -c $BUILD_CONFIGURATION -o /build/publish /p:UseAppHost=false

FROM base AS finalstage
EXPOSE 8080
EXPOSE 8443
WORKDIR /app
COPY --from=publishstage /build/publish /app
ENTRYPOINT ["dotnet", "WebApi.dll"]