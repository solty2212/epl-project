# Використовуємо офіційний .NET 8 SDK для білду
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Копіюємо файл рішення та проєкт для кешування залежностей
COPY src/EPLTeamSquadDataApi.sln ./
COPY src/EPLTeamSquadDataApi/*.csproj ./EPLTeamSquadDataApi/

# Відновлюємо залежності
RUN dotnet restore EPLTeamSquadDataApi/*.csproj

# Копіюємо решту коду
COPY src/. ./

# Білдимо застосунок
RUN dotnet publish EPLTeamSquadDataApi/*.csproj -c Release -o out

# Використовуємо легкий .NET 8 runtime для запуску
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

# Відкриваємо порт
EXPOSE 5000

# Запускаємо API
CMD ["dotnet", "EPLTeamSquadDataApi.dll"]
