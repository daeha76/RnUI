# RnUI Roadmap

> **Principle**: shadcn/ui won the React ecosystem not because of design, but by following the sequence: **Developer Trust → DX → Ecosystem**. RnUI follows the same path.

## Phase 0 — Immediate Actions (This Week)

Low-cost, high-impact tasks that define the first impression.

- [x] **CHANGELOG.md** — [Keep a Changelog](https://keepachangelog.com/) format, retroactively covering all releases
- [x] **GitHub Issue Templates** — Bug report, feature request, component request templates
- [x] **Pull Request Template** — Standardized PR checklist
- [x] **CONTRIBUTING.md** — Contributor guide with component porting workflow
- [ ] **GitHub Discussions** — Enable Q&A, Ideas, and Showcase categories
- [ ] **awesome-shadcn-ui Registration** — Submit PR to [birobirobiro/awesome-shadcn-ui](https://github.com/birobirobiro/awesome-shadcn-ui) and [shadcn.io/awesome/ports](https://ui.shadcn.com/docs/community/ports)

## Phase 1 — Stability & Trust (1–3 Months)

**Goal**: Remove the "beta" label and confidently release v1.0.0.

### 1-1. bUnit Test Framework

- [ ] Priority test targets: `DataTableState`, `RnForm`, `RnCombobox`, `CssUtils.Cn()`
- [ ] Enforce "PRs must include tests" policy
- [ ] Target: 50% coverage at 3 months, 80% at 12 months

### 1-2. CI Pipeline Enhancement

- [x] Basic CI workflow (build + test on PR)
- [ ] Multi-target build validation (`net8.0`, `net9.0`, `net10.0`)
- [ ] NuGet dry-run packaging on PR
- [ ] CI badge in README

### 1-3. Semantic Versioning + Release Automation

- [ ] Strict SemVer: `v0.x.x` (beta) → `v1.0.0` (at 80% test coverage)
- [ ] Automated NuGet publish on tag push (`v*`)
- [ ] GitHub Releases with auto-generated notes

### 1-4. CSS Source Transparency

- [ ] Document all CSS custom properties (design tokens)
- [ ] Add "Style Override Guide" to documentation
- [ ] Per-component CSS source documentation

## Phase 2 — Developer Experience (3–6 Months)

**Goal**: "Install and run in 5 minutes" — this is the word-of-mouth catalyst.

### 2-1. `dotnet new` Templates (Highest Single Impact)

- [ ] `Daeha.RnUI.Templates` NuGet package
- [ ] `dotnet new rnui-blazor-wasm` — WASM template with Tailwind v4 pre-configured
- [ ] `dotnet new rnui-blazor-server` — Server template
- [ ] Zero Tailwind setup friction

### 2-2. IDE Snippets

- [ ] Visual Studio XML snippets
- [ ] JetBrains Rider Live Templates
- [ ] VS Code `.code-snippets` file

### 2-3. Theme System + Gallery

- [ ] Official theme CSS files (Ocean, Rose, Violet, Amber)
- [ ] Live theme preview on demo site
- [ ] CSS copy button for social sharing

### 2-4. Documentation Site Enhancement

- [ ] Installation video guide
- [ ] Migration guide from MudBlazor
- [ ] Cookbook (real-world patterns)
- [ ] API Reference auto-generation (XML comments → DocFX)

## Phase 3 — Community Ecosystem (6–12 Months)

**Goal**: Community-driven growth, not solo maintenance.

### 3-1. Discord Server

- [ ] Channels: announcements, general, bug-reports, feature-requests, showcase, help, contributing, i18n
- [ ] Bidirectional link with GitHub Discussions

### 3-2. Contributor System

- [ ] `good-first-issue`, `help-wanted`, `i18n` issue labels
- [ ] Component sponsor system (shadcn/ui Registry model)

### 3-3. Content Marketing

- [ ] Blog/Dev.to series: "Why I built shadcn/ui for Blazor"
- [ ] YouTube tutorials: Installation → DataTable → Dark Mode → Admin Dashboard
- [ ] Reddit posts: r/Blazor, r/dotnet, r/csharp, r/webdev

### 3-4. Block Library

- [ ] Dashboard Layouts (3 variants)
- [ ] Authentication pages (Login, Register, Forgot Password, 2FA)
- [ ] Data Pages (User management, Order list, Analytics)
- [ ] Settings Pages (Profile, Billing)
- [ ] Each block as a single copy-paste `.razor` file

## Phase 4 — Enterprise & Long-term Growth (12+ Months)

**Goal**: Expand trust from individuals → startups → enterprises.

- [ ] **WCAG 2.1 AA** — Full accessibility audit, axe-core CI integration
- [ ] **Blazor MAUI Hybrid** — Official support and guide
- [ ] **AI-Friendly Catalog** — Copilot-optimized XML comments, LLM-readable API docs
- [ ] **Showcase Gallery** — "Made with RnUI" project gallery

## Success Metrics (KPIs)

| Metric | Current | 3 Months | 12 Months |
|--------|---------|----------|-----------|
| GitHub Stars | Early | 200+ | 1,000+ |
| NuGet Monthly Downloads | Low | 500+ | 5,000+ |
| Contributors | 1 | 5 | 20+ |
| Issue Response Time | N/A | <48h | <24h |
| Test Coverage | ~0% | 50%+ | 80%+ |
| Documentation | 54 basic | 54 detailed | + 20 blocks |

## Execution Priority (Pick 3)

For a solo-maintained early-stage OSS project, focus on these three first:

1. **`dotnet new` Templates (2-1)** — Remove installation friction = adoption rate
2. **bUnit Tests + CI (1-1, 1-2)** — Remove beta label = trust
3. **awesome-shadcn-ui + Dev.to article (0, 3-3)** — Zero cost, first 100 users

These three alone can reach the tipping point where community forms naturally. The rest can be built together with the community.
