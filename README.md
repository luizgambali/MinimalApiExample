# C# Minimal Api example

.NET 6 Minimal Api Example

## *BEFORE RUN, run Migrations!*

	dotnet ef migrations add InitialMigration
	dotnet ef database update
  
### Documentation and live test using Swagger

https://localhost:7028/swagger/index.html

*update launchSettings.json to use a different address and port*

### Dependencies 

	Microsoft.EntityFrameworkCore
	Microsoft.EntityFrameworkCore.SqLite
	Microsoft.EntityFrameworkCore.Design
	Swashbuckle.AspNetCore
