FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# Install Node.js
RUN apt-get update && \
apt-get install -y wget && \
apt-get install -y gnupg2 && \
wget -qO- https://deb.nodesource.com/setup_16.x | bash - && \
apt-get install -y build-essential nodejs
# End Install

ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Cint.CodingChallenge.Web/Cint.CodingChallenge.Web.csproj", "src/Cint.CodingChallenge.Web/"]
COPY ["src/Cint.CodingChallenge.Data/Cint.CodingChallenge.Data.csproj", "src/Cint.CodingChallenge.Data/"]
COPY ["src/Cint.CodingChallenge.Model/Cint.CodingChallenge.Model.csproj", "src/Cint.CodingChallenge.Model/"]
COPY ["src/Cint.CodingChallenge.Business/Cint.CodingChallenge.Business.csproj", "src/Cint.CodingChallenge.Business/"]
RUN dotnet restore "src/Cint.CodingChallenge.Web/Cint.CodingChallenge.Web.csproj"
COPY . .
WORKDIR "/src/src/Cint.CodingChallenge.Web/app"
RUN npm install
RUN npm run build
WORKDIR "/src/src/Cint.CodingChallenge.Web"
RUN dotnet build "Cint.CodingChallenge.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Cint.CodingChallenge.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Cint.CodingChallenge.Web.dll"]
