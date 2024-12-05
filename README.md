This application was developed for Web Application module, as coursework portfolio project @ WIUT by student ID: 00017102

# Simple Blog Application

This application is a simple blogging platform built using **ASP.NET Core Web API** for the backend and **Angular** for the frontend. It allows the management of blog posts, categories, and anonymous comments. Posts are automatically reassigned to a default category if their original category is deleted.

---

## Table of Contents
1. [Features](#features)
2. [Technologies Used](#technologies-used)
3. [Architecture](#architecture)
4. [API Endpoints](#api-endpoints)

---

## Features
- Create, read, update, and delete (CRUD) blog posts.
- Manage categories and reassign posts to a default category when a category is deleted.
- Anonymous users can leave comments on posts.
- Responsive design for an enhanced user experience.

---

## Technologies Used
### Backend
- **ASP.NET Core Web API**
- **Entity Framework Core** (Code-First Approach)
- **SQL Server** as the database
- **Swagger** for API documentation

### Frontend
- **Angular** (Single Page Application)
- **Bootstrap** for responsive UI design
- **RxJS** for handling API calls and reactive programming

---
## Architecture

### Backend
- **Controller-Service-Repository Pattern**:
  - **Controllers**: Handle HTTP requests and return responses.
  - **Repositories**: Encapsulate database access logic.
  - **Services** (optional): Manage business logic.
- **Entities**:
  - `Post`: Represents a blog post.
  - `Category`: Represents a category to which posts belong.
  - `Comment`: Represents anonymous comments on a post.

### Frontend
- **Component-Based Structure**:
  - **Components**: Handle posts, categories, and comments.
  - **Shared Services**: Manage API interactions.
- **State Management**:
  - Uses **RxJS** for reactive programming.
  - Shared state management through Angular services.

---
### API Endpoints

#### Posts
GET /api/posts  
- **Description**: Get all posts with categories.

GET /api/posts/{id}  
- **Description**: Get a specific post with details and include comments.

POST /api/posts  
- **Description**: Create a new post.

PUT /api/posts/{id}  
- **Description**: Update an existing post.

DELETE /api/posts/{id}  
- **Description**: Delete a post.

---

#### Categories
GET /api/categories  
- **Description**: Get all categories.

GET /api/categories/{id}  
- **Description**: Get a specific category.

GET /api/categories/filter/{id}  
- **Description**: Get a specific category and the posts associated with it.

POST /api/categories  
- **Description**: Create a new category.

PUT /api/categories/{id}  
- **Description**: Update an existing category.

DELETE /api/categories/{id}  
- **Description**: Delete a category (reassigns posts to the default category).

---

#### Comments
GET /api/comments  
- **Description**: Get all comments.

GET /api/comments/{id}  
- **Description**: Get a specific comment.

POST /api/comments  
- **Description**: Create a new comment for a post.

