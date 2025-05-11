# Statistics Dashboard

A web application designed for managing and visualizing statistics related to items and clients. This app allows users to manage items, assign items to clients, view client profiles, and generate visual statistics on item distribution by category, as well as client-based statistics.

## Features

- **Item Management:**
  - View and manage items.
  - Assign items to clients.
  - Delete item assignments from clients.

- **Client Management:**
  - View client profiles.
  - View assigned items and their total value.
  - Assign existing items to clients and remove item assignments.

- **Statistics Dashboard:**
  - Visualize item statistics by category (e.g., total value, item count).
  - Visualize client statistics (e.g., total number of items, total value of assigned items).

## Technologies Used

- **ASP.NET Core MVC** for the web application framework.
- **Entity Framework Core** for database operations.
- **Chart.js** for data visualization.
- **Bootstrap** for styling and responsive layout.
- **Microsoft SQL Server** for the database.

## Setup

### Prerequisites

- Install [Visual Studio](https://visualstudio.microsoft.com/) or another IDE that supports ASP.NET Core.
- Install [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) or use a local database.
- Ensure that the following NuGet packages are installed:
  - `Microsoft.EntityFrameworkCore`
  - `Microsoft.EntityFrameworkCore.SqlServer`
  - `Microsoft.AspNetCore.Mvc`
  - `Chart.js` (for front-end data visualization)

### Clone the Repository

```bash
git clone https://github.com/your-username/statistics-dashboard.git
cd statistics-dashboard
```
### Database Setup
- Create a new database in SQL Server.
- Update the `appsettings.json` file with your database connection string.
- Run the migrations to set up the database schema in Package Manager Console: `Update-Database`
- Run the app.


## API Endpoints

### Assign Item to Client

**POST** `/Clients/AssignItem`

**Parameters:**
- `clientId`
- `itemId`

---

### Remove Assigned Item

**POST** `/Clients/RemoveAssignedItem`

**Parameters:**
- `clientId`
- `itemId`

---

### Get Item Statistics

**GET** `/Dashboard/GetItemStats`

**Returns:** JSON with item statistics by category.

---

### Get Client Statistics

**GET** `/api/statistics/client-stats`

**Returns:** JSON with client-based statistics (item count, total value).

---
