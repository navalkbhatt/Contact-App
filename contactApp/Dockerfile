# Stage 1: Build the Angular app
FROM node:16 AS build

# Set the working directory
WORKDIR /app

# Copy package.json and package-lock.json files to the working directory
COPY package*.json ./

# Install dependencies
RUN npm install

# Copy the rest of the application files
COPY . .

# Build the Angular application in production mode
RUN npm run build --prod

# Stage 2: Serve the application with Nginx
FROM nginx:alpine

# Copy the built files from the previous stage to Nginx's HTML directory
COPY --from=build /app/dist /usr/share/nginx/html
# Copy your custom Nginx configuration file

# Copy custom Nginx configuration if needed (optional)
COPY nginx.conf /etc/nginx/nginx.conf

# Expose port 80
EXPOSE 80

# Start Nginx server
CMD ["nginx", "-g", "daemon off;"]
 