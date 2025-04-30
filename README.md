# ⚡ Test Project

A simple .NET 8 API service that fetches mock data from Azure SQL Server.

---

## ✅ Features Implemented

- ✅ .NET 8 Web API project
- ✅ Fetches data from **Azure SQL Server**
- ✅ Exposes a REST endpoint returning structured DTOs
- ✅ Reads SQL and CosmosDB connection string from **environment variables**
- ✅ Token-based **multi-customer authentication**
- ✅ **PartitionKey** for CosmosDB
- ✅ **Runtime** database switcher
- ✅ **Parametrized** CosmosDB inline performance optimized queries
- ✅ Server-side **paging** with **continuation tokens**
- ✅ **Caching** for customer
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

**For CosmosDB**

Environment variables:

```bash
export COCSMOSDB_CONNECTIONSTRING="AccountEndpoint=https://energsoft-cosmos.documents.azure.com:443/;AccountKey=<your_cosmosdb_key>;"
export COSMOSDB_DATABASE="EnergsoftDB"
export COSMOSDB_CONTAINER="Measurements"
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

open notebook/verify_data.ipynb file and set variables in a proper sections:

- base_url = "http://localhost:5103"  # Change if needed
- endpoint = "/api/measurements?page=1&pageSize=1&continuationToken=" # Change page and pageSize if needed
- token = "token_1"  # Actual token values (token_1: dataSource=SQL, token_2: dataSource=SQL, token_3: dataSource=CosmosDB)

```bash
cd notebook
jupyter notebook
```