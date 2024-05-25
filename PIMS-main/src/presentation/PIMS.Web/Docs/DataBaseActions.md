# Установка миграции
```
add-migration InitialCreate -Project PIMS.Migrations.MSQL -StartupProject PIMS.Web -Args "--ProviderName MSSQLServer"
```

## После установки обязательно обновляем
```
update-database -Project PIMS.Migrations.MSQL -StartupProject PIMS.Web -Args "--ProviderName MSSQLServer"
```
