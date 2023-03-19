To add migration, set HTS.Data project and run:
Add-Migration "Initial" -OutputDir "Migrations" -Context AppDbContext
Update-Database -Context AppDbContext