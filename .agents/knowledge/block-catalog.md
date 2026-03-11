# Block Catalog

## 카테고리별 구현 대상

### Login (8종)

| Block | 설명 | 레이아웃 | 필요 컴포넌트 | 구현 가능 |
|-------|------|---------|--------------|----------|
| **login-01** | 기본 카드 폼 | 중앙 카드 | Card, Button, Input, Label | ✅ |
| **login-02** | 이미지+폼 분할 | 2열 grid | Card, Button, Input, Label | ✅ |
| **login-03** | 중앙 정렬 (카드 없음) | 중앙 | Button, Input, Label | ✅ |
| **login-04** | 소셜 로그인 포함 | 중앙 카드 + 소셜 | Card, Button, Input, Label, Separator | ✅ |
| **login-05** | 회원가입 변형 | 중앙 카드 | Card, Button, Input, Label | ✅ |
| **login-06** | Magic link + SSO | 중앙 카드 | Card, Button, Input, Label, Separator | ✅ |
| **login-07** | 전체페이지 + 아이콘 | 전체 너비 | Button, Input, Label, Icons | ✅ |
| **login-08** | SSO 전용 | 중앙 카드 | Card, Button, Label | ✅ |

### Signup (login과 유사, 추가 필드)

| Block | 설명 | 추가 필드 | 구현 가능 |
|-------|------|----------|----------|
| **signup-01** | 기본 회원가입 | 이름, 이메일, 비밀번호 | ✅ |
| **signup-02** | 이미지+폼 분할 | 이름, 이메일, 비밀번호, 약관 | ✅ |

### Sidebar (7종)

| Block | 설명 | Sidebar 변형 | 필요 컴포넌트 | 구현 가능 |
|-------|------|-------------|--------------|----------|
| **sidebar-01** | Inset 대시보드 | Inset | Sidebar*, Breadcrumb, Separator | ✅ |
| **sidebar-02** | Floating | Floating | Sidebar*, Breadcrumb | ✅ |
| **sidebar-03** | 더블 사이드바 (메일) | Default × 2 | Sidebar* × 2 | ✅ |
| **sidebar-04** | 더블 사이드바 | Default + Rail | Sidebar*, SidebarRail | ⚠️ Rail 확인 필요 |
| **sidebar-05** | Collapsible 그룹 | Collapsible | Sidebar*, MenuSub | ✅ |
| **sidebar-06** | Tree view | Default | Sidebar*, Collapsible | ✅ |
| **sidebar-07** | With footer | Default | Sidebar*, DropdownMenu | ✅ |

### DataTable (2종)

| Block | 설명 | 필요 컴포넌트 | 구현 가능 |
|-------|------|--------------|----------|
| **data-table-01** | 결제 데이터 테이블 | DataTable, Badge, DropdownMenu, Dialog, Select, Input | ✅ |
| **data-table-02** | 분기별 매출 리포트 (2단 헤더) | DataTable, ColumnGroup, Badge, Avatar | ✅ |

### Dashboard (Featured)

| Block | 설명 | 필요 컴포넌트 | 구현 가능 |
|-------|------|--------------|----------|
| **dashboard-01** | 풀 대시보드 | Sidebar, Card, Table | ⚠️ Chart 미구현 |

---

## 선행 조건 (Prerequisite) 체크

### 구현 완료된 필수 컴포넌트

- [x] RnButton (6 variants, 8 sizes)
- [x] RnInput
- [x] RnLabel
- [x] RnCard + 서브컴포넌트 (7개)
- [x] RnSidebar + 서브컴포넌트 (13개)
- [x] RnBreadcrumb + 서브컴포넌트 (5개)
- [x] RnSeparator
- [x] RnBadge (6 variants)
- [x] RnAvatar + Fallback
- [x] RnDropdownMenu + 서브컴포넌트
- [x] RnTable + 서브컴포넌트 (8개)
- [x] RnTabs + 서브컴포넌트
- [x] RnCheckbox
- [x] RnSpinner
- [x] RnIcon

### 미구현 (Dashboard에 필요)

- [ ] Chart 컴포넌트 (영역 차트, 바 차트, 라인 차트)
- [x] DataTable (정렬, 필터, 페이지네이션 통합)

---

## 구현 우선순위 제안

1. **login-01** → 가장 간단, 기본 패턴 확립
2. **login-04** → 소셜 로그인 추가 (Separator 활용)
3. **signup-01** → login 패턴 재사용
4. **login-02** → 이미지 분할 레이아웃 도전
5. **sidebar-01** → Inset 사이드바 (기본)
6. **sidebar-05** → Collapsible 사이드바
7. **sidebar-07** → Footer + DropdownMenu
8. **dashboard-01** → Chart 컴포넌트 구현 후

---

## 참조 URL

각 블록의 shadcn/ui 원본:

```
https://ui.shadcn.com/blocks/login
https://ui.shadcn.com/blocks/sidebar
https://github.com/shadcn-ui/ui/tree/main/apps/v4/registry/new-york-v4/blocks
```

개별 블록 소스 (예: login-01):
```
https://github.com/shadcn-ui/ui/tree/main/apps/v4/registry/new-york-v4/blocks/login-01
```
