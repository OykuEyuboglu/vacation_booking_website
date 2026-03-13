# 🏖️ Vacation Booking Website

<div align="center">

![Vacation App](https://cdn-icons-png.flaticon.com/512/201/201623.png)

*A full-stack holiday booking platform built with ASP.NET Core MVC.*

[![ASP.NET](https://img.shields.io/badge/ASP.NET%20Core-5C2D91?style=for-the-badge&logo=dotnet&logoColor=white)]()
[![Entity Framework](https://img.shields.io/badge/Entity%20Framework-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)]()
[![SQL Server](https://img.shields.io/badge/SQL%20Server-CC2927?style=for-the-badge&logo=microsoftsqlserver&logoColor=white)]()
[![Bootstrap](https://img.shields.io/badge/Bootstrap-563D7C?style=for-the-badge&logo=bootstrap&logoColor=white)]()

</div>

---

# 🌟 Project Overview

**Vacation Booking Website** is a **full-stack ASP.NET Core MVC web application** designed for managing and booking holiday packages.

The platform allows users to browse **hotels, flights, and tours**, make reservations, and manage bookings.  
It follows a **layered architecture** with DTO abstraction and uses **Entity Framework Core** with **SQL Server**.

This project was developed as the **final graduation project** of the:

🎓 **BilgeAdam Akademi – Fullstack Web Development with .NET Program**

The project demonstrates modern backend development practices such as:

- Repository Pattern
- Service Layer Architecture
- DTO abstraction
- Dependency Injection
- Entity Framework Core

---

# 🎥 Demo Video

[![Watch the demo](https://img.youtube.com/vi/O5BL-8hjChw/0.jpg)](https://youtu.be/O5BL-8hjChw)

> Click the thumbnail above to watch the full project demo.

---

# 🚀 Features

## 🏡 Vacation Package Listings
Users can browse available:

- Hotels
- Flights
- Tour activities

Each listing includes detailed information such as location, price, and availability.

---

## 📅 Reservation System

Users can:

- Select travel dates
- Choose number of guests
- Complete booking reservations

All reservations are stored securely in the database.

---

## 👤 User Management

- User registration
- Login system
- Profile management
- Booking history tracking

---

## 🧑‍💼 Admin Panel

Administrators can:

- Add new hotels or tours
- Update package information
- Delete outdated listings
- Manage platform content

---

## 📦 DTO Abstraction

The application uses **Data Transfer Objects (DTOs)** to ensure:

- Secure data flow
- Clean separation between layers
- Better maintainability

Examples:

```
UserDto
HotelDto
BookingDto
FlightDto
```

---

# 🧱 Technology Stack

## 🎯 Frontend

```
ASP.NET Core MVC
Razor Views
HTML / CSS
Bootstrap
```

---

## 🛠 Backend

```
ASP.NET Core (.NET 8)
Entity Framework Core
Repository Pattern
Service Layer Architecture
Dependency Injection
AutoMapper
```

---

## 🗄 Database

```
Microsoft SQL Server
Code-First Approach
Entity Framework Migrations
```

---

# 🧩 Project Architecture

```
VacationBooking
│
├── VacationBooking.UI
│     ├── Controllers
│     ├── Views
│     └── ViewModels
│
├── VacationBooking.Business
│     └── Services
│
├── VacationBooking.DataAccess
│     ├── Entities
│     ├── Repositories
│     └── AppDbContext.cs
│
├── VacationBooking.Common
│     └── DTOs
│
└── VacationBooking.Tests
```

---

# 🔄 Application Workflow

1️⃣ User visits the website  
2️⃣ The MVC controller processes requests  
3️⃣ Business services handle application logic  
4️⃣ Repository layer communicates with the database  
5️⃣ Data is returned to the UI via DTOs

---

# ⚙️ Installation & Setup

## 📌 Requirements

Make sure the following tools are installed:

- .NET 8 SDK
- SQL Server
- Visual Studio or VS Code
- Entity Framework Core Tools

---

# 🔧 Configure Database

Update your **connection string** inside `appsettings.json`.

Example:

```json
"ConnectionStrings": {
  "Default": "Server=(localdb)\\mssqllocaldb;Database=VacationBookingDB;Trusted_Connection=True;"
}
```

Then apply migrations:

```
Update-Database
```

---

# 🚀 Run the Application

1️⃣ Clone the repository

```
git clone https://github.com/OykuEyuboglu/vacation_booking_website.git
```

2️⃣ Open the project

Open **VacationBooking.UI** in Visual Studio.

3️⃣ Run the application

```
https://localhost:5001
```

---

# 📚 Concepts Demonstrated

This project demonstrates several important software engineering concepts:

- MVC Pattern (Model–View–Controller)
- DTO architecture
- AutoMapper usage
- Repository Pattern
- Service Layer pattern
- Dependency Injection
- Entity Framework Core
- SQL Server integration

---

# 🎓 Education Context

This project was developed as part of:

**BilgeAdam Akademi  
Fullstack Web Development with .NET Training Program**

Topics covered during the training included:

- ASP.NET Core MVC
- C# Programming
- Entity Framework Core
- SQL Server
- RESTful architecture
- Dependency Injection
- Clean architecture principles

---

# 👩‍💻 Author

**Dila Öykü Eyüboğlu**  
