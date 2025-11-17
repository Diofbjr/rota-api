# 🚀 Rota API --- Otimização Inteligente de Rotas Logísticas

![.NET](https://img.shields.io/badge/.NET-9.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![Status](https://img.shields.io/badge/Status-Em%20Desenvolvimento-blue?style=for-the-badge)

------------------------------------------------------------------------

## 📦 Sobre o Projeto

A **Rota API** é uma aplicação construída em **.NET 9** para otimização
inteligente de rotas logísticas, permitindo cálculos de distância,
tempo, custos, autonomia e restrições reais de transporte.

Possui: - Criação de rotas com múltiplos pontos\
- Gerenciamento de veículos com restrições reais\
- Cálculo de distância, tempo e custo\
- Validações avançadas\
- Registro de histórico de cálculos\
- Testes unitários e de integração

------------------------------------------------------------------------

## 📚 Sumário

-   Tecnologias Utilizadas\
-   Funcionalidades da API\
-   Arquitetura do Projeto\
-   Endpoints Principais\
-   Como Rodar o Projeto\
-   Como Rodar os Testes\
-   Modelos Importantes\
-   Testes Automatizados\
-   Contribuição\
-   Licença

------------------------------------------------------------------------

## 🛠 Tecnologias Utilizadas

-   .NET 9\
-   Entity Framework Core\
-   FluentValidation\
-   SQL Server\
-   Swagger\
-   xUnit\
-   WebApplicationFactory\
-   InMemory Database

------------------------------------------------------------------------

## 🧭 Funcionalidades

### 1. 🚗 Veículos

Cada veículo inclui: 
- Peso máximo\
- Volume máximo\
- Altura máxima\
- Autonomia\
- Custo por km e hora\
- Tipo (Carro, Van, Caminhão etc.)

### 2. 🗺 Rotas com múltiplos pontos

Inclui waypoints com: - Latitude\
- Longitude\
- Ordem da parada

### 3. 🎯 Cálculo Inteligente

Realiza: - Distância total (Haversine)\
- Tempo estimado\
- Custos avançados\
- Restrições reais (peso, volume, autonomia)

### 4. 📊 Histórico automático

Armazena: 
- Distância\
- Tempo\
- Custo\
- Caminho serializado\
- Data/Hora

### 5. 🧪 Validações robustas

FluentValidation garantindo integridade de dados.

### 6. 🧩 Testes Automatizados

-   Testes de integração\
-   Testes unitários\
-   Banco InMemory

------------------------------------------------------------------------

## 🏗 Arquitetura

    Rota-api/
    │
    ├── Rota.Api/              
    │   ├── Domain/              
    │   ├── Data/                
    │   ├── Dtos/                
    │   ├── Validators/          
    │   └── Program.cs           
    │
    └── Rota.Tests/              
        ├── IntegrationTests/
        ├── UnitTests/
        ├── TestServerFactory.cs
        └── Usings.cs

------------------------------------------------------------------------

## 📡 Endpoints Principais

### 🚗 Veículos
| Método | Rota | Descrição |
|--------|-------|-----------|
| GET | /vehicles | Listar |
| GET | /vehicles/{id} | Buscar |
| POST | /vehicles | Criar |
| PUT | /vehicles/{id} | Atualizar |
| DELETE | /vehicles/{id} | Remover |

### 🗺 Rotas
| Método | Rota | Descrição |
|--------|-------|-----------|
| GET | /route-requests | Listar |
| GET | /route-requests/{id} | Buscar |
| POST | /route-requests | Criar |
| PUT | /route-requests/{id} | Atualizar |
| DELETE | /route-requests/{id} | Remover |
| POST | /route-requests/{id}/calculate | Calcular rota |

### 📊 Resultados
| Método | Rota | Descrição |
|--------|-------|-----------|
| GET | /route-results | Listar |
| GET | /route-results/{id} | Buscar |


------------------------------------------------------------------------

## ⚙ Como Rodar o Projeto

### 1. Clonar repositório

    git clone https://github.com/Diofbjr/rota-api

### 2. Configurar `appsettings.json`

    "ConnectionStrings": {
      "DefaultConnection": "Server=localhost;Database=RotaDb;Trusted_Connection=True;TrustServerCertificate=True;"
    }

### 3. Rodar migrações

    dotnet ef database update

### 4. Rodar API

    dotnet run

Acesse Swagger: 
- http://localhost:5000/swagger\
- https://localhost:7000/swagger

------------------------------------------------------------------------

## 🧪 Rodando os Testes

    cd Rota.Tests
    dotnet test

------------------------------------------------------------------------

## 📦 Modelos

### Vehicle

-   Peso máximo\
-   Volume máximo\
-   Altura\
-   Autonomia\
-   Custos

### RouteRequest

-   Peso\
-   Volume\
-   Veículo\
-   Waypoints

### RouteResult

-   Distância\
-   Tempo\
-   Custo final\
-   Caminho

------------------------------------------------------------------------

## 🤝 Contribuição

1.  Crie uma branch\
2.  Commits semânticos\
3.  Abra PR

Pull requests são bem-vindos!

------------------------------------------------------------------------

## 📄 Licença

MIT License.

------------------------------------------------------------------------

## 🎉 Obrigado por conferir!

Se quiser, posso gerar: 
- CI/CD\
- Dashboard React\
- Deploy Azure\
- Algoritmos avançados
