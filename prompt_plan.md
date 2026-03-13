# Implementation Plan: RnEventCalendar Redesign

## 상태: 진행 중

## 요구사항
- RnEventCalendar를 네이버 캘린더 스타일 UI (껍데기만)로 리팩토링
- RnUI 컴포넌트 철학에 맞게 `.cn-event-calendar-*` CSS 클래스 체계 구축
- 모바일 반응형: 이벤트가 점(dot)으로 표시
- 여러 날 걸치는 이벤트 바 지원
- 접근성 (role, aria-*) 추가

## Phases

### Phase 1: Data Model
- `EventCalendarEnums.cs` — EventVariant enum (BarVariant와 동일 색상 체계)
- `RnCalendarEvent.cs` — Variant 속성 추가

### Phase 2: CSS (~120줄)
- `shadcn.src.css`에 `.cn-event-calendar-*` 클래스 전체 추가
- 모든 테두리 `var(--color-border-subtle)` 사용
- 반응형 show/hide (desktop events / mobile dots)

### Phase 3: Multi-day Layout
- `EventCalendarLayout.cs` — 주 단위 이벤트 배치 계산

### Phase 4: Component Refactor
- RnEventCalendar.razor 전면 리팩토링
- 인라인 Tailwind → CSS 클래스 switch
- `<style>` 블록 제거
- 접근성 속성 추가

### Phase 5: Tests (14개)
### Phase 6: CSS build + dotnet build 검증

## 구현하지 않는 것
- 드래그앤드롭, 리사이즈, 인라인 생성
- 주간/일간/어젠다 뷰
- 반복 이벤트, 타임존
