# ⚡ Test Project

A simple .NET 8 API service that fetches mock data from Azure SQL Server.

---

## ✅ Features Implemented

- ✅ .NET 8 Web API project
- ✅ Fetches data from **Azure SQL Server**
- ✅ Exposes a REST endpoint returning structured DTOs
- ✅ Reads SQL connection string from **environment variables**
- ✅ Token-based **multi-tenant authentication**
- ✅ **Caching** for tenant
- ✅ **Pagination** support
- ✅ **Unit tests** using xUnit and Moq
- ✅ Common **error handling middleware**
- ✅ Jupyter Notebook

---

## 🚀 Getting Started

### 🔧 Prerequisites

- NET 8 SDK
- Azure SQL Server connection details
- Python and Jupyter notebook

---

### ⚙️ Setup

#### 1. Set the environment variable for connection string

**On macOS/Linux:**

```bash
export DefaultConnection="Server=<server_name>;Initial Catalog=EnergsoftTestDb;Persist Security Info=False;User ID=<your_username>;Password=<your_password>;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
```

You can also add it to `~/.zshrc` or `~/.bashrc`:

```bash
echo 'export DefaultConnection="your-connection-string-here"' >> ~/.zshrc
source ~/.zshrc
```

**On Windows (PowerShell):**

```powershell
$env:DefaultConnection="Server=<server_name>;Initial Catalog=EnergsoftTestDb;Persist Security Info=False;User ID=<your_username>;Password=<your_password>;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
```

---

### ▶️ Run the app

```bash
cd EnergsoftInterview.Api
dotnet run
```

The API will be available at:

```
https://localhost:5103/swagger
```

---

## 🧪 Running Unit Tests

```bash
cd EnergsoftInterview.Tests
dotnet test
```

---

## 📌 Test and Run with jupyter notebook

open notebook/verify_data.ipynb file and set Configuration variables:

- base_url = "http://localhost:5103"  **Change if needed**
- endpoint = "/api/measurements?page=1&pageSize=10" **Change page and pageSize if needed**
- token = "Tenant_1_token" **Actual token values (Tenant_1_token, Tenant_2_token)**

```bash
cd notebook
jupyter notebook
```
