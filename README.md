# Frontend and Backend project for the Movies Web App

This project is a web application that allows users to search for movies and view details about them. The project is divided into two parts: the frontend and the backend. The frontend is a React app that uses the Backend API to search for movies and display their details. The backend is an ASP .NET Core Web API that provides the data for the frontend.

## Running
### Docker Compose
You can use docker-compose to run the frontend and backend together. Navigate to the root directory and run the following command:
```bash
docker-compose up
```

After the containers are running, you can navigate to `http://localhost:3000` in your browser to see the frontend.
To see the backend API, navigate to `https://localhost:5001/swagger/index.html` in your browser.
The backend has a few endpoints that are protected by an API key, which is passed in the Authorization header.
The API key is `apisecret`.

### Frontend
To run the frontend, navigate to the `MoviesWeb` directory and run the following commands:
```bash
npm install
npm start
```

### Backend
To run the backend, navigate to the `MoviesAPI` directory and run the following commands:
```bash
dotnet run
```

