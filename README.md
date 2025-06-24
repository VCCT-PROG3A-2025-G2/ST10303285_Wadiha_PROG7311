# Agri-Energy Connect WebApp

##  Table of Contents

- [Description](#description)
- [System Functionalities](#system-functionalities)
- [Technologies Used](#technologies-used)
- [Development Environment Setup](#development-environment-setup)
- [How to Build and Run the Prototype](#how-to-build-and-run-the-prototype)
- [User Roles and Access](#user-roles-and-access)
- [Testing and Validation](#testing-and-validation)
- [Notes](#notes)

## Description: 
Agri-Energy Connect is a web app that connects farmers and their products with employees. Employees can create farmer profiles with which farmers cn login into the platform to add their products. Employees are also able to see a list of all the products in the platform.

##  System Functionalities:
- Registration and Login of Employee
- Employee creats Farmer profile
- Farmer logs in once profile is created
- Farmer creates and adds their products
- Employees are abel to view the products list with filters
- Role-based access control using ASP.NET Core Identity


##  Technologies Used:
- .NET 8 SDK
- ASP.NET Core MVC
- ASP.NET Core Identity
- SQLite (local database)
- Visual Studio 2022 or later


##  Development Environment Setup

### Prerequisites

Before running the application, ensure the following are installed:
- .NET 8 SDK: https://dotnet.microsoft.com/download
- Visual Studio 2022 or later: https://visualstudio.microsoft.com/
- Git (optional)

### Steps to Set Up the Development Environment

1. **Clone the Repository**

  ```
  Git clone https://github.com/VCCT-PROG3A-2025-G2/ST10303285_Wadiha_PROG7311.git 
  cd FarmerConnectWebApp
  ```

2. **Open the Solution in Visual Studio**
- Locate and open `FarmersConnectWebApp.sln`

3. **Restore NuGet Packages**
- Visual Studio usually restores packages automatically.
- Or run:
  ```
  dotnet restore
  ```

4. **Apply Database Migrations**
- In Visual Studio, go to **Tools > NuGet Package Manager > Package Manager Console**, then run:
  ```
  Update-Database
  ```
- Or from the command line:
  ```
  dotnet ef database update
  ```


##  How to Build and Run the Prototype

### Build the Application

In Visual Studio:
- Click **Build > Build Solution** or press `Ctrl+Shift+B`

Command line:
  ```
  dotnet run
  ```
-The app will open in your browser

##  User Roles and Access

### Employee
- Register and log in
- Create and manage farmer profiles
- View all product listings submitted by farmers
 - **Email:** `employee@example.com`
 - **Password:** `Employee123!` 

### Farmer
- Can only log in if created by an employee
- Add,their own product listings
- - **Login credentials:**
  - **Email:** Provided by the employee during profile creation `Farmer@farm.com`
  - **Password:** `Farmer@123` (default for all farmer accounts)


##  Testing and Validation

- **Form Validation**: Ensures all required fields and valid input ranges are enforced.
- **Error Handling**: Covers login failures, database errors, and invalid data entries.
- **Responsive Design**: Tested on desktop, tablet, and mobile using browser tools.
- **Manual Testing**: All core user journeys have been manually tested by test users.



##  Notes

- SQLite is used for simplicity and local storage. You can switch to SQL Server for production.
