# 🏖️ Vacation Booking Website

**Vacation Booking Website** is a full-stack ASP.NET Core MVC application designed for managing and booking holiday packages.  
It follows a layered architecture with support for DTOs, dependency injection, repository & service patterns, and SQL Server database integration.

---

## 🌟 Key Features

- 🏡 **Holiday Package Listings**: Browse available accommodations, flights, and tour activities
- 📅 **Reservation System**: Choose travel dates, guests, and submit bookings
- 👤 **User Management**: Register, login, update profile
- 🧑‍💼 **Admin Panel**: Create, update, or delete packages and tour activities
- 📦 **DTO Abstraction**: Secure and clean data transfer between layers
- 📚 **Service & Repository Pattern**: Separate business logic from data access

---

## 🚀 Tech Stack

### 🎯 Frontend & Architecture
- **ASP.NET Core MVC** (.NET 8+)
- **Razor Views** with **Bootstrap 4/5**

### 🛠 Backend
- **Entity Framework Core** (Code-First)
- **Repository Pattern**
- **Service Layer with Dependency Injection**

### 🗄 Database
- **SQL Server** (LocalDB / Azure / Docker-compatible)

### 📦 Abstractions
- **DTOs** (e.g., `UserDto`, `HotelDto`, `BookingDto`)
- **AutoMapper** for Entity ↔ DTO mapping

---

## 📁 Project Structure

VacationBooking/
├── VacationBooking.UI # MVC UI project
│ ├── Controllers/
│ ├── Views/
│ └── ViewModels/
├── VacationBooking.Business # Service layer
│ └── Services/
├── VacationBooking.DataAccess # Repository + EF DbContext
│ ├── Entities/
│ ├── Repositories/
│ └── AppDbContext.cs
├── VacationBooking.Common # DTOs, AutoMapper Profiles
│ └── Dtos/
└── VacationBooking.Tests # Unit/Integration tests

---

## ⚙️ Getting Started

### 1. Configure Database

- Update the connection string in `appsettings.json`:

"ConnectionStrings": {
  "Default": "Server=(localdb)\\mssqllocaldb;Database=VacationBookingDB;Trusted_Connection=True;"
}

- Then, apply migrations:
Update-Database

### 2. Run the Application

- Open VacationBooking.UI in Visual Studio or VS Code
- Run the application
- Navigate to: https://localhost:5001/

---

## 🔍 Use Cases
Below are the key scenarios and user flows supported by the Vacation Booking Website:

- 📝 User Registration & Login
Visitors can sign up with their credentials and log in to access personalized features such as booking history and saved vacation packages.

- 📆 Booking a Vacation
Users can browse available hotels, flights, and tours; select preferred dates and number of guests; and complete the booking process.

- 🧑‍💼 Admin Management Panel
Administrators can manage platform content by adding, updating, or removing hotels, flights, vacation packages, and other related entities through a secure admin interface.

- 🔎 Browsing & Filtering
Users can filter vacation options based on location, type (hotel, flight, tour), availability, or price, making it easier to find the perfect getaway.

---

## ✅ Core Concepts Used

- ✅ MVC Pattern: Clear separation between Models, Views, Controllers
- 📦 DTOs: Used for clean, secure communication between layers
- 🔁 AutoMapper: Handles data mapping between Entities and DTOs
- 🗃️ Generic Repository: Abstraction over data access logic
- 💼 Unit of Work: Manages transactions
- ✅ Form Validation: Using [DataAnnotations] in ViewModels
- 🔒 Simple Authentication: Role or claim-based access possible
- 💉 Dependency Injection: Handled via Program.cs
