#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS publishstage
ARG BUILD_CONFIGURATION=${BUILD_CONFIGURATION}
WORKDIR /src
COPY ["WebAPI/WebAPI.csproj", "WebAPI/"]
COPY ["TicTacToe.Distribution/TicTacToe.Distribution.csproj", "TicTacToe.Distribution/"]
COPY ["TicTacToeBL/TicTacToeBL.csproj", "TicTacToeBL/"]
COPY ["TicTacToeML/TicTacToeML.csproj", "TicTacToeML/"]
RUN dotnet restore "./WebAPI/./WebAPI.csproj"
COPY . .
WORKDIR "/src/WebAPI"
RUN dotnet build "./WebAPI.csproj" -c $BUILD_CONFIGURATION -o /build/build
RUN dotnet publish "./WebAPI.csproj" -c $BUILD_CONFIGURATION -o /build/publish /p:UseAppHost=false

FROM base AS finalstage
EXPOSE 80
EXPOSE 443
WORKDIR /app
COPY --from=publishstage /build/publish/. /app
ENTRYPOINT ["dotnet", "WebAPI.dll"]