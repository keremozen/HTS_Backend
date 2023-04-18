To add migration, set HTS.Data project and run:
Add-Migration "Initial" -OutputDir "Migrations" -Context AppDbContext
Update-Database -Context AppDbContext
Update-Database -Args '--environment Production'

For mac:
dotnet ef migrations Add  Initial --context AppDbContext -o Migrations

// To set environment in package manager console
$env:ASPNETCORE_ENVIRONMENT='Development'
