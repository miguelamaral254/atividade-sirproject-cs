export POSTGRES_CONNECTION_STRING="Host=localhost;Port=5432;Database=sirproject_crud;Username=postgres;Password=masterkey"
export JWT_KEY="super_secret_key_12345678901234567890123456789012"
export JWT_ISSUER="SirProject"
export JWT_AUDIENCE="SirProjectUsers"

dotnet ef migrations add InitialCreate -o Infrastructure/Data/Migrations && dotnet ef database update