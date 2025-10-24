# Development Dockerfile for EazyPay frontend
FROM node:20-alpine
WORKDIR /app

# Copy dependency files

# Install dependencies
RUN npm install

# Copy all source files
COPY . .

# Expose the dev port (usually 5173 for Vite or 3000 for CRA)
EXPOSE 5173

# Run the development server
CMD ["npm", "run", "dev"]
