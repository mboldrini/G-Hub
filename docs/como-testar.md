# Como Testar as Funcionalidades

Todas as funcionalidades abaixo estão no branch `main`. Use o modo `--demo` para testar a UI sem precisar de credenciais do GitHub.

```
dotnet run --project PrDashboard/PrDashboard.csproj -- --demo
```

---

## Launcher gráfico (WebView2)

1. Execute `PrDashboard.exe` sem argumentos (ou `dotnet run --project PrDashboard`)
2. Uma janela splash aparece com barra de progresso enquanto os pré-checks rodam
3. Quando ficar verde, clique **Launch** para abrir o browser no dashboard
4. Minimizar a janela esconde para a bandeja do sistema; duplo-clique no ícone restaura

**Testar erro de pré-check:**
1. Remova temporariamente o `gh.exe` do PATH
2. A janela splash deve mostrar o erro e o comando de correção (`winget install --id GitHub.cli`)

---

## Filtro de texto

1. Em qualquer aba (Meus PRs, Reviews, Mergeados), há uma caixa de busca no topo
2. Digite parte de um título ou nome de repo — cards que não batem somem imediatamente
3. Limpe o campo para restaurar todos os cards

---

## Cor progressiva de idade

No modo demo os PRs têm idades variadas. O badge de idade deve aparecer:
- **Verde** → criado há menos de 1 dia
- **Amarelo** → 1 a 3 dias
- **Vermelho** → mais de 3 dias

---

## Cards expansíveis (descrição + comentários)

1. Clique no chevron à esquerda de qualquer card de PR
2. Um painel expande abaixo com duas colunas: **Descrição** (Markdown renderizado) e **Comentários** (ordenados do mais recente)
3. Se houver mais de um comentário, aparece "+ N mais" para expandir os anteriores
4. Clique no chevron novamente para fechar

---

## CI checks detalhados

1. No modo real, abra um PR com CI falhando
2. Passe o mouse sobre o badge vermelho de CI — um tooltip deve listar **todos** os checks com falha separados por vírgula

---

## Assignees nos cards

1. No modo real, abra um PR que tenha alguém assignado
2. O card exibe um badge `→ login-do-usuario` na linha de metadados
3. No modo demo, confira via `GET http://localhost:5555/api/prs` — campo `assignees`

---

## Notificações de aprovação e saída de Draft

1. Abra o menu de usuário (círculo no canto superior direito)
2. Em **Notificações** devem aparecer 5 grupos: Comentário, CI, Merge, Aprovado, Pronto p/ review
3. Em **Testar notificações**, clique em **Aprovado** e em **Draft** — os toasts devem aparecer e o áudio tocar
4. Os eventos aparecem na aba **Notificações** do dashboard

---

## Aba de histórico de notificações

1. Abra o dashboard — deve haver uma 4ª aba "Notificações" com contador
2. Dispare notificações de teste pelo menu de usuário
3. Os eventos listam: hora, repo, número e título clicável
4. O botão **Limpar** zera a lista em memória

---

## Logger de notificações (arquivo)

1. Rode em modo real e aguarde ou dispare notificações de teste via menu
2. Navegue até `out/<Mês>/<semana>/notifications/`
3. Deve existir um arquivo `notifications-YYYYMMDD.log` com uma linha JSON por evento:
   ```json
   {"timestamp":"...","type":"ci","repo":"org/repo","prNumber":123,"prTitle":"..."}
   ```

---

## Backfill de PRs (modal)

1. Abra o menu de usuário → **Backfill PRs**
2. Selecione um mês e clique em **Buscar**
3. Barra de progresso e log em tempo real mostram o andamento
4. Arquivos são salvos em `out/prs/merged/<yyyy-MM>/` e `out/prs/open/`
5. PRs já existentes são ignorados automaticamente

---

## Testes unitários

```powershell
dotnet test PrDashboard.Tests/PrDashboard.Tests.csproj
```

Resultado esperado: **19/19 testes passando**.
