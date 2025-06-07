# 🧩 API .NET com Docker

Este projeto é uma API RESTful desenvolvida com .NET 9.0, estruturada em camadas e preparada para execução em containers Docker.

---

## 📦 Estrutura da Solução

A solução está dividida nos seguintes projetos:

- `Api/` – Projeto principal da API
- `Application/` – Camada de aplicação
- `Application.Dto/` – Objetos de transferência de dados
- `Domain/` – Entidades de domínio
- `Domain.Common/`, `Domain.Enums/`, `Domain.Filter/` – Complementos do domínio
- `Infrastructure.Data/` – Acesso a dados e implementação de repositórios

---

## 🚀 Executando com Docker

### ✔️ Pré-requisitos

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Docker](https://www.docker.com/)

---

### 🏗️ Construção da imagem

Abra o terminal na raiz do projeto (onde está o `Dockerfile`) e execute:

```bash
docker build -t Api .
