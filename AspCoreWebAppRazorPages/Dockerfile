#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

#FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
#WORKDIR /app
#EXPOSE 80
#EXPOSE 443

#FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
#ARG BUILD_CONFIGURATION=Release
#WORKDIR /src
#COPY ["AspCoreWebAppRazorPages/AspCoreWebAppRazorPages.csproj", "AspCoreWebAppRazorPages/"]
#COPY ["TicTacToe.Distribution/TicTacToe.Distribution.csproj", "TicTacToe.Distribution/"]
#RUN dotnet restore "./AspCoreWebAppRazorPages/AspCoreWebAppRazorPages.csproj"
#COPY . .
#WORKDIR "/src/AspCoreWebAppRazorPages"
#RUN dotnet build "./AspCoreWebAppRazorPages.csproj" -c $BUILD_CONFIGURATION -o /app/build

#FROM build AS publish
#ARG BUILD_CONFIGURATION=Release
#RUN dotnet publish "./AspCoreWebAppRazorPages.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "AspCoreWebAppRazorPages.dll"]

#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS publishstage
ARG BUILD_CONFIGURATION=${BUILD_CONFIGURATION}
WORKDIR /src
COPY ["AspCoreWebAppRazorPages/AspCoreWebAppRazorPages.csproj", "AspCoreWebAppRazorPages/"]
COPY ["TicTacToe.Distribution/TicTacToe.Distribution.csproj", "TicTacToe.Distribution/"]
RUN dotnet restore "./AspCoreWebAppRazorPages/./AspCoreWebAppRazorPages.csproj"
COPY . .
WORKDIR "/src/AspCoreWebAppRazorPages"
RUN dotnet build "AspCoreWebAppRazorPages.csproj" -c $BUILD_CONFIGURATION -o /build/build
RUN dotnet publish "AspCoreWebAppRazorPages.csproj" -c $BUILD_CONFIGURATION -o /build/publish /p:UseAppHost=true
#RUN dotnet publish -p:PublishProfile=FolderProfile$BUILD_CONFIGURATION

FROM base AS finalstage
ARG BUILD_CONFIGURATION=${BUILD_CONFIGURATION}
EXPOSE 80
EXPOSE 443
WORKDIR /app
COPY --from=publishstage /build/publish/. /app
ENTRYPOINT "/app/AspCoreWebAppRazorPages"
#COPY --from=publishstage /src/AspCoreWebAppRazorPages/bin/$BUILD_CONFIGURATION/net6.0/publish /app
#ENTRYPOINT ["dotnet", "AspCoreWebAppRazorPages.dll"]