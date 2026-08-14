# OSTech — React

Frontend da plataforma **OSTech**, um sistema de gerenciamento de ordens de serviço desenvolvido com React e integrado a uma API REST em ASP.NET Core.

Projeto construído para consolidar conceitos de React, consumo de APIs com TanStack Query, componentização, validação, responsividade e organização de uma aplicação frontend.

---

## 📸 Screenshots

### Dashboard
![Dashboard](./docs/screenshots/dashboard.png)

### Ordens de Serviço
![WorkOrder](./docs/screenshots/workorder.png)

### Listagem de Clientes
![Customers](./docs/screenshots/customers.png)

### Responsivo — Mobile
![Mobile](./docs/screenshots/mobile.png)


## 📌 Sobre o projeto

O OSTech gerencia:

- Categorias
- Clientes
- Técnicos
- Equipamentos
- Ordens de Serviço

A aplicação cobre operações completas de **CRUD**, relacionamentos entre entidades, validação de formulários, tratamento de erros de API, feedback visual e navegação entre páginas.

---

## 🚀 Funcionalidades

### Dashboard
- Totais por entidade: clientes, técnicos, equipamentos, categorias, ordens de serviço
- Ordens de serviço por status

### Categorias / Clientes / Equipamentos / Técnicos
- Listagem com TanStack Query
- Criação com `useMutation`
- Edição com `useMutation`
- Exclusão com `useMutation`
- Invalidação automática das queries após alterações
- Validação dos dados
- Feedback visual das operações

### Técnicos
- Controle de disponibilidade (boolean)

### Ordens de Serviço
- Listagem, criação, edição, exclusão via TanStack Query
- Relacionamento com cliente, técnico, categoria e equipamento
- Validação dos dados antes do envio
- Tratamento de erros retornados pela API

### Navegação
- React Router com rotas para todas as entidades
- Página inicial (Home) com acesso às entidades
- Página 404 para rotas inexistentes
- Botão de retorno à Home nas páginas internas

---

## 🛠️ Tecnologias

### Frontend
- React
- React Router
- Tailwind CSS
- TanStack Query
- Axios
- React Toastify
- Phosphor Icons

### Backend
- ASP.NET Core (Web API)

> O backend é um projeto separado, consumido pelo frontend via HTTP.

---

## 🧱 Arquitetura

As chamadas à API são organizadas por entidade em services independentes:

- `categoryService`
- `customerService`
- `equipmentService`
- `technicianService`
- `workOrderService`

Os componentes e páginas utilizam esses services em conjunto com o TanStack Query, mantendo a comunicação com a API separada da camada de interface.

---

## 📂 Estrutura do projeto

```text
src/
├── components/
│   ├── Buttons/
│   ├── Container/
│   ├── EmptyState/
│   ├── ErrorState/
│   ├── Header/
│   ├── Loading/
│   ├── Modal/
│   └── ...
│
├── hooks/
│   ├── useModals.js
│   └── useRequestState.js
│
├── pages/
│   ├── Home/
│   ├── Dashboard/
│   ├── Category/
│   ├── Customer/
│   ├── Equipment/
│   ├── Technician/
│   ├── WorkOrder/
│   └── NotFound/
│
├── services/
│   ├── api.js
│   ├── categoryService.js
│   ├── customerService.js
│   ├── equipmentService.js
│   ├── technicianService.js
│   └── workOrderService.js
│
├── validations/
│   ├── categoryValidation.js
│   ├── customerValidation.js
│   ├── equipmentValidation.js
│   ├── technicianValidation.js
│   └── workOrderValidation.js
│
├── utils/
│   └── apiError.js
│
├── routes.jsx
├── Global.css
└── index.jsx
```

---

## 🧩 Componentização

Componentes reutilizáveis usados nas páginas de entidade:

- `Container`, `Header`, `Loading`, `EmptyState`, `ErrorState`
- `Modal` (genérico, com scroll interno para formulários grandes)
- `CreateButton`, `ActionsButtons`

Os modais de cada entidade são separados por responsabilidade (`Create*`, `Edit*`, `Delete*`), mantendo os componentes de página mais enxutos.

---

## 🔄 Gerenciamento de dados

TanStack Query é utilizado para:

- Buscar dados com `useQuery`
- Criar registros com `useMutation`
- Atualizar registros com `useMutation`
- Excluir registros com `useMutation`
- Invalidar queries após alterações
- Cache das consultas
- Refetch automático após invalidação
- Controlar estados de loading e erro das requisições diretamente pelas queries/mutations

---

## 🔄 Hooks personalizados

- **`useModals`** — centraliza o estado de abertura/fechamento dos modais de criar, editar e excluir (`isCreateOpen`, `isEditOpen`, `isDeleteOpen` + funções `open*`/`close*`).
- **`useRequestState`** — centraliza os estados de validação que permanecem necessários nas páginas (`errors`/`setErrors`), já que loading, submitting e erro de requisição passaram a ser controlados pelo TanStack Query.

---

## 🔗 Relacionamentos

Ordem de Serviço se relaciona com Cliente, Técnico, Categoria e Equipamento. Na criação/edição, o usuário seleciona as entidades via `<select>`, e os IDs correspondentes são enviados no payload:

```json
{
    "customerId": 1,
    "technicianId": 2,
    "categoryId": 3,
    "equipmentId": 4
}
```

---

## ✅ Validação

Cada entidade tem uma função de validação própria (`validations/`), executada antes do envio à API. Erros são armazenados em estado e exibidos junto ao campo correspondente:

```javascript
const validationErrors = validateWorkOrder(workOrder);

if (Object.keys(validationErrors).length > 0) {
    setErrors(validationErrors);
    return;
}
```

---

## ⚠️ Tratamento de erros

Erros de API são interpretados por um utilitário central:

```javascript
getApiErrorMessage(error)
```

E exibidos via toast:

```javascript
toast.error(getApiErrorMessage(error));
```

---

## 🔔 Feedback visual

React Toastify é usado para confirmar criação, edição, exclusão e reportar erros de operações.

---

## 📱 Responsividade

A interface foi revisada para diferentes tamanhos de tela, com atenção especial a dispositivos a partir de **360px**.

Foram revisados:

- Header e navegação
- Dashboard
- Listagens
- Formulários
- Modais
- Cards e botões de ação
- Espaçamentos e hierarquia visual

---

### Passos

```bash
git clone <URL_DO_REPOSITORIO>
cd OSTech.React
npm install
npm start
```

Configure a URL da API em `src/services/api.js` de acordo com o ambiente local.

---

## 📚 Objetivos do projeto

- Praticar React em um projeto de porte real
- Consumir uma API REST própria
- Trabalhar com CRUD, relacionamentos entre entidades e validação de formulários
- Migrar o gerenciamento de estado assíncrono para TanStack Query
- Criar hooks personalizados para reduzir duplicação entre páginas
- Construir uma interface responsiva com Tailwind CSS

---

## 📄 Licença

Projeto desenvolvido para fins educacionais e de portfólio.