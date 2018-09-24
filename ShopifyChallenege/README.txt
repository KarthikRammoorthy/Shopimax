To run Shopimax API
1. Download the prerequisites as mentioned in documentation.
Dotnet Core SDK - https://www.microsoft.com/net/download
Visual Studio Code is the recommended Editor.
2. Choose File => Open folder. Select Shopimax.API folder under Shopimax folder.
3. "dotnet restore" CLI command to restore dependencies.
4. "dotnet run" to start the application. Once run for the first time, comment seeder methods in Startup.cs file.
5. Application listens in http://localhost:5000/api/ path. Postman is the recommended API tool.
6. Once user logs in, a token is received upon successful login. Pass token in header for accessing endpoints. Refer documentation for detailed steps.

To run Shopimax Test
1. Choose File => Open folder, select ShopimaxTest folder under Shopimax folder.
2. "dotnet restore" CLI command to restore dependencies.
3. "dotnet test" CLI command to run test.