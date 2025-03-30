Lost and found management system using ASP.NET Core (ver 9.0) for the backend API.

For the backend to function certain NuGet packages need to be installed:
1: EntityFrameworkCore 9.0
2: EntityFrameWorkCore.SqlServer 9.0
3: EntityFrameworkCore.Tools 9.0
4. AspNetCore.Authetication.JwtBearer 9.0.3
5. IdentityModel.Tokens.Jwt 8.7.0

For testing API Endpoints:
1: AspNetCore.OpenApi 9.0.3
2: Scalar.AspNetCore 2.1.2

After intalling the packages you have to set up an SQL server and create a new database, once that's done you have to paste your connection string into appsettings.json.
After that is complete you need to open a terminal instance in visual studio and make sure you're in the (/backend) directory.

You can now use the following commands to populate the database with the required tables and their respective columns. 
  1: dotnet ef migrations add InitialCreate
  2: dotnet ef database update

You can now run the program and go to localhost:5555/scalar to test the API endpoints.
