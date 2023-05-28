FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine AS base

RUN apk update && apk add libgdiplus
RUN apk add ttf-freefont && apk add libssl1.1
RUN apk update; apk add fontconfig ttf-liberation
RUN fc-cache -f -v

WORKDIR /app
RUN apk add --no-cache icu-libs
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["PracticeAPI/*.cs", "PracticeAPI/"]
COPY ["PracticeAPI/*.csproj", "PracticeAPI/"]

RUN dotnet restore "PracticeAPI/PracticeAPI.csproj"
COPY . .
WORKDIR "/src/PracticeAPI"
RUN dotnet build "PracticeAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PracticeAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PracticeAPI.dll"]