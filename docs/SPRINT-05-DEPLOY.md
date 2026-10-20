# Sprint 05 — Deploy e Recuperação

## 1. Objetivo

Documentar os procedimentos de implantação manual, validação pós-deploy e recuperação da aplicação OSTech Work Order System.

## 2. Configuração atual

| Item | Configuração |
|---|---|
| API | Render |
| Frontend | Netlify |
| Banco de dados | MySQL — Aiven |
| Branch de produção | `master` |
| Deploy automático no Render | Desativado |
| Deploy automático no Netlify | Desativado |
| Health check da API | `GET /health` |

## 3. Health check

A API possui o endpoint `GET /health`, que retorna HTTP 200 e um JSON com o status da aplicação.

Resposta esperada:

```json
{
  "status": "Healthy"
}
```

### Validações realizadas

- [x] Teste local com resposta HTTP 200.
- [x] Teste em produção após deploy manual no Render.
- [x] CI aprovado antes do merge da implementação.

**Limitação:** o endpoint verifica apenas se a aplicação responde. Ele não confirma a disponibilidade do banco MySQL nem a saúde de todas as dependências.

## 4. Procedimento de deploy manual

### API — Render

1. Confirmar que as alterações foram integradas à branch `master`.
2. Conferir se os checks do CI passaram.
3. Acessar o serviço da API no Render.
4. Selecionar a opção de deploy manual e implantar o commit desejado.
5. Aguardar a conclusão da implantação.
6. Acessar https://ostech-workorder-system.onrender.com/health.
7. Confirmar que a resposta é HTTP 200 e contém `"status": "Healthy"`.
8. Verificar os logs do serviço caso a implantação falhe ou o endpoint não responda como esperado.

**Status:** procedimento executado com sucesso para a implementação do endpoint `/health`.

### Frontend — Netlify

1. Confirmar que as alterações estão integradas à branch de produção.
2. Acessar o projeto no Netlify.
3. Usar a opção de implantação manual disponível no painel.
4. Aguardar a conclusão do deploy.
5. Abrir a URL de produção e verificar se a página carrega corretamente.
6. Testar as funcionalidades principais e a comunicação com a API.

**Status:** deploy manual executado com sucesso. A implantação automática permanece desativada.

**Observação:** o deploy manual foi validado, mas ainda é necessário registrar testes funcionais detalhados do frontend e da comunicação com a API.

## 5. Procedimento de rollback — Render

O rollback permite retornar a uma implantação anterior quando a versão atual apresenta problemas.

1. Acessar o serviço da API no Render.
2. Abrir a seção de deploys.
3. Localizar uma implantação anterior bem-sucedida.
4. Verificar se a opção `Rollback` está disponível para aquela implantação.
5. Confirmar que a versão selecionada é a desejada antes de executar a ação.
6. Aguardar a conclusão da recuperação.
7. Acessar o endpoint `/health` e confirmar a resposta HTTP 200.
8. Testar uma funcionalidade da API e consultar os logs.

**Atenção:** não executar rollback sem identificar a versão de destino. A disponibilidade da opção pode variar entre implantações.

### Estado do teste

- [x] Localização da opção de rollback identificada no painel.
- [ ] Execução de rollback real validada.
- [ ] Funcionamento da API após rollback validado.

A opção foi localizada, mas nenhum rollback real foi executado. A recuperação permanece pendente de validação.

## 6. Banco de dados — Aiven

Antes de mudanças que possam afetar a estrutura ou os dados:

- Confirmar a existência de backup adequado e conhecer o procedimento de restauração.
- Avaliar o impacto de migrations antes de aplicá-las.
- Não utilizar credenciais de produção em testes locais ou no CI sem necessidade.
- Não registrar senhas, tokens ou strings de conexão com credenciais neste documento.

O rollback da aplicação não garante a reversão das alterações no banco de dados. Migrations destrutivas exigem um plano de recuperação específico.

### Estado dos backups

- Backups completos disponíveis no painel do Aiven.
- Localização informada: `do-sfo2`.
- Backups listados em 17, 18 e 19 de outubro de 2026.
- Logs de transações armazenados junto aos backups, com suporte à recuperação point-in-time.
- O recurso `Fork service` não está disponível no plano atual.

**Limitação:** o procedimento de restauração ainda não foi validado. Nenhuma restauração foi executada em produção. Antes de uma recuperação real, consultar a documentação do Aiven para confirmar as opções disponíveis no plano contratado e o impacto sobre os dados existentes.

## 7. Critérios de conclusão do Sprint 05

- [x] Deploy automático mantido desativado durante a validação.
- [x] Endpoint `/health` implementado e testado localmente.
- [x] Endpoint `/health` validado em produção.
- [x] CI aprovado antes do merge da implementação.
- [x] Deploy manual da API validado.
- [x] Deploy manual do frontend validado.
- [x] Backups disponíveis identificados no Aiven.
- [x] Opção de rollback localizada no Render.
- [x] Página inicial do frontend validada em produção.
- [x] Lista de ordens de serviço carregada corretamente.
- [x] Operação de consulta validada.
- [x] Console do navegador verificado, sem erros relevantes.
- [ ] Rollback real testado em condições seguras ou procedimento alternativo formalmente validado.
- [ ] Procedimento de restauração do Aiven confirmado.

### Resultado

Os procedimentos de deploy manual da API e do frontend foram executados com sucesso. O endpoint de saúde e as principais consultas do frontend foram validados em produção.

A recuperação permanece parcialmente validada: a opção de rollback foi localizada no Render e os backups foram identificados no Aiven, mas não foram executados testes reais de rollback ou restauração do banco de dados.

Os deploys automáticos permanecem desativados até que a estratégia de recuperação seja melhor definida.


## 8. Observações

A automação de deploy deve ser considerada somente após a validação do fluxo manual, dos critérios de saúde da aplicação, das permissões e da estratégia de recuperação.

O ambiente GitHub `production` só aplicará suas proteções a um job de deploy quando o workflow declarar explicitamente esse ambiente.

Manter os deploys automáticos desativados permite controlar as implantações enquanto os procedimentos de recuperação ainda não foram totalmente validados.
