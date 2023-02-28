FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Rich.BookMark.Blazor/Rich.BookMark.Blazor.csproj", "src/Rich.BookMark.Blazor/"]
COPY ["src/Rich.BookMark.Application/Rich.BookMark.Application.csproj", "src/Rich.BookMark.Application/"]
COPY ["src/Rich.BookMark.Domain/Rich.BookMark.Domain.csproj", "src/Rich.BookMark.Domain/"]
COPY ["src/Rich.BookMark.Domain.Shared/Rich.BookMark.Domain.Shared.csproj", "src/Rich.BookMark.Domain.Shared/"]
COPY ["src/Rich.BookMark.Application.Contracts/Rich.BookMark.Application.Contracts.csproj", "src/Rich.BookMark.Application.Contracts/"]
COPY ["src/Rich.BookMark.HttpApi/Rich.BookMark.HttpApi.csproj", "src/Rich.BookMark.HttpApi/"]
COPY ["src/Rich.BookMark.EntityFrameworkCore/Rich.BookMark.EntityFrameworkCore.csproj", "src/Rich.BookMark.EntityFrameworkCore/"]
RUN dotnet restore "src/Rich.BookMark.Blazor/Rich.BookMark.Blazor.csproj"
COPY . .
WORKDIR "/src/src/Rich.BookMark.Blazor"
RUN dotnet build "Rich.BookMark.Blazor.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Rich.BookMark.Blazor.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Rich.BookMark.Blazor.dll"]
