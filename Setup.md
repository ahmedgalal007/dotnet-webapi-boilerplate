# Install Entity Framework dotnet Tool
### dotnet tool install --global dotnet-ef --version 7.0.13

# Add Migration to ApplicationDbContext

dotnet ef migrations add `Migration Name` --project .././Migrators/Migrators.`DBProvider`/ --context ApplicationDbContext -o Migrations/Application

# Add Migration to TenantDbContext

dotnet ef migrations add `Migration Name` --project .././Migrators/Migrators.`DBProvider`/ --context TenantDbContext -o Migrations/Tenant
