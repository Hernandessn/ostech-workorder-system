FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /src

COPY OSTech.WebAPI/OSTech.WebAPI.csproj OSTech.WebAPI/
COPY OSTech.Application/OSTech.Application.csproj OSTech.Application/
COPY OSTech.EFCore/OSTech.Infrastructure.csproj OSTech.EFCore/
COPY OSTech.Domain/OSTech.Domain.csproj OSTech.Domain/

RUN dotnet restore OSTech.WebAPI/OSTech.WebAPI.csproj

COPY . .

RUN dotnet publish OSTech.WebAPI/OSTech.WebAPI.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine

WORKDIR /app

ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
ENV ASPNETCORE_HTTP_PORTS=8080

EXPOSE 8080

RUN apk add --no-cache icu-libs

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "OSTech.WebAPI.dll"]