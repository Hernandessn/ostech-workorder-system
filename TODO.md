# Débitos técnicos conhecidos

- `OSTech.EFCore/OSTech.Infrastructure.csproj`: nome do arquivo não bate com o nome da pasta do projeto (histórico de renomeação incompleta). Não quebra nada, mas confunde quem for procurar por convenção de nome. Avaliar renomear pasta ou arquivo para consistência.
- `HttpsRedirection`: removido do pipeline em `Program.cs` porque a aplicação roda em container atrás de proxy reverso que termina TLS. Se rodar sem proxy no futuro, reavaliar.