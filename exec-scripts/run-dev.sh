#!/bin/bash

# Environment variables for PostgreSQL
export POSTGRES_CONNECTION_STRING="Host=localhost;Port=5432;Database=sirproject_crud;Username=postgres;Password=masterkey"

# Environment variables for JWT
export JWT_KEY="super_secret_key_12345678901234567890123456789012" # Must be at least 32 characters for HS256
export JWT_ISSUER="SirProject"
export JWT_AUDIENCE="SirProjectUsers"

# Apply EF Core migrations
dotnet ef database update

# Run the application
dotnet run
