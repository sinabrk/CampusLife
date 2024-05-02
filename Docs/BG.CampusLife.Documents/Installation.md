# Installation Guides

Campus Life API runs on **.NET 5.0 SDK** and **Microsoft SQL Server**.

To run the project (first insure sql server is running):

**Note:**
if there is no migration file, you need to create migration files first. As we have **two db contexts**
we should use these below commands:

1. `dotnet ef migrations add --context [NameOfMigration] --project BG.CampusLife.Infrastructure --startup-project BG.CampusLife.Presentation -o Migrations`
2. `dotnet ef migrations add --context [NameOfMigration] --project BG.CampusLife.Persistence --startup-project BG.CampusLife.Presentation -o Migrations`

if you are using the Package Manager Console and you did not install the EF CLI Tools use the following commands:
1. `Add-Migration [NameOfMigration] -Context IdentityDbContext`  (CAUTION: Change Default Project of package manger console to BG.CampusLife.Infrastructure)
2. `Add-Migration [NameOfMigration] -Context CampusContext`  (CAUTION: Change Default Project of package manger console to BG.CampusLife.Persistence)

_There is no need to update database, it will migrate in code._

(However if you want to udpate the DB use the following command: `Update-Database -Context [NameOfContext]`)


Next:
> $ cd BG.CampusLife.Presentation
>
> $ dotnet run

If there is no **CampusLife Database** available, database will be created automatically.
In our **test phase**, there is also a seed that imports some data into database.
if you want to use the predefined user and do not register with your own email and password,
there are two free users, one as _Admin_, another as _Student_. In production seed will be removed.
```
Admin:
    username: Test@Email.com
    password: Test#1234
 
Student:
    username: User@Email.com
    password: Test#1234
```

### After you run the project:

**Swagger** `https://localhost:5001/swagger/index.html` 

**ReDoc** `https://localhost:5001/api-docs/index.html`