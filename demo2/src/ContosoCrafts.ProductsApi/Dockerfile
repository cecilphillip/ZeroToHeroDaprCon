FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
ARG USERNAME=wutang
ARG USER_UID=1000
ARG USER_GID=$USER_UID

# Create the user
RUN groupadd --gid $USER_GID $USERNAME \
    && useradd --uid $USER_UID --gid $USER_GID -m $USERNAME

ENV ASPNETCORE_URLS=http://+:80;

EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ContosoCrafts.ProductsApi.csproj", "./"]
RUN dotnet restore "./ContosoCrafts.ProductsApi.csproj"
COPY . .
WORKDIR /src
RUN dotnet build "ContosoCrafts.ProductsApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ContosoCrafts.ProductsApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
USER $USERNAME
ENTRYPOINT ["dotnet", "ContosoCrafts.ProductsApi.dll"]
