#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["BG.CampusLife.Presentation/BG.CampusLife.Presentation.csproj", "BG.CampusLife.Presentation/"]
COPY ["BG.CampusLife.Domain/BG.CampusLife.Domain.csproj", "BG.CampusLife.Domain/"]
COPY ["BG.CampusLife.Infrastructure/BG.CampusLife.Infrastructure.csproj", "BG.CampusLife.Infrastructure/"]
COPY ["BG.CampusLife.SharedKernel/BG.CampusLife.SharedKernel.csproj", "BG.CampusLife.SharedKernel/"]
COPY ["BG.CampusLife.Application/BG.CampusLife.Application.csproj", "BG.CampusLife.Application/"]
RUN dotnet restore "BG.CampusLife.Presentation/BG.CampusLife.Presentation.csproj"
COPY . .
WORKDIR "/src/BG.CampusLife.Presentation"
RUN dotnet build "BG.CampusLife.Presentation.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BG.CampusLife.Presentation.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BG.CampusLife.Presentation.dll"]