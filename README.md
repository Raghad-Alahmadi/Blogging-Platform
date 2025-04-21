# Blogging Platform Microservices

A containerized microservices application for managing blog posts and comments using .NET 8.

## Table of Contents
- [Overview](#overview)
- [Prerequisites](#prerequisites)
- [Getting Started](#getting-started)
- [API Documentation](#api-documentation)
- [Environment Variables](#environment-variables)
- [Testing](#testing)
- [Containerization](#containerization)
- [Future Enhancements](#future-enhancements)

## Overview

This Blogging Platform demonstrates a microservices architecture with two services:
1. **PostService**: Manages blog posts creation and retrieval
2. **CommentService**: Handles comments for blog posts with cross-service validation

The application implements a basic blogging system where users can create posts and add comments to existing posts.


### Components:
- **Blogging Platform.DTO**: Shared class library containing DTOs
- **PostService**: Web API for post management
- **CommentService**: Web API for comment management
- **Docker**: Container orchestration

The services communicate via HTTP and use Docker networking for service discovery.

## Prerequisites

- .NET 8 SDK
- Docker Desktop
- Postman (or any API testing tool)

## Getting Started

### Option 1: Using Docker Compose (Recommended)
1. Clone the repository
2. Navigate to the solution folder
3. Run Docker Compose:
   ```
   docker-compose up --build
   ```
4. Access the services:
   - Posts: http://localhost:6001/posts
   - Comments: http://localhost:6002/comments

### Option 2: Running Services Locally
1. Start the PostService:
   ```
   cd PostService
   dotnet run
   ```
2. Start the CommentService:
   ```
   cd CommentService
   dotnet run
   ```

## API Documentation

### PostService (http://localhost:6001)

| Endpoint | Method | Description |
|----------|--------|-------------|
| /posts | GET | Retrieve all posts |
| /posts | POST | Create a new post |

#### Create Post Request Body
```json
{
  "title": "Getting Started with .NET",
  "content": "This is a sample blog post."
}
```

#### Post Response
```json
{
  "id": "GUID_HERE",
  "title": "Getting Started with .NET",
  "content": "This is a sample blog post."
}
```

### CommentService (http://localhost:6002)

| Endpoint | Method | Description |
|----------|--------|-------------|
| /comments/{postId} | GET | Retrieve all comments for a post |
| /comments | POST | Add a comment to a post |

#### Create Comment Request Body
```json
{
  "postId": "GUID_HERE",
  "author": "Jane Doe",
  "text": "Great article!"
}
```

#### Comment Response
```json
{
  "postId": "GUID_HERE",
  "author": "Jane Doe",
  "text": "Great article!"
}
```

## Environment Variables

### PostService
- `ASPNETCORE_ENVIRONMENT`: Application environment (Development, Production)
- `ASPNETCORE_URLS`: URL(s) the service listens on

### CommentService
- `ASPNETCORE_ENVIRONMENT`: Application environment (Development, Production)
- `ASPNETCORE_URLS`: URL(s) the service listens on
- `POST_SERVICE_URL`: URL of the PostService for cross-service validation

## Testing

### Using Postman
1. Create a post:
   - Send a POST request to http://localhost:6001/posts with the post data
2. Get all posts:
   - Send a GET request to http://localhost:6001/posts
3. Add a comment:
   - Send a POST request to http://localhost:6002/comments with the comment data and post ID
4. Get comments for a post:
   - Send a GET request to http://localhost:6002/comments/{postId}

### Using curl

```bash
# Create a post
curl -X POST http://localhost:6001/posts \
  -H "Content-Type: application/json" \
  -d '{"title":"Getting Started with .NET","content":"This is a sample blog post."}'

# Get all posts
curl -X GET http://localhost:6001/posts

# Add a comment (replace GUID_HERE with actual post ID)
curl -X POST http://localhost:6002/comments \
  -H "Content-Type: application/json" \
  -d '{"postId":"GUID_HERE","author":"Jane Doe","text":"Great article!"}'

# Get comments for a post
curl -X GET http://localhost:6002/comments/GUID_HERE
```

## Containerization

The application is containerized using Docker:
- Each service has its own Dockerfile
- Docker Compose orchestrates the services
- Services communicate over a Docker network
- Environment variables pass configuration between services

## Future Enhancements

- Persistent data storage (replacing in-memory repositories)
- Authentication and authorization
- Pagination for large result sets
- Swagger/OpenAPI documentation
- Monitoring and logging
- Automated testing
- CI/CD pipeline setup
