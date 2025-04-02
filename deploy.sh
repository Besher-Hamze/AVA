#!/bin/bash

# Stop on error
set -e

echo "====== Starting deployment process ======"

# Change to the project directory
cd /root/AVA

# Pull the latest changes from Git
echo "Pulling latest changes from Git..."
git pull origin master

# Build the .NET application in Release mode
echo "Building the application..."
dotnet publish -c Release

# Stop and remove the existing container if it exists
echo "Stopping and removing existing container..."
docker stop ava-container || true
docker rm ava-container || true

# Build a new Docker image
echo "Building new Docker image..."
docker build -t ava-app .

# Run the new container
echo "Starting new container..."
docker run -d -p 8080:8080 --name ava-container --add-host=host.docker.internal:host-gateway ava-app

# Check if container is running
echo "Verifying deployment..."
if docker ps | grep -q ava-container; then
    echo "✅ Deployment successful! Application is running at http://5.189.185.195:8080"
    echo "Swagger UI available at http://5.189.185.195:8080/swagger/index.html"
else
    echo "❌ Deployment failed. Check logs with: docker logs ava-container"
fi

echo "====== Deployment process completed ======"