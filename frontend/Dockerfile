# Stage 1: Build React app
FROM node:20-alpine AS build
WORKDIR /app

# Copy dependency files first
COPY package*.json ./

# Install dependencies
RUN npm ci

# Copy all source files
COPY . .

# Build React app for production
RUN npm run build

# Verify build output exists (debug aid)
RUN ls -lah /app/build

# Stage 2: Serve using Nginx
FROM nginx:alpine
# Copy build output from previous stage
COPY --from=build /app/build /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
