FROM node:8-alpine
WORKDIR /app

# Copy compiled js
COPY dist/* ./
RUN dotnet restore

ENTRYPOINT ["node", "main.js"]
