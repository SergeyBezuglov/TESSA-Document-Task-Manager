# Установка миграции
```
add-migration InitialCreate -Project PIMS.Migrations.MSQL -StartupProject PIMS.Web -Args "--ProviderName MSSQLServer"
```
add-migration InitialCreate -Project PIMS.Migrations.SQLite -StartupProject PIMS.Web -Args "--ProviderName SQLite"
```
## После установки обязательно обновляем
```
update-database -Project PIMS.Migrations.MSQL -StartupProject PIMS.Web -Args "--ProviderName MSSQLServer"
```
update-database -Project PIMS.Migrations.SQLite -StartupProject PIMS.Web -Args "--ProviderName SQLite"
```