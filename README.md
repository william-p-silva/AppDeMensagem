# AppDeMensagem 💬

Projeto acadêmico de estudo, construído como exercício de **Clean Architecture** em .NET, inspirado nos padrões da Microsoft e de autores como Steve "ardalis" Smith. O objetivo é replicar, de forma simplificada, os conceitos centrais de um app de mensageria estilo Signal/WhatsApp: usuários, conversas privadas, conversas em grupo e troca de mensagens.

> ⚠️ Projeto com foco **didático**. As decisões arquiteturais aqui documentadas priorizam aprendizado (herança, Value Objects, Repository Pattern, Unit of Work) mesmo quando uma solução mais simples seria suficiente para o escopo real do app.

---

## 🎯 Objetivo do projeto

Construir um backend funcional, testável e desacoplado, entendendo **por que** cada classe está em sua respectiva camada — sem copiar padrões de forma mecânica.

---

## 🏗️ Arquitetura

O projeto segue os princípios da **Clean Architecture**, com separação rigorosa de responsabilidades entre camadas:

```
AppDeMensagem/
├── AppDeMensagem.Domain/          → Núcleo do sistema
├── AppDeMensagem.Application/     → Casos de uso e contratos
├── AppDeMensagem.Infrastructure/  → Persistência e serviços externos
└── AppDeMensagem.Web/             → API, Controllers e Middlewares
```

### Domain (Núcleo)
Entidades, Value Objects e regras de negócio puras. **Não depende de nenhuma outra camada** e não conhece Entity Framework, ASP.NET ou qualquer biblioteca externa.

### Application
Casos de uso (Use Cases), DTOs e interfaces de repositório (`IUserRepository`, `IUnitOfWork`). Orquestra o Domain sem saber como a persistência é implementada de fato.

### Infrastructure
Implementação concreta dos repositórios, `DbContext` (Entity Framework Core + SQL Server), configurações de mapeamento (`IEntityTypeConfiguration`) e serviços externos (ex: hashing de senha).

### Web (Apresentação)
Controllers, `Program.cs`, injeção de dependência e configuração de Middlewares.

---

## 🧩 Modelo de domínio

### Entidades

| Entidade | Descrição |
|---|---|
| `Usuario` | Representa uma pessoa cadastrada no sistema (login, perfil). |
| `Chat` (abstrata) | Base comum para qualquer tipo de conversa: possui mensagens e participantes. |
| `ChatPrivate : Chat` | Conversa 1:1. Nasce com exatamente 2 participantes, fixados na criação — nunca mudam depois. |
| `ChatGroup : Chat` | Conversa em grupo. Participantes podem ser adicionados dinamicamente; possui conceito de admin. |
| `UserChat` | Entidade associativa (N:N) entre `Usuario` e `Chat`, carregando dados próprios da relação (ex: `IsAdmin`). |
| `Message` | Mensagem enviada em um `Chat`, com status de entrega (`Sent`, `Delivered`, `Read`). |

### Value Objects

| Value Object | Descrição |
|---|---|
| `Email` | Garante formato de e-mail válido na criação. Imutável. |
| `Name` | Garante que o nome contenha apenas letras/acentos/espaços válidos. Imutável. |

### Decisões arquiteturais relevantes

- **Herança em `Chat`**: optou-se por modelar `ChatPrivate` e `ChatGroup` como subclasses de `Chat`, ao invés de um único tipo com enum discriminador, como exercício prático de herança e mapeamento no EF Core (TPH/TPT).
- **Encapsulamento de coleções**: listas internas (`_messages`, `_usersChat`) são privadas; expostas externamente apenas como `IReadOnlyCollection<T>`, evitando que código fora da entidade burle as regras de negócio (ex: adicionar participante duplicado).
- **`AddParticipant` como `protected`**: método de manipulação da lista de participantes fica acessível apenas para a própria hierarquia de `Chat`, forçando cada subclasse a expor (ou não) essa operação através de sua própria regra de negócio.
- **Mensagem em grupo**: por simplicidade, o status "lida" é considerado satisfeito assim que **pelo menos um** participante lê a mensagem (trade-off consciente, documentado — não há rastreamento de leitura por pessoa em grupos, por enquanto).
- **`PasswordHash`**: o `Domain` recebe o hash já pronto (nunca a senha em texto puro), preservando a regra de que o Domain não depende de bibliotecas externas de criptografia.

---

## 🛠️ Stack

- **.NET** (C#)
- **Entity Framework Core**
- **SQL Server**
- **Clean Architecture** + **Repository Pattern** + **Unit of Work**

---

## 📌 Status atual

- [x] Modelagem do Domain (entidades + Value Objects)
- [x] Definição de interfaces de repositório (`IUserRepository`, `IUnitOfWork`)
- [x] Configuração do `AppDbContext` e mapeamento das entidades (EF Core)
- [x] Implementação dos repositórios concretos
- [x] Primeiro Use Case (`RegisterUserUseCase`)
- [ ] Camada de apresentação (Controllers, autenticação)

---

## 📚 Aprendizados-chave documentados neste projeto

- Diferença entre Entidades (identidade) e Value Objects (igualdade de valor)
- Armadilhas de herança e o Princípio de Substituição de Liskov
- Encapsulamento real de coleções em entidades de domínio
- Inversão de Dependência: por que `Application` não deve depender de `Infrastructure`
- Trade-offs entre estratégias de mapeamento de herança no EF Core (TPH vs TPT vs TPC)

---

## 👤 Autor: William José Pereira da Silva

Projeto desenvolvido como estudo pessoal, mentorado com foco em fundamentos de arquitetura de software no ecossistema .NET.