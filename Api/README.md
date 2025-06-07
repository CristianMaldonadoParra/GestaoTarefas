# API .NET com Docker

Este projeto é uma API RESTful desenvolvida com .NET 9.0, estruturada em camadas e preparada para execução em containers Docker.

---

## Estrutura da Solução

A solução está dividida nos seguintes projetos:

- `Api/` – Projeto principal da API
- `Application/` – Camada de aplicação
- `Application.Dto/` – Objetos de transferência de dados
- `Domain/` – Entidades de domínio
- `Domain.Common/`, `Domain.Enums/`, `Domain.Filter/` – Complementos do domínio
- `Infrastructure.Data/` – Acesso a dados e implementação de repositórios

---

## Executando com Docker

### Pré-requisitos

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Docker](https://www.docker.com/)

---

### Construção da imagem

Abra o terminal na raiz do projeto (onde está o `Dockerfile`) e execute:

```bash
docker build -t Api .







---

### Refinamento com o PO

Durante o desenvolvimento da API, algumas decisões podem ser melhor embasadas com a colaboração do Product Owner (PO). Abaixo está uma sugestão de pergunta para discussão em sessões de refinamento, com o objetivo de antecipar melhorias e alinhar o comportamento esperado do sistema:

### ❓ Pergunta para Refinamento

> **"O limite máximo de tarefas por projeto deve considerar tarefas com qual status?**
>
> - Apenas tarefas ativas?
> - Tarefas já concluídas também devem ser contabilizadas?
> - Em caso de reabertura de tarefas concluídas, o limite deve ser reavaliado?"

### 💡 Sugestão Inicial

Por padrão, **não considerar tarefas concluídas** para o limite máximo de tarefas pode ser mais flexível para o usuário final, evitando bloqueios desnecessários em projetos já encerrados. No entanto, é essencial alinhar esse comportamento com as regras de negócio definidas pelo PO.

---

