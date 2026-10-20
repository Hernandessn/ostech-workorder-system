# CI/CD — OSTech Work Order System

## 1. Objetivo

Documentar o pipeline de Integração Contínua (CI/CD) do OSTech Work Order System, incluindo eventos de execução, validações automatizadas, construção de imagens Docker, configuração de variáveis, diagnóstico de falhas e procedimento de publicação em produção.

O pipeline automatiza verificações de qualidade e compilação. **O deploy em produção permanece manual**, tanto na API hospedada no Render quanto no frontend hospedado na Netlify.

## 2. Workflow

- **Arquivo:** `.github/workflows/ci.yml`
- **Nome:** OsTech in Github Actions
- **Branch principal:** `master`
- **Permissão:** `contents: read`

### Eventos que iniciam o workflow

| Evento | Quando ocorre |
|---|---|
| `push` | Quando há um push para `master`. |
| `pull_request` | Quando um pull request tem `master` como branch de destino. |
| `workflow_dispatch` | Quando o workflow é iniciado manualmente pela interface do GitHub Actions. |

## 3. Jobs do pipeline

Os jobs `api`, `frontend` e `docker` são executados em runners Ubuntu hospedados pelo GitHub. Cada job valida uma parte do projeto.

### 3.1. API — Restore, build e testes

O job `api` utiliza o .NET 9 e um serviço MySQL 8.4 para executar as seguintes etapas:

1. Baixar o código com `actions/checkout@v4`.
2. Configurar o SDK .NET com `actions/setup-dotnet@v4`.
3. Verificar a versão do SDK selecionada com `dotnet --version` e `dotnet --info`.
4. Restaurar as dependências com `dotnet restore OSTech.slnx`.
5. Compilar a solução com `dotnet build OSTech.slnx --no-restore`.
6. Instalar a ferramenta `dotnet-ef` na versão 9.0.0.
7. Aplicar as migrations no banco MySQL de teste.
8. Executar os testes com o filtro `FullyQualifiedName~UnitTests.Domain`.

**Limitação:** o filtro prioriza os testes do domínio e não representa a execução de toda a suíte de testes. O banco MySQL utilizado é temporário e destinado à CI; as migrations não são aplicadas ao banco de produção pelo workflow.

### 3.2. Frontend — Build React

O job `frontend` utiliza Node.js na versão definida pela variável `NODE_VERSION_NETLIFY` e trabalha no diretório `ostechreact`.

Etapas:

1. Baixar o código.
2. Configurar o Node.js.
3. Instalar as dependências com `npm ci`.
4. Compilar o frontend com `npm run build`.

A variável de ambiente `REACT_APP_API_URL` recebe o valor de `vars.REACT_APP_API_URL_CI`.

**Limitação:** o job verifica se o frontend pode ser compilado, mas não publica o resultado na Netlify nem substitui testes automatizados de interface ou testes funcionais em produção.

### 3.3. Docker — Construção das imagens

O job `docker` constrói duas imagens:

| Imagem | Dockerfile | Tag |
|---|---|---|
| API | `./Dockerfile` | `ostech-api:latest` |
| Frontend | `./ostechreact/Dockerfile` | `ostech-frontend:latest` |

A construção utiliza `docker/build-push-action@v6`, com `push: false`.

**Limitação:** as imagens são construídas para validação, mas não são enviadas para um registry. O workflow não publica imagens nem executa containers em produção.

## 4. Variáveis e credenciais

### Variáveis do GitHub Actions

| Nome | Tipo/uso |
|---|---|
| `REACT_APP_API_URL_CI` | Variável do GitHub Actions utilizada para fornecer a URL da API ao build do frontend e à construção da imagem Docker do frontend. |
| `NODE_VERSION_NETLIFY` | Variável definida no próprio workflow, com o valor `24.21.0`, para selecionar a versão do Node.js. |

A variável `REACT_APP_API_URL_CI` deve ser configurada em **Settings → Secrets and variables → Actions → Variables** no repositório. Seu valor deve apontar para a URL apropriada da API no contexto de CI.

### Credenciais e valores exclusivos de teste

O job da API utiliza valores de teste para o MySQL:

- Banco: `ostech_test`
- Usuário: `ostech`
- Senha de teste: definida no workflow
- String de conexão: `ConnectionStrings__DefaultConnection`
- Chave JWT de teste: `JWT__SecretKey`

Esses valores são destinados ao ambiente isolado da CI e não devem ser reutilizados em produção.

O workflow atual não declara referências a GitHub Actions Secrets. Não se deve inserir credenciais reais, senhas de produção, chaves privadas ou credenciais do Aiven, Render ou Netlify diretamente no código.

**Importante:** variáveis `REACT_APP_*` incorporadas ao frontend durante a compilação podem ficar visíveis no código entregue ao navegador. Portanto, elas não devem conter informações secretas.

## 5. Diagnóstico de falhas

Quando um job falhar, abra o repositório no GitHub e acesse **Actions → OsTech in Github Actions**. Selecione a execução correspondente e abra o job que apresentou erro.

### Falhas comuns

| Job | Possível causa | O que verificar |
|---|---|---|
| `api` | Falha ao restaurar dependências | Arquivos de solução, projetos referenciados e disponibilidade dos pacotes. |
| `api` | Falha na compilação | Mensagens do compilador e erros de configuração ou código. |
| `api` | MySQL indisponível | Estado de saúde do serviço, porta 3306 e configuração de conexão de teste. |
| `api` | Falha nas migrations | Projeto de infraestrutura, projeto de inicialização, versão do EF Core e configuração do banco. |
| `api` | Testes reprovados | Nome do teste, mensagem de erro e filtro utilizado. |
| `frontend` | Falha em `npm ci` | `package-lock.json`, dependências e versão do Node.js. |
| `frontend` | Falha em `npm run build` | Erros de compilação, imports, variáveis de ambiente e dependências. |
| `docker` | Falha ao construir uma imagem | Caminho do Dockerfile, contexto de build e instruções do Dockerfile. |

Corrija o problema na branch de trabalho e envie a alteração para uma nova execução do pipeline. Antes do merge, confirme que os checks obrigatórios do pull request passaram.

## 6. Caminho da alteração até a produção

O fluxo adotado pelo projeto é:

1. Desenvolver e testar a alteração em uma branch de trabalho.
2. Abrir um pull request com destino a `master`.
3. Aguardar os jobs da CI e analisar eventuais falhas.
4. Corrigir os problemas identificados e repetir as verificações.
5. Fazer o merge após a aprovação e a conclusão dos checks exigidos.
6. Confirmar que a execução do workflow em `master` foi concluída com sucesso.
7. Realizar manualmente o deploy da API no Render.
8. Realizar manualmente o deploy do frontend na Netlify.
9. Validar a aplicação publicada e os endpoints relevantes.

### Publicação da API — Render

- Abrir o serviço da API no Render.
- Conferir o commit que será publicado.
- Acionar o deploy manual conforme o procedimento da equipe.
- Verificar o resultado do deploy e consultar os logs em caso de falha.
- Validar o endpoint de saúde:

`https://ostech-workorder-system.onrender.com/health`

Resposta esperada:

```json
{
  "status": "Healthy"
}
```

### Publicação do frontend — Netlify

- Abrir o projeto `ostech-system` na Netlify.
- Acionar o deploy manual.
- Aguardar a conclusão da publicação.
- Abrir o site e verificar o carregamento da página inicial.
- Conferir a listagem de ordens de serviço e executar uma operação de consulta.
- Verificar erros relevantes no Console do navegador.

### Política de deploy

O deploy automático permanece desabilitado no Render e na Netlify. A CI não executa deploy, não aplica migrations em produção e não publica imagens Docker.

Essa separação permite validar as alterações antes de decidir quando publicar em produção.

## 7. Limitações e pontos de atenção

- Os testes da API são limitados pelo filtro definido no workflow.
- O build do frontend não equivale a testes completos de interface.
- As imagens Docker são construídas, mas não são publicadas em um registry.
- O workflow não realiza deploy automático.
- O workflow não aplica migrations no banco de produção.
- O rollback do Render e a restauração de backups do Aiven exigem procedimentos próprios.
- A disponibilidade de backups não comprova que a restauração foi testada com sucesso.
- O endpoint `/health` confirma a resposta da aplicação, mas não comprova sozinho a disponibilidade de todas as dependências, como o banco de dados.

## 8. Verificação de segurança

Antes de concluir a entrega:

- [x] Verificar se há senhas, tokens, chaves privadas ou credenciais reais versionadas nos arquivos examinados; nenhuma credencial real de produção foi identificada.
- [ ] Confirmar que os valores do MySQL e a chave JWT da CI são exclusivos de teste.
- [ ] Confirmar que a URL da API está configurada adequadamente nas variáveis do GitHub Actions.
- [ ] Conferir se as permissões do workflow permanecem mínimas.
- [ ] Verificar se nenhum job de CI publica imagens ou realiza deploy inadvertidamente.
- [ ] Confirmar que não foram adicionados arquivos `.env` com credenciais reais.

A documentação registra os nomes das variáveis e o propósito de cada uma, sem reproduzir valores secretos de produção.

## 9. Checklist de validação do Sprint 06

- [ ] Documentação do pipeline criada em `docs/ci-cd.md`.
- [ ] Eventos, jobs, variáveis e limitações documentados.
- [ ] Diagnóstico de falhas e procedimento de deploy documentados.
- [ ] Badge do workflow adicionado ao README.
- [ ] Fluxo de pull request até produção validado.
- [ ] Procedimento de clonagem e execução do README repetido.
- [ ] `TODO.md` atualizado com itens concluídos e pendentes.
- [ ] Repositório verificado quanto à presença de informações sensíveis.

**Critério de conclusão:** a documentação deve refletir o workflow existente, as verificações devem ser executadas com sucesso e as etapas de publicação devem continuar explícitas e controladas manualmente.
