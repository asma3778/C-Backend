# Phone Store Backend

## Project Overview
This is a backend project for a phone store application. The system allows two types of users: admins and customers. The entities managed within the system include categories, brands, products, users, and orders.

## Features
- **User Management**: Create and manage users, with sign-in functionality and user authentication using JWT tokens.
- **Role-based access control**: Admins and customers have different levels of access.
- **Admins** can create, read, update, and delete (CRUD) categories, brands, and products, and can also place orders.
- **Customers** can sign in and place orders for products.
- **Category Management**: Admins can perform CRUD operations on product categories.
- **Brand Management**: Admins can perform CRUD operations on brands associated with products.
- **Product Management**: Admins can perform CRUD operations on products in the store.
- **Order Management**: Both admins and customers can place and manage orders for products.

## Technologies Used 
- **.NET 8**: Web API framework.
- **Entity Framework Core**: ORM for database interactions.
- **PostgreSQL**: Relational database for storing data.
- **JWT**: For user authentication and authorization.
- **AutoMapper**: For object mapping.
- **Swagger**: For API documentation.

## Prerequisites 
- .NET 8 SDK
- SQL Server
- Visual Studio Code

## Getting Started 
### 1. Clone the repository:
```bash 
git clone git@github.com:asma3778/sda-3-online-Backend_Teamwork.git
```
### 2. Set up the database:
- Ensure PostgreSQL server is running.
- Create an appsettings.json file.
- Update the connection string in appsettings.json:
``` json
{ 
  "ConnectionStrings": {
    "Local": "Host=localhost;Database=sda;Username=postgres;Password=123456"
  }   
}
```
- Run migrations to create the database:
```bash
dotnet ef migrations add InitialCreate 
dotnet ef database update 
```
- Run the application:
```bash
dotnet watch 
```
- The API will be available at: http://localhost:5053

### 3. Swagger
- Navigate to http://localhost:5053/swagger/index.html to explore the API endpoints.

## Project Structure
```bash
-- Controllers  API controllers for handling requests and responses.
-- Database DbContext and database configurations.
-- DTOs Data Transfer Objects for passing data between layers.
-- Entities Database entities (User, Product, Category, Order, Brand).
-- Middleware Request logging, response handling, and error management.
-- Repositories Repository layer for database operations.
-- Services Business logic layer.
-- Utils Common utility functions and helpers.
-- Migrations Entity Framework migrations.
-- Program.cs Application entry point.
```
## API Endpoints
### User
- **POST** `api/v1/users` - Create a new user.
- **POST** `api/v1/users/SignIn` - Sign in and get JWT token.
- **GET** `api/v1/users` - Get all users (admin only).
- **GET** `api/v1/users/{id}` - Get a specific user by ID.
- **PUT** `api/v1/users/{id}` - Update user information by ID.
- **DELETE** `api/v1/users/{id}` - Delete a user by ID.
### Brand
- **POST** `api/v1/brands` - Create a new brand (admin only).
- **GET** `api/v1/brands` - Get all brands.
- **GET** `api/v1/brands/{id}` - Get a specific brand by ID.
- **PUT** `api/v1/brands/{id}` - Update brand information by ID.
- **DELETE** `api/v1/brands/{id}` - Delete a brand by ID.
### Category
- **POST** `api/v1/categories` - Create a new category (admin only).
- **GET** `api/v1/categories` - Get all categories.
- **GET** `api/v1/categories/{id}` - Get a specific category by ID.
- **PUT** `api/v1/categories/{id}` - Update category information by ID.
- **DELETE** `api/v1/categories/{id}` - Delete a category by ID.
### Order
- **POST** `api/v1/orders` - Create a new order.
- **GET** `api/v1/orders` - Get all orders.
- **GET** `api/v1/orders/{id}` - Get a specific order by ID.
- **GET** `api/v1/orders/user/{userId}` - Get all orders for a specific user.
- **PUT** `api/v1/orders/{id}` - Update order information by ID.
- **DELETE** `api/v1/orders/{id}` - Delete an order by ID.
### Product
- **POST** `api/v1/products` - Create a new product (admin only).
- **GET** `api/v1/products` - Get all products.
- **GET** `api/v1/products/{id}` - Get a specific product by ID.
- **PUT** `api/v1/products/{id}` - Update product information by ID.
- **DELETE** `api/v1/products/{id}` - Delete a product by ID.
## Deployment
The application is deployed and can be accessed at: https://sharpers-project.onrender.com 
## Team Members
- **Leader**: Asma Alsaleh
- **Member**: Jana Alghasham
- **Member**: Areej Alkhaldi
- **Member**: Taif Alahmadi