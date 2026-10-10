# CarStockApi-TechTest
A car stock management API built with **ASP.NET Core**, **FastEndpoints** and **SQLite**.



## Overview

This project provides a RESTful API for managing car inventory across multiple dealerships.

Authenticated dealers can manage their own vehicles, including:
- Adding cars
- Retrieving car details
- Listing and searching cars
- Updating stock levels
- Removing cars

*JWT authentication** and **dealer-level access control** ensure that each dealer can only access and manage their own vehicle inventory.



## Technologies Used

- **Language & Runtime:** C# / .NET 10 and ASP.NET Core 10
- **API Framework:** FastEndpoints 8.3.0 — Defines API endpoints using the REPR pattern.
- **Data Access:** Dapper 2.1.89 — Provides lightweight database access.
- **Database:** SQLite via Microsoft.Data.Sqlite 10.0.12.
- **Authentication:** Microsoft.AspNetCore.Authentication.JwtBearer 10.0.12 for JWT authentication.
- **Password Hashing:** ASP.NET Core Identity `PasswordHasher<Dealer>` for password hashing and verification.



## API Routes & Directory Mapping

All endpoint routes are implemented using **FastEndpoints**. Except for the login endpoint, all car management endpoints inherit from custom base endpoints (`DealerEndpoint` variants), making them **secure by default** and accessible **only to authenticated dealers**.


### 🔐 Authentication

| HTTP Method | Route | Description | Endpoint Class File |
| :--- | :--- | :--- | :--- |
| POST | `/api/auth/login` | Authenticates dealer and returns a JWT | `Features/Auth/Login/LoginEndpoint.cs` |


### Security

- **JWT Authentication** — Protected endpoints require a valid JWT token.
- **Password Hashing** — Dealer passwords are verified against stored password hashes.
- **Dealer-Level Access Control** — Dealers can only access and manage their own car inventory.
- **Input Validation** — Validates login requests to ensure the username and password are provided.
- **Parameterized SQL Queries** — Uses Dapper parameterized queries to help prevent SQL injection.


### Car Stock Management

| HTTP Method | Route | Description | Endpoint Class File |
| :--- | :--- | :--- | :--- |
| POST | `/api/cars` | Adds a new car to the dealer's stock | `Features/Cars/AddCar/AddCarEndpoint.cs` |
| GET | `/api/cars` | Lists all cars belonging to the authenticated dealer | `Features/Cars/ListCars/ListCarsEndpoint.cs` |
| GET | `/api/cars/{id}` | Retrieves details of a specific car by ID | `Features/Cars/GetCar/GetCarEndpoint.cs` |
| GET | `/api/cars/search` | Searches/filters cars within the dealer's stock | `Features/Cars/SearchCar/SearchCarEndpoint.cs` |
| PUT | `/api/cars/{id}/stock` | Updates the stock quantity of a specific car | `Features/Cars/UpdateStock/UpdateStockEndpoint.cs` |
| DELETE | `/api/cars/{id}` | Removes a car from the dealer's stock | `Features/Cars/RemoveCar/RemoveCarEndpoint.cs` |



## Setup and Run
### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)

### Steps

1. Clone the repository:

   ```bash
   git clone https://github.com/jordansusu/CarStockApi-TechTest.git
   ```

2. Navigate to the API project directory:

   ```bash
   cd CarStockApi-TechTest/CarStockApi
   ```

3. Run the API:

   ```bash
   dotnet run
   ```

The API will start using the configured launch settings.



## Authentication

This API uses **JWT (JSON Web Token) authentication** to authenticate dealers.

1. Send a `POST` request to `/api/auth/login` with valid dealer credentials.
### Login Example

Send a `POST` request to `/api/auth/login` with the following JSON body:

```json
{
  "username": "dealer_a",
  "password": "aaaaaaaa"
}
```

2. Copy the JWT token from the response.
3. Include the token in the `Authorization` header when calling protected endpoints:

   ```http
   Authorization: Bearer <your-jwt-token>
   ```

**Dealer-level access control** ensures that dealers can only access and manage their own car inventory.


### Test Credentials

Use the following credentials to test the API:

| Username | Password |
|---|---|
| `dealer_a` | `aaaaaaaa` |
| `dealer_b` | `bbbbbbbb` |



## Example API Requests

### 1. Login

**Request**
POST /api/auth/login
Content-Type: application/json

**Request Body(JSON)**
{
  "Username": "dealer_a",
  "Password": "aaaaaaaa"
}

### 2.Add a Car

**Request**
POST /api/cars
Authorization: Bearer <your-jwt-token>
Content-Type: application/json

**Request Body(JSON)**
{
  "Brand": "Toyota",
  "Model": "Corolla",
  "Year": 2022,
  "Stock": 5
}

### 3. List Cars and Stock Level

**Request**
GET /api/cars
Authorization: Bearer <your-jwt-token>

### 4. Get a Car by ID

**Request**
GET /api/cars/{car_id}
Authorization: Bearer <your-jwt-token>

### 5. Search Cars

**Request**
GET /api/cars/search?brand={Brand_Name}&model={Model_Name}
Authorization: Bearer <your-jwt-token>

### 6. Update Stock

**Request**
PUT /api/cars/{car_id}/stock
Authorization: Bearer <your-jwt-token>
Content-Type: application/json

**Request Body(JSON)**
{
  "Stock": 10
}

### 7. Delete a Car

**Request**
DELETE /api/cars/{car_id}
Authorization: Bearer <your-jwt-token>



## Error Handling

### Common HTTP Status Codes

The API returns appropriate HTTP status codes to indicate the result of each request.

| Status Code | Description |
|---|---|
| `200 OK` | The request completed successfully |
| `201 Created` | A new car was created successfully |
| `400 Bad Request` | The request contains invalid data |
| `401 Unauthorized` | Authentication is missing or invalid |
| `403 Forbidden` | The dealer does not have permission to access the resource |
| `404 Not Found` | The requested resource could not be found |
| `500 Internal Server Error` | An unexpected server error occurred |


### Endpoint-Specific Behavior

| Endpoint | Behavior |
|---|---|
| `POST /api/auth/login` | Returns an authentication error when the credentials are invalid |
| `POST /api/cars` | Validates the request before creating a car |
| `GET /api/cars` | Returns the authenticated dealer's car inventory |
| `GET /api/cars/{id}` | Returns an error if the car does not exist or does not belong to the dealer |
| `GET /api/cars/search` | Returns cars matching the search criteria within the dealer's inventory |
| `PUT /api/cars/{id}/stock` | Returns an error if the car cannot be found or accessed |
| `DELETE /api/cars/{id}` | Returns an error if the car cannot be found or accessed |



## Project Structure

```text
CarStockApi/
├── Auth/                       # Authentication and JWT handling
├── Database/                   # Database connection and SQLite database
├── Features/
│   ├── Auth/
│   │   └── Login/              # Dealer login endpoint and validation
│   └── Cars/
│       ├── AddCar/             # Add a car
│       ├── RemoveCar/          # Delete a car
│       ├── GetCar/             # Retrieve a car by ID
│       ├── ListCars/           # List cars
│       ├── SearchCar/          # Search cars
│       └── UpdateStock/        # Update stock levels
├── Models/                     # Data models
├── Program.cs                  # Application configuration
└── appsettings.json            # Application settings
```
