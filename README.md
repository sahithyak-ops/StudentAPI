# StudentAPI 🎓

A simple **Student Management REST API** built with **ASP.NET Core Web API**, **Entity Framework Core**, and **SQL Server**.

The project includes student CRUD operations, JWT authentication, role-based authorization, validation, search, pagination, and Swagger API documentation.

## 🚀 Features

- User registration and login
- Secure password hashing with BCrypt
- JWT-based authentication
- Role-based authorization
  - **Student:** view and search students
  - **Admin:** view, add, update, and delete students
- Student CRUD operations
- DTO-based request and response models
- Input validation
- Search students by name
- Pagination
- Global exception handling middleware
- Standardized API responses
- Swagger/OpenAPI documentation
- Entity Framework Core with SQL Server

## 🛠️ Technologies Used

- **C#**
- **ASP.NET Core Web API (.NET 8)**
- **Entity Framework Core**
- **SQL Server**
- **JWT (JSON Web Tokens)**
- **BCrypt**
- **Swagger / OpenAPI**

## 📁 Project Structure

```text
StudentAPI/
│
├── Controllers/
│   ├── AuthController.cs
│   └── StudentController.cs
│
├── Data/
│   └── AppDbContext.cs
│
├── DTOs/
│   ├── ApiResponseDto.cs
│   ├── LoginDto.cs
│   ├── RegisterDto.cs
│   ├── StudentDto.cs
│   └── StudentResponseDto.cs
│
├── Middleware/
│   └── ExceptionMiddleware.cs
│
├── Models/
│   ├── Student.cs
│   └── User.cs
│
├── Services/
│   ├── IStudentService.cs
│   └── StudentService.cs
│
├── Program.cs
└── appsettings.json
```

## ⚙️ Setup

### 1. Clone the repository

```bash
git clone <YOUR-GITHUB-REPOSITORY-URL>
cd StudentAPI
```

### 2. Restore dependencies

```bash
dotnet restore
```

### 3. Configure SQL Server

Update the `DefaultConnection` in `appsettings.json` with your SQL Server connection details.

Example:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=StudentDB;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

> Do not commit real database passwords or JWT secrets to a public repository.

### 4. Configure JWT

Add your JWT settings to `appsettings.json`:

```json
{
  "Jwt": {
    "Key": "YOUR_LONG_RANDOM_SECRET_KEY"
  }
}
```

For a public GitHub repository, use environment variables or User Secrets instead of storing a real secret in the repository.

### 5. Create/update the database

Run:

```bash
dotnet ef database update
```

If EF CLI is not installed:

```bash
dotnet tool install --global dotnet-ef
```

### 6. Run the API

```bash
dotnet run
```

The API will be available at the local URL shown in the terminal.

## 📖 Swagger

After starting the application, open the Swagger URL shown by the application, usually:

```text
http://localhost:5042/swagger
```

Swagger lets you test all API endpoints directly from the browser.

## 🔐 Authentication Flow

### Register

```http
POST /api/Auth/register
```

Example:

```json
{
  "name": "Test User",
  "email": "test@example.com",
  "password": "TestPassword123"
}
```

### Login

```http
POST /api/Auth/login
```

Example:

```json
{
  "email": "test@example.com",
  "password": "TestPassword123"
}
```

The login endpoint returns a JWT token.

Click **Authorize** in Swagger and enter:

```text
Bearer YOUR_JWT_TOKEN
```

## 👥 Authorization

| Operation | Student | Admin |
|---|:---:|:---:|
| View students | ✅ | ✅ |
| Search students | ✅ | ✅ |
| Add student | ❌ | ✅ |
| Update student | ❌ | ✅ |
| Delete student | ❌ | ✅ |

## 📚 Student Endpoints

| Method | Endpoint | Access | Description |
|---|---|---|---|
| GET | `/api/Student` | Student/Admin | Get students |
| GET | `/api/Student/{id}` | Student/Admin | Get student by ID |
| GET | `/api/Student/search` | Student/Admin | Search students |
| POST | `/api/Student` | Admin | Add a student |
| PUT | `/api/Student/{id}` | Admin | Update a student |
| DELETE | `/api/Student/{id}` | Admin | Delete a student |

## 🔎 Search & Pagination

Example:

```http
GET /api/Student/search?name=Ananya&page=1&pageSize=10
```

Normal pagination:

```http
GET /api/Student?page=1&pageSize=10
```

## 🧪 Example Student Request

```json
{
  "name": "Ananya",
  "age": 18,
  "department": "CSE",
  "email": "ananya@example.com",
  "phone": "9876543210"
}
```

## 🏗️ Architecture

The project follows a simple layered architecture:

```text
Client / Swagger
       ↓
Controllers
       ↓
Services
       ↓
Entity Framework Core
       ↓
SQL Server
```

JWT authentication and authorization are handled through ASP.NET Core middleware.

## 📌 Learning Goals

This project was built to practice:

- ASP.NET Core Web API development
- REST API design
- C# and async programming
- Entity Framework Core
- SQL Server integration
- JWT authentication
- Role-based authorization
- DTOs and validation
- Service-layer architecture
- API testing with Swagger
- Git and GitHub project management

## 🔒 Security Notes

- Passwords are hashed using BCrypt rather than stored as plain text.
- JWT authentication protects secured endpoints.
- Admin-only operations use role-based authorization.
- Never commit production database credentials or JWT secrets to GitHub.
- For production applications, use secure secret management and HTTPS.

## 📄 License

This project is intended for learning and educational purposes.
