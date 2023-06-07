To add migration, set HTS.Data project and run:
Add-Migration "Initial" -OutputDir "Migrations" -Context AppDbContext
Update-Database -Context AppDbContext
Update-Database -Args '--environment Production'

For mac:
dotnet ef migrations Add  Initial --context AppDbContext -o Migrations

// To set environment in package manager console
$env:ASPNETCORE_ENVIRONMENT='Development'


Staging ortamına kurulum adımları:

	Lokal pgAdmin üzerinden Staging DB'ye bağlan.
	HTS veritabanını sil, yeniden oluştur.
	PM Console üzerinde environment'i staging'e çek.
	Update database yap.
	HTS.DbMigrator.Staging çalıştır.

