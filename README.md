# Item Management System

## Overview

**Item Management System** is a web application developed using **ASP.NET Core MVC** and **SQL Server**. The system allows users to register, log in, and manage a list of items through **CRUD operations**. This project demonstrates secure user authentication, database operations, and a user-friendly interface suitable for a small business to manage inventory items.

---

## Features

### User Registration & Authentication
- **Register:** Users can sign up with Full Name, Email, Password.
- Passwords are hashed using **BCrypt**.
- **Login:** Users can log in with their email and password.
- **Logout:** Session-based logout to clear user data.
- Role-based access control: Only authenticated users can manage items.

### Item Management (CRUD)
- **Create:** Add new items with Name, Description, Price, Quantity.
- **Read:** View all items with sorting and search by Name or Price.
- **Update:** Edit item details.
- **Delete:** Remove items with confirmation prompts.
- Pagination: 5 items per page.

### Security
- Input validation & sanitization to prevent SQL Injection and XSS.
- CSRF protection with anti-forgery tokens.
- Passwords securely hashed.
- Session management for authentication.

### UI & UX
- Responsive interface using **Bootstrap 5**.
- Clear feedback messages for actions (success/error).
- Welcome message on login.
- Fade-out notifications and popups.

---

## Technology Stack

- **Backend:** ASP.NET Core MVC
- **Database:** SQL Server
- **Frontend:** HTML, CSS, Bootstrap, Razor Pages
- **Security:** BCrypt, Anti-forgery tokens
- **Development Tools:** Visual Studio / VS Code

---

## Database Schema

### Users Table

| Column     | Type         | Constraints                 |
|------------|-------------|-----------------------------|
| UserId     | INT         | Primary Key, Identity       |
| FullName   | NVARCHAR(50)| Not Null                   |
| Email      | NVARCHAR(100)| Not Null, Unique           |
| Password   | NVARCHAR(255)| Not Null                   |
| Role       | NVARCHAR(50)| Not Null, Default = 'User' |

### Items Table

| Column       | Type          | Constraints                 |
|--------------|---------------|----------------------------|
| ItemId       | INT           | Primary Key, Identity       |
| Name         | NVARCHAR(100) | Not Null                   |
| Description  | NVARCHAR(500) | Optional                   |
| Price        | DECIMAL(18,2) | Not Null, >= 0             |
| Quantity     | INT           | Not Null, >= 0             |
| CreatedDate  | DATETIME      | Default = GETDATE()        |

---

## Setup Instructions

1. **Clone the Repository**

```bash
git clone https://github.com/yourusername/ItemManagementSystem.git
cd ItemManagementSystem
