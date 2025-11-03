# 📚 BookLendingService API

A modular ASP.NET Core Web API for managing book lending operations. Built with a multi-project architecture and containerized using Docker for streamlined deployment.


![.NET Version](https://img.shields.io/badge/.NET-8.0-blue)
![Build Status](https://github.com/yuvraj1165/BookLendingService/actions/workflows/ci-sonar.yml/badge.svg)






---

## 🚀 Features

- Multi-project solution with layered architecture
- Dockerized with multi-stage builds
- Swagger UI for API exploration
- CI/CD-ready with SonarCloud and code coverage integration

---

## 🛠️ Tech Stack

- .NET 8.0
- ASP.NET Core Web API
- Docker & Docker Desktop
- Swagger (Swashbuckle)
- SonarCloud
- GitHub Actions 

---


## 🐳 Running the API with Docker

### 1. **Build the Docker Image**
Use Docker BuildKit to build and tag the image:
```bash
docker buildx build . --tag booklendingservice --load
```
### 2. Run the Container

Start the API container and map it to a host port:

```bash
docker run -d -p 8080:80 --name booklending-api booklendingservice
```
- -d: Runs the container in detached mode (background)
- -p 8080:80: Maps port 80 inside the container to port 8080 on your machine
- --name booklending-api: Names the container for easier reference

### 3. Access the API
Once running, access the API at: http://localhost:8080
