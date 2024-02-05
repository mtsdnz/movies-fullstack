# Step 1: Build the React application
# Use an official Node.js runtime as a parent image
FROM node:lts as build

# Set the working directory in the container
WORKDIR /app

# Copy package.json and package-lock.json (or yarn.lock) files
COPY package*.json ./

# Install any needed packages
RUN npm install

# Copy the rest of the application's code
COPY . .

# Build the app for production
RUN npm run build

# Step 2: Serve the application using Nginx
# Use an official Nginx runtime as a parent image for the serving stage
FROM nginx:alpine
COPY nginx.conf /etc/nginx/conf.d/default.conf

# Copy the build output to replace the default Nginx contents.
COPY --from=build /app/build /usr/share/nginx/html

# Expose port 80 to the outside world
EXPOSE 80

# Start Nginx when the container has provisioned.
CMD ["nginx", "-g", "daemon off;"]
