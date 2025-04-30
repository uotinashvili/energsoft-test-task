# ⚡ Test Project

A simple .NET 8 API service that fetches mock data from Azure SQL Server.

---

## ✅ Features Implemented

- ✅ .NET 8 Web API project
- ✅ Fetches data from **Azure SQL Server**
- ✅ Exposes a REST endpoint returning structured DTOs
- ✅ Reads SQL and CosmosDB connection string from **environment variables**
- ✅ JWT **multi-customer authentication**
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

- at first you have to send POST request to api/auth/token with "X-API-Key" header (value will be api_key_1, api_key_2, etc...) for Auth.
- to read data from Azure SQL Server, you don't need to pass continuationToken as param. api_key could be api_key_1, api_key_2 based on customer
- to read data from CosmosDB, at first call you don't need to pass continuationToken, but every next call you need to pass it returned from the previous call.

```bash
api_key_1 : SQL : customer_1
api_key_2 : SQL : customer_2
api_key_3 : CosmosDB : customer_3
```

```bash
- base_url = "http://localhost:5103"
- measurements_endpoint = f"{base_url}/api/measurements?page=1&pageSize=3&continuationToken="
- api_key = "api_key_1"
```

```bash
cd notebook
jupyter notebook
```
