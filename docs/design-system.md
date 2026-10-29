# PrDashboard — Design System

> Catppuccin Mocha palette throughout. All color tokens defined here are the single source of truth for both the web UI (`WebAssets.cs`) and the desktop splash window (`SplashHtml.cs`).

---

## Color Palette — Catppuccin Mocha

### Background Layers

| Token | Hex | Usage |
|---|---|---|
| **Crust** | `#11111b` | Deepest background (rarely used directly) |
| **Mantle** | `#181825` | Titlebar, deeply nested cards, status card |
| **Base** | `#1e1e2e` | Page / window background |
| **Surface** | `#252537` | Card and panel background |

### Borders & Overlays

| Token | Hex | Usage |
|---|---|---|
| **Surface 0** | `#313244` | Card borders, dividers, input borders |
| **Surface 1** | `#45475a` | Hover borders, input focus ring base |
| **Surface 2** | `#585b70` | Very subtle dividers |
| **Hover** | `#2a2a3d` | Row/item hover background |

### Text

| Token | Hex | Catppuccin name | Usage |
|---|---|---|---|
| **Text** | `#cdd6f4` | Text | Primary body text |
| **Subtext 1** | `#bac2de` | Subtext 1 | Secondary text |
| **Subtext 0** | `#a6adc8` | Subtext 0 | Placeholder, disabled |
| **Overlay 0** | `#6c7086` | Overlay 0 | Muted labels, timestamps |
| **Overlay 1** | `#7f849c` | Overlay 1 | Icons at rest |

### Accent Colors

| Token | Hex | Catppuccin name | Usage |
|---|---|---|---|
| **Mauve** | `#cba6f7` | Mauve | **Primary accent** — buttons, focus rings, active states, progress bars |
| **Blue** | `#89b4fa` | Blue | Links, secondary accent |
| **Lavender** | `#b4befe` | Lavender | Violet badges, tertiary accent |
| **Sky** | `#89dceb` | Sky | Info states |
| **Teal** | `#94e2d5` | Teal | Alternate success |

### Semantic Colors

| Token | Hex | Catppuccin name | Usage |
|---|---|---|---|
| **Green** | `#a6e3a1` | Green | Success, service running, CI passing |
| **Yellow** | `#f9e2af` | Yellow | Warning, draft |
| **Peach** | `#fab387` | Peach | Caution, blocked states |
| **Red** | `#f38ba8` | Red | Error, CI failing, service stopped |
| **Maroon** | `#eba0ac` | Maroon | Soft error |

### Dark backgrounds for semantic badges

These are used as `background` on status badges alongside the semantic text color above.

| Semantic | Badge bg | Badge border |
|---|---|---|
| Green | `rgba(166,227,161,.15)` | `rgba(166,227,161,.35)` |
| Yellow | `rgba(249,226,175,.15)` | `rgba(249,226,175,.35)` |
| Red | `rgba(243,139,168,.15)` | `rgba(243,139,168,.35)` |
| Mauve | `rgba(203,166,247,.15)` | `rgba(203,166,247,.35)` |
| Blue | `rgba(137,180,250,.15)` | `rgba(137,180,250,.35)` |

---

## CSS Custom Properties (Web UI — dark mode)

```css
.dark {
  --bg:       #1e1e2e;   /* Base   */
  --surf:     #252537;   /* Surface */
  --subtle:   #181825;   /* Mantle — nested, inset */
  --border:   #313244;   /* Surface 0 */
  --text:     #cdd6f4;   /* Text */
  --muted:    #6c7086;   /* Overlay 0 */
  --hover:    #2a2a3d;
  --indigo:   #cba6f7;   /* Mauve — primary accent */
  --header-bg: rgba(30,30,46,.92);
}
```

---

## Typography

| Role | Family | Size | Weight |
|---|---|---|---|
| Body | Inter | 13–14px | 400 |
| Label / Badge | Inter | 10–11px | 500–700 |
| Headline | Inter | 15–24px | 600 |
| Monospace | JetBrains Mono | 11–13px | 400–500 |

---

## Spacing Scale

| Name | Value | Usage |
|---|---|---|
| `xs` | 4px | Icon gap, tight inline spacing |
| `sm` | 8px | Inner padding for compact items |
| `md` | 12px | Default card padding |
| `lg` | 16px | Section padding |
| `xl` | 24px | Page padding |

---

## Border Radius

| Token | Value | Usage |
|---|---|---|
| Small | `6px` | Chips, badges, inputs |
| Medium | `10–12px` | Cards, toolbars |
| Large | `14px` | Repo groups, big cards |
| Full | `9999px` | Pills, avatar rings |

---

## Component Patterns

### Card / Surface
```css
background: var(--surf);
border: 1px solid var(--border);
border-radius: 12px;
box-shadow: 0 2px 12px rgba(0,0,0,.25);
```

### Status Badge
```css
display: inline-flex;
align-items: center;
padding: 2px 7px;
border-radius: 6px;
font-size: 11px;
font-weight: 500;
border: 1px solid <badge-border>;
background: <badge-bg>;
color: <semantic-color>;
```

### Progress Bar (active)
```css
background: linear-gradient(90deg, #9955c2, #cba6f7);
box-shadow: 0 0 8px rgba(203,166,247,.4);
```

### Active / Running Dot
Green ping animation using `#a6e3a1` — reserved for live service/process status.

### Primary Action Button (ready state)
```css
background: linear-gradient(135deg, #7c3aed, #cba6f7);
box-shadow: 0 4px 12px rgba(203,166,247,.3);
color: #1e1e2e;
```

---

## Dark vs Light

The web UI supports both light and dark themes. Light mode uses a neutral white/slate palette (Tailwind slate scale). The Catppuccin Mocha palette is applied **only in dark mode** via the `.dark` class on `<html>`.

The desktop splash window is always dark (no light mode toggle).

---

## Files

| File | Scope |
|---|---|
| `PrDashboard/Web/WebAssets.cs` | Web UI — full HTML/CSS/JS |
| `PrDashboard/UI/SplashHtml.cs` | Desktop splash — Tailwind + inline CSS |
| `PrDashboard/UI/SplashWindow.cs` | WinForms host — `BackColor` must match `--bg` |
