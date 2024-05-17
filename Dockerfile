FROM node:14.15.1-alpine as frontend_build
WORKDIR /app
COPY /frontend/package.json package.json
COPY /frontend/package-lock.json package-lock.json
RUN npm install --silent
COPY /frontend/ .
RUN npm run build

FROM mcr.microsoft.com/dotnet/sdk:6.0 as backend_build
WORKDIR /app
COPY /backend/*.sln ./
COPY /backend/backend/*.csproj ./backend/
RUN dotnet restore

COPY /backend/backend ./backend/
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0 as final_image
COPY --from=backend_build /app/out /frdb
COPY --from=frontend_build /app/build /frdb/wwwroot
WORKDIR /frdb
EXPOSE 80
ENTRYPOINT ["dotnet", "backend.dll"]