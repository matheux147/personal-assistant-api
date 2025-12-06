# Personal Assistant API (.NET + Semantic Kernel)

> Uma API que transforma linguagem natural em ações de organização pessoal usando IA generativa.

Este projeto é um backend construído com **.NET 8/**, projetado para atuar como o núcleo de um assistente pessoal inteligente. Ele utiliza o **Microsoft Semantic Kernel** para interpretar intenções do usuário e executar funções nativas em C# para gerenciar **Tarefas** e **Contas Financeiras**.

---

## Tecnologias e Padrões

* **Framework:** .NET 8 / ASP.NET Core Web API
* **IA e Orquestração:** Microsoft Semantic Kernel (OpenAI Api)
* **Banco de Dados:** SQL Server (via Docker)
* **ORM:** Entity Framework Core (Code First)

### Arquitetura

* **CQRS** (Command Query Responsibility Segregation) com **MediatR**
* **Feature Slices:** Organização por funcionalidades, não por camadas técnicas
* **Result Pattern:** Controle de fluxo sem exceções para regras de negócio
* **Repository Pattern:** Abstração do acesso a dados

## Funcionalidades

### Inteligência Artificial (Agente)

* **Processamento de Linguagem Natural:**
  Interpreta comandos como:

  * “Agendar dentista amanhã às 14h”
  * “Adicionar conta de energia de 200 reais para dezembro”
  * “Quais meus afazeres de hoje?”
* **Function Calling Manual:**
  Utiliza o padrão moderno de *Tool Call*, onde a IA decide qual ferramenta utilizar, mas o backend executa e retorna o JSON estruturado para o frontend. Isso reduz consumo de tokens e permite uma UI mais rica.

---

### Gestão de Tarefas

* Criação inteligente com extração automática de:

  * Título
  * Data e hora
* Listagem de tarefas pendentes ou por período
* Conclusão de tarefas

---

### Gestão Financeira

* Registro de contas a pagar com extração de:

  * Descrição
  * Valor
* Listagem de contas pendentes
* Baixa e pagamento de contas
