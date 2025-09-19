# Introduction:
The project comprises of two main directories - frontend and backend. The front end consists of a React SPA whilst the backend is a C# ASP.NET Core Web Api. The projects have been de-coupled in this way because their independence allows for more flexibility. That way we can make changes on the backend and frontend without affecting one or the other too much.

## Setup Instructions

### Pre-requisites
To successfully run the application. You will need to have dotnet 9 runtime and nodejs 20 installed on your machine.

### Build and run the C# ASP.NET Core Web API
1. Navigate to the directory /backend
   
    ```javascript
    
          cd backend
    
    ```
    
3. Setup the database. The database is a postgresql database.
   Modify the file appsettings.json by changing the host, port, username, password in the setting marked 'ConnectionStrings'
   Ensure the username has CREATEDB rights.
5. Run the following command to create the database.
   
     ```javascript
     
          dotnet ef database update --project EazyPay.Infrastructure/EazyPay.Infrastructure.csproj --startup-project EazyPay.Api/EazyPay.Api.csproj
     
     ```
     
6. After the previous steps have been completed, run the following command to start the api:
   
      ```javascript
      
          dotnet run --project EazyPay.Api/EazyPay.Api.csproj
      
     ```
      
8. You have completed building the API and it is now listening on the specified port 🎉.

### Build and run the React app
1. For the frontend. Navigate to the directory:
   
  ```javascript

    cd frontend

  ```

2. Run the folling commands:
   
   ```javascript
   
     npm install
     npm run dev
   
   ```
   
 The react app should be running on the specified port 🎉. You can proceed to navigate to the url specified to view and interact with the app.
