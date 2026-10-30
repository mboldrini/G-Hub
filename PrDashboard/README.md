# PrDashboard

Dashboard de Pull Requests do GitHub para Windows. Abre um painel web local em `localhost:5555` com seus PRs abertos, reviews pendentes, CI status e histórico de merges — com atualização automática e notificações de áudio/visuais.

## Requisitos

- [GitHub CLI (`gh`)](https://cli.github.com) no PATH, autenticado (`gh auth login`)
- [Microsoft Edge WebView2 Runtime](https://developer.microsoft.com/en-us/microsoft-edge/webview2/) (já instalado na maioria dos Windows 11)
- .NET 8+ **ou** use o `.exe` self-contained (não precisa de .NET instalado)

## Modos de uso

```
PrDashboard.exe              # Abre janela splash (WebView2), inicia servidor, minimiza para bandeja
PrDashboard.exe --once       # Busca uma vez, salva arquivos Markdown, sai (ideal para Agendador de Tarefas)
PrDashboard.exe --demo       # Dados falsos, sem chamar a API do GitHub
PrDashboard.exe --checkpoint # Salva JSON bruto dos PRs reais em fixtures/real/
PrDashboard.exe --help       # Ajuda
```

## Configuração (`config.json`)

```json
{
  "intervalSeconds": 300,
  "outputDir": "./out",
  "cacheDir": "./cache",
  "historyDays": 30,
  "hostname": "github.com",
  "ignoredOrgs": [],
  "ignoredRepos": ["org/repo-ruido"],
  "hideDrafts": false,
  "hideBotReviews": false,
  "webPort": 5555,
  "notifCommentVisual": true,
  "notifCommentAudio": true,
  "notifCiVisual": true,
  "notifCiAudio": true,
  "notifMergedVisual": true,
  "notifMergedAudio": true,
  "notifApprovedVisual": true,
  "notifApprovedAudio": true,
  "notifReadyVisual": true,
  "notifReadyAudio": true
}
```

## Arquivos de saída (`./out/`)

| Arquivo/Pasta | Descrição |
|---------------|-----------|
| `<Mês>/<semana>/` | Logs de app organizados por semana |
| `<Mês>/<semana>/notifications/` | Logs de notificação (JSON por dia) |
| `prs/open/<repo>--<number>.json` | Snapshot JSON de cada PR aberto |
| `prs/merged/<yyyy-MM>/<repo>--<number>.json` | Snapshot JSON de PRs mergeados |

Cache incremental por repo em `./cache/repos/`.

## Publicar como .exe único (self-contained)

```powershell
dotnet publish PrDashboard -c Release -r win-x64 --self-contained true `
  -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true -o publish/
```

O executável ficará em `publish/PrDashboard.exe`. Mantê-lo junto com `config.json` e `aspnetcorev2_inprocess.dll`.

## Troubleshooting

**SSO/SAML — erro ao acessar organização:**
```
gh auth refresh -h github.com -s read:org
```
Depois autorize SSO no browser que abrir.

**`GH_TOKEN` ou `GITHUB_TOKEN` sobrescrevendo o login do gh:**
```powershell
Remove-Item Env:GH_TOKEN
```

**WebView2 não encontrado:**
Instale o [Microsoft Edge WebView2 Runtime](https://developer.microsoft.com/en-us/microsoft-edge/webview2/) e tente novamente.

**SmartScreen/antivírus bloqueando o .exe:**
Clique em "Mais informações → Executar assim mesmo", ou assine com um certificado de code signing.
