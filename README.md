# SGITS Project
Steps for getting API up and running:

Step 1:
- Clone repository from: https://github.com/AJ-1003/SGITS-Project.git

Step 2:
- Create appsettings.Development.json file under SGITS.API project
- Add the following content to file:

--------------------------------------------------------------- File Content (Start) ---------------------------------------------------------------

{
  "ConnectionStrings": {
    "DefaultConnectionString": "Server=<Replace with your server name>;Database=SGITS_DB;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}

---------------------------------------------------------------- File Content (End) ----------------------------------------------------------------

Step 3:
- Rebuild Solution

Step 4:
- CLI: 
  - cd to the SGITS.Persistence project directory and use the following command: dotnet ef database update --project ../SGITS.Persistence/SGITS.Persistence.csproj
- PMC (Package Manager Console)
  - Select the SGITS.Persistence project as 'Default project' in the PMC
  - Use the following command: update-database
- This will create the database with the latest migrations on the server specified in the "DefaultConnectionString"
- It will also create the required stored procedure for the 'Household Report'

Step 5:
- Run application (F5)
- Swagger documentation is included on Swagger UI