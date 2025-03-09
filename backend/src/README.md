# EPL Team Info - Backend

This is the backend API for the EPL Team Info application, built with **.NET 8**.

## 🚀 Features
- Fetch team details from a third-party EPL API.
- Expose RESTful API endpoints.
- Dockerized for easy deployment.

## 📞 Tech Stack
- **.NET 8 (C#)**
- **Swagger** (API documentation)
- **Docker** (containerization)

## 🛠 Setup & Installation
### Prerequisites
- **.NET 8 SDK**
- **Docker** (if running in a container)

### Install dependencies
```sh
dotnet restore
```

### Run the application
```sh
dotnet run --project backend/
```

### Running in Docker
```sh
docker build -t epl-api .
docker run -p 5000:5000 epl-api
```

## ⚡ Configuration
Create an `appsettings.json` file with:
```json
{
  "EplApi": {
    "BaseUrl": "https://www.thesportsdb.com/api/v1/json/3/",
    "ApiKey": "your_api_key_here"
  }
}
```

## 🐝 License
This project is licensed under the MIT License.

