# SirProject

Sistema de gerenciamento de usuários e pessoas desenvolvido em ASP.NET Core MVC.

## Tecnologias
- .NET 8 / C#
- Entity Framework Core
- ASP.NET Core MVC
- JWT / Cookie Authentication

## Arquitetura
O projeto segue uma divisão em camadas para separação de responsabilidades:

- **Core**: Contém as entidades de domínio, interfaces de serviço/repositório, serviços de negócio e a lógica de paginação.
- **Infrastructure**: Implementação do contexto de banco de dados (EF Core), repositórios concretos e middlewares de exceção e autenticação.
- **Web**: Camada de apresentação composta por Controllers, Views, DTOs e Mappers.

## Funcionalidades
- Autenticação e Autorização baseada em perfis (Roles).
- CRUD de Usuários e Pessoas.
- Gerenciamento de upload de imagens.
- Paginação genérica de listas.

## Execução

### Primeira Configuração
Para aplicar as migrações iniciais do banco de dados:
```bash
./exec-scripts/first-migration.sh
```

### Modo de Desenvolvimento
Para iniciar a aplicação em ambiente de desenvolvimento:
```bash
./exec-scripts/run-dev.sh
```

## Estrutura de Pastas
- `/Core`: Lógica de negócio e interfaces.
- `/Infrastructure`: Acesso a dados e infraestrutura técnica.
- `/Web`: Interface do usuário e controle de requisições.
- `/wwwroot`: Recursos estáticos e uploads.
