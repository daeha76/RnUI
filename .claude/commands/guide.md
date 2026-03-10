# RnUI Command Guide

사용 가능한 모든 커맨드를 보여줍니다.

## 실행

아래 목록을 사용자에게 보여주세요:

---

## Available Commands

### `/new-component {Name}`
shadcn/ui 컴포넌트를 RnUI Blazor 컴포넌트로 스캐폴딩합니다.
- 예: `/new-component Tooltip`
- 생성: `.razor`, `.razor.cs`, Enums, CSS (`.cn-*`), bUnit 테스트

### `/new-block {block-name}`
shadcn/ui 블록(페이지 레이아웃)을 RnUI 데모 앱에 스캐폴딩합니다.
- 예: `/new-block login-01`, `/new-block sidebar-05`
- 생성: `BlockDemos/{Category}/` 데모 + 조합 컴포넌트

### `/sync-shadcn {name}`
shadcn/ui 원본과 RnUI 컴포넌트 구현을 비교합니다.
- 예: `/sync-shadcn Button`
- 용도: 누락된 variant, prop, 스타일 확인

### `/sync-block {block-name}`
shadcn/ui 원본과 RnUI 블록 구현을 비교합니다.
- 예: `/sync-block login-01`
- 용도: 레이아웃, 컴포넌트 매핑, Form 변환, 반응형 확인

### `/build-css`
CSS 파이프라인을 빌드하고 변경 사항을 확인합니다.
- Library CSS: `shadcn.src.css` → `shadcn.css`
- Demo CSS: `input.css` → `tailwindcss.css`

---

## Quick Reference

| Command | 용도 | 예시 |
|---------|------|------|
| `/new-component` | 컴포넌트 생성 | `/new-component Dialog` |
| `/new-block` | 블록(페이지) 생성 | `/new-block login-02` |
| `/sync-shadcn` | 컴포넌트 원본 비교 | `/sync-shadcn Card` |
| `/sync-block` | 블록 원본 비교 | `/sync-block login-01` |
| `/build-css` | CSS 빌드 | `/build-css` |
| `/guide` | 이 도움말 | `/guide` |
