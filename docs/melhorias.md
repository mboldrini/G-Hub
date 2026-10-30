# PrDashboard — Melhorias e Funcionalidades

Sugestões de melhoria organizadas por categoria. Cada item explica **o que mudar** e **por que vale a pena**.

---

## Índice

- [UX e Interface](#ux-e-interface)
  - [1. Aba "Mergeados" com mais contexto](#1-aba-mergeados-com-mais-contexto)
  - [2. Filtro de texto livre nos PRs](#2-filtro-de-texto-livre-nos-prs)
  - [3. Indicador de "PR travado há muito tempo"](#3-indicador-de-pr-travado-há-muito-tempo)
  - [4. Contador de dias/horas no card do PR](#4-contador-de-diashoras-no-card-do-pr)
- [Notificações](#notificações)
  - [5. Notificação nativa do Windows (Toast)](#5-notificação-nativa-do-windows-toast)
  - [6. Notificar quando alguém aprova seu PR](#6-notificar-quando-alguém-aprova-seu-pr)
  - [7. Notificar quando o PR sai do estado Draft](#7-notificar-quando-o-pr-sai-do-estado-draft)
  - [8. Histórico de notificações na UI e em logs dedicados](#8-histórico-de-notificações-na-ui-e-em-logs-dedicados)
- [Dados e Integração com GitHub](#dados-e-integração-com-github)
  - [9. Mostrar checks de CI com nome detalhado](#9-mostrar-checks-de-ci-com-nome-detalhado)
  - [10. Mostrar quem foi assignado ao PR](#10-mostrar-quem-foi-assignado-ao-pr)
  - [11. Histórico de tempo médio em review por repo](#11-histórico-de-tempo-médio-em-review-por-repo)
- [Confiabilidade e Infraestrutura](#confiabilidade-e-infraestrutura)
  - [12. Limpeza de código morto](#12-limpeza-de-código-morto)
  - [13. Separar WebAssets.cs em arquivo HTML/CSS/JS real](#13-separar-webassetscs-em-arquivo-htmlcssjs-real)
  - [14. Persistência de settings em config.json em vez de localStorage](#14-persistência-de-settings-em-configjson-em-vez-de-localstorage)
  - [15. Tela de erro mais informativa no SplashWindow](#15-tela-de-erro-mais-informativa-no-splashwindow)

---

## UX e Interface

### 1. Aba "Mergeados" com mais contexto

**O que:** A aba "Mergeados" hoje só lista os PRs com repo, título e data. Falta mostrar quem fez o merge, para qual branch foi mergeado, e quantos dias ficou aberto antes do merge.

**Por que:** É a aba usada para retrospectiva rápida ("o que entregamos essa semana?"). Com esses dados ela vira uma linha do tempo útil, não só uma lista de nomes.

---

### ~~2. Filtro de texto livre nos PRs~~ ✅ feito

**O que:** Caixa de busca no topo de cada aba filtrando em tempo real pelo título ou nome do repo.

**Status:** implementado — disponível em todas as abas.

---

### 3. Indicador de "PR travado há muito tempo"

**O que:** Destacar visualmente PRs que estão abertos há mais de X dias (configurável) sem nenhuma atividade (sem novo commit, sem novo comentário, sem mudança de CI).

**Por que:** PRs velhos e sem movimento são o maior acumulador de dívida. Um badge "parado há 7d" chama atenção antes de virar problema de merge conflict.

---

### ~~4. Contador de dias/horas no card do PR~~ ✅ feito

**O que:** Badge colorido progressivo: verde (< 1 dia), amarelo (1–3 dias), vermelho (> 3 dias).

**Status:** implementado.

---

## Notificações

### 5. Notificação nativa do Windows (Toast)

**O que:** O `ToastNotifier.cs` já existe no projeto mas não está sendo chamado para nenhum evento da web UI. Ligar os eventos de "CI com erro", "novo comentário" e "PR mergeado" ao toast nativo do Windows.

**Por que:** Hoje as notificações só aparecem dentro do browser. Se o dashboard estiver minimizado ou em outra aba, o usuário não vê nada. Toast nativo aparece mesmo com o app em background.

---

### ~~6. Notificar quando alguém aprova seu PR~~ ✅ feito

**Status:** implementado — notificação visual + áudio configurável (`notifApprovedVisual` / `notifApprovedAudio`).

---

### ~~7. Notificar quando o PR sai do estado Draft~~ ✅ feito

**Status:** implementado — `notifReadyVisual` / `notifReadyAudio`.

---

### ~~8. Histórico de notificações na UI e em logs dedicados~~ ✅ feito

**Status:** implementado — aba "Notificações" no dashboard + `NotificationLogger` gravando em `out/<semana>/notifications/notifications-YYYYMMDD.log` (JSON por linha, separado dos logs de app).

---

## Dados e Integração com GitHub

### ~~9. Mostrar checks de CI com nome detalhado~~ ✅ feito

**Status:** implementado — tooltip no badge de CI lista todos os checks com falha.

---

### ~~10. Mostrar quem foi assignado ao PR~~ ✅ feito

**Status:** implementado — assignees aparecem como badges no card do PR.

---

### 11. Histórico de tempo médio em review por repo

**O que:** Usar os dados de `mergedPrs` para calcular e exibir na aba "Mergeados" o tempo médio que um PR ficou aberto por repositório (opened → merged).

**Por que:** Métrica de lead time por repo. Mostra onde o processo está mais lento sem precisar de ferramentas externas. Os dados já chegam via API; é só agregação.

---

## Confiabilidade e Infraestrutura

### ~~12. Limpeza de código morto~~ ✅ feito

**Status:** `LiveDashboard.cs`, `PrRenderer.cs` e `TerminalDetector.cs` removidos.

---

### 13. Separar WebAssets.cs em arquivo HTML/CSS/JS real

**O que:** Todo o frontend (1000+ linhas de HTML/CSS/JS) está num único string literal em C#. A melhoria é extrair para arquivos `.html`, `.css` e `.js` reais e embuti-los no build como recursos.

**Por que:** Hoje editar CSS ou JS exige navegar por um arquivo C# com escape de aspas e sem syntax highlighting. Separar os arquivos restaura o tooling de frontend (formatadores, linters, autocomplete de IDE) sem mudar o comportamento do publish single-file.

---

### 14. Persistência de settings em config.json em vez de localStorage

**O que:** Preferências como tema, idioma, sidebar aberta e intervalo de refresh são salvas no `localStorage` do browser. Se o usuário limpar os dados do browser ou acessar de outro perfil de browser, tudo volta ao padrão.

**Por que:** O app já tem um `config.json` ao lado do executável e um endpoint `/api/settings` que parcialmente persiste no servidor. Centralizar tudo no `config.json` garante que as preferências sobrevivam a qualquer troca de browser.

---

### ~~15. Tela de erro mais informativa no SplashWindow~~ ✅ feito

**Status:** SplashWindow exibe o `ex.Message` mais o comando de correção (`ex.FixCommand`) quando disponível.
