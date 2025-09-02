├── Backend/ExameNet8/          # API RESTful .NET 8 + EF Core 8
│   ├── Exame.Domain/           # Entidades, interfaces e contratos
│   ├── Exame.Application/      # DTOs e regras de aplicação (services)
│   ├── Exame.Infrastructure/   # DbContext, Repositórios, UoW
│   ├── Exame.Api/              # ASP.NET Core Web API com Swagger
│   └── Exame.Tests/            # Testes unitários com xUnit + Moq
│
├── Frontend/APP/               # Aplicação Angular 17 organizada por camadas
│   └── src/
│       ├── app/
│       │   ├── core/           # Serviços, interceptors
│       │   ├── shared/         # Modelos tipados
│       │   └── features/       # Componentes de listagem e formulário
│
├── Database/
│   └── 001_create_and_seed.sql  # Script SQL com estrutura e dados de seed
