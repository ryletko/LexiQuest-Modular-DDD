ARG IMAGEVERSION
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/aspnet:8.0-bookworm-slim${IMAGEVERSION} AS base
ARG TARGETARCH
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG TARGETARCH
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["LexiQuest.Framework/LexiQuest.Framework.Application/LexiQuest.Framework.Application.csproj", "LexiQuest.Framework/LexiQuest.Framework.Application/"]
COPY ["LexiQuest.Framework/LexiQuest.Framework.Domain/LexiQuest.Framework.Domain.csproj", "LexiQuest.Framework/LexiQuest.Framework.Domain/"]
COPY ["LexiQuest.Framework/LexiQuest.Framework.Infrastructure/LexiQuest.Framework.Infrastructure.csproj", "LexiQuest.Framework/LexiQuest.Framework.Infrastructure/"]
COPY ["LexiQuest.Framework/LexiQuest.Framework.Module/LexiQuest.Framework.Module.csproj", "LexiQuest.Framework/LexiQuest.Framework.Module/"]

COPY ["LexiQuest.Import/LexiQuest.Import.GoogleSheets/LexiQuest.Import.GoogleSheets.csproj", "LexiQuest.Import/LexiQuest.Import.GoogleSheets/"]
COPY ["LexiQuest.Import/LexiQuest.Import.GoogleSheets.Contracts/LexiQuest.Import.GoogleSheets.Contracts.csproj", "LexiQuest.Import/LexiQuest.Import.GoogleSheets.Contracts/"]

COPY ["LexiQuest.PuzzleMgr/LexiQuest.PuzzleMgr.Application/LexiQuest.PuzzleMgr.Application.csproj", "LexiQuest.PuzzleMgr/LexiQuest.PuzzleMgr.Application/"]
COPY ["LexiQuest.PuzzleMgr/LexiQuest.PuzzleMgr.Application/LexiQuest.PuzzleMgr.Application.csproj", "LexiQuest.PuzzleMgr/LexiQuest.PuzzleMgr.Application/"]
COPY ["LexiQuest.PuzzleMgr/LexiQuest.PuzzleMgr.Domain/LexiQuest.PuzzleMgr.Domain.csproj", "LexiQuest.PuzzleMgr/LexiQuest.PuzzleMgr.Domain/"]
COPY ["LexiQuest.PuzzleMgr/.Infrastructure/LexiQuest.PuzzleMgr.Infrastructure.csproj", "LexiQuest.PuzzleMgr/LexiQuest.PuzzleMgr.Infrastructure/"]

COPY ["LexiQuest.QuizGame/LexiQuest.QuizGame.Application/LexiQuest.QuizGame.Application.csproj", "LexiQuest.QuizGame/LexiQuest.QuizGame.Application/"]
COPY ["LexiQuest.QuizGame/LexiQuest.QuizGame.Contracts/LexiQuest.QuizGame.Contracts.csproj", "LexiQuest.QuizGame/LexiQuest.QuizGame.Contracts/"]
COPY ["LexiQuest.QuizGame/LexiQuest.QuizGame.Domain/LexiQuest.QuizGame.Domain.csproj", "LexiQuest.QuizGame/LexiQuest.QuizGame.Domain/"]
COPY ["LexiQuest.QuizGame/LexiQuest.QuizGame.Infrastructure/LexiQuest.QuizGame.Infrastructure.csproj", "LexiQuest.QuizGame/LexiQuest.QuizGame.Infrastructure/"]

COPY ["LexiQuest.Shared.Puzzle/LexiQuest.Shared.Puzzle.csproj", "LexiQuest.Shared.Puzzle/"]
COPY ["LexiQuest.Shared.QuizGame/LexiQuest.Shared.QuizGame.csproj", "LexiQuest.Shared.QuizGame/"]

COPY ["Utils.Core/Utils.Core.csproj", "Utils.Core/"]
COPY ["Utils.EfCore/Utils.EfCore.csproj", "Utils.EfCore/"]

COPY ["LexiQuest.Module/LexiQuest.Module.csproj", "LexiQuest.Module/"]
COPY ["LexiQuest.WebApp/LexiQuest.WebApp/LexiQuest.WebApp.csproj", "LexiQuest.WebApp/LexiQuest.WebApp/"]
COPY ["LexiQuest.WebApp/LexiQuest.WebApp.Client/LexiQuest.WebApp.Client.csproj", "LexiQuest.WebApp/LexiQuest.WebApp.Client/"]
COPY ["LexiQuest.WebApp/LexiQuest.WebApp.Shared/LexiQuest.WebApp.Shared.csproj", "LexiQuest.WebApp/LexiQuest.WebApp.Shared/"]

RUN echo $TARGETARCH
RUN dotnet restore "LexiQuest.WebApp/LexiQuest.WebApp/LexiQuest.WebApp.csproj" -a $TARGETARCH
COPY . .
WORKDIR "/src/LexiQuest.WebApp/LexiQuest.WebApp"
RUN dotnet build "LexiQuest.WebApp.csproj" -c $BUILD_CONFIGURATION -o /app/build -a $TARGETARCH

FROM build AS publish
ARG TARGETPLATFORM
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "LexiQuest.WebApp.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false -a $TARGETARCH

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LexiQuest.WebApp.dll"]
