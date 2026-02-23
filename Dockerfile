FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copiamos los archivos de proyecto
COPY ["gestionDeTareas/gestionDeTareas.csproj", "gestionDeTareas/"]
COPY ["GestionDeTareasBlazor/GestionDeTareasBlazor.csproj", "GestionDeTareasBlazor/"]

# Restauramos
RUN dotnet restore "gestionDeTareas/gestionDeTareas.csproj"

# Copiamos todo y publicamos el Backend
COPY . .
WORKDIR "/src/gestionDeTareas"
RUN dotnet publish "gestionDeTareas.csproj" -c Release -o /app/publish

# Imagen final
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app/publish .
COPY --from=build /src/GestionDeTareasBlazor/wwwroot ./wwwroot

ENV PORT=8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "gestionDeTareas.dll"]
