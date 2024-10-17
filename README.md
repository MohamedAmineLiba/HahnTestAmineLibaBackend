Steps To Follow for running the project:
1- Install .NET SDK (if not installed) / To verify installation: dotnet --version ( The result should return a version number of 8.x.x.).
2- Restore the project's Dependecies / command : dotnet restore.
3-Environment variables and configuration ( connection strings in appsettings.json) - Apply database migrations (dotnet ef database update) 
4- Run the project and test the API's ( using swagger (default integrated or postman if needed ).
5- verify the endpoints and frontend URL for CORS in program.cs (if needed).
