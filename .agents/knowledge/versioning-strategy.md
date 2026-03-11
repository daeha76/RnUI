# RnUI 버전 관리 전략 (Versioning Strategy)

## Semantic Versioning (SemVer)

RnUI는 [Semantic Versioning 2.0.0](https://semver.org/lang/ko/) 규칙을 따릅니다.

```
MAJOR.MINOR.PATCH
 │      │     └── 버그 수정, CSS 미세 조정 (하위 호환)
 │      └──────── 새 컴포넌트/기능 추가, 기존 기능 개선 (하위 호환)
 └─────────────── 호환되지 않는 API 변경 (Breaking Changes)
```

## 버전 범위별 기준

### PATCH (0.x.Y → 0.x.Y+1)

다음 경우에 PATCH 버전을 올립니다:

- 컴포넌트 버그 수정 (렌더링, 이벤트, 접근성 등)
- CSS 스타일 미세 조정 (시각적 버그 수정)
- 문서/README 수정만 포함된 릴리스
- 기존 컴포넌트의 내부 리팩토링 (외부 API 변경 없음)
- 테스트 추가/수정
- 성능 개선 (API 변경 없음)

### MINOR (0.X.0 → 0.X+1.0)

다음 경우에 MINOR 버전을 올립니다:

- 새 컴포넌트 추가 (예: `RnGantt`, `RnCarousel` 등)
- 기존 컴포넌트에 새 Parameter/Variant/Size 추가
- 새 서비스/유틸리티 추가
- CSS 디자인 토큰 추가
- 새 JS Interop 기능 추가
- .NET 타겟 프레임워크 추가

### MAJOR (X.0.0 → X+1.0.0)

다음 경우에 MAJOR 버전을 올립니다:

- 컴포넌트 Parameter 이름/타입 변경 (Breaking)
- 컴포넌트 삭제 또는 이름 변경
- CSS 클래스명 변경 (`.cn-*` 네이밍 변경)
- 디자인 토큰 변수명 변경
- 필수 의존성 변경 (예: Tailwind 메이저 버전 업)
- .NET 타겟 프레임워크 제거

## 1.0.0 이전 (현재 단계)

현재 RnUI는 `0.x.y` 단계로, 아직 안정 API가 확정되지 않았습니다:

- `0.x.y`에서는 MINOR 변경에도 Breaking Change가 포함될 수 있음
- 1.0.0 릴리스 기준:
  - 모든 핵심 컴포넌트 구현 완료
  - API 안정화 (Parameter 이름/타입 확정)
  - 충분한 테스트 커버리지 확보
  - 프로덕션 환경 사용 검증

## 버전 변경 위치

버전은 **단일 위치**에서만 관리합니다:

```
src/Daeha.RnUI/Daeha.RnUI.csproj → <Version>X.Y.Z</Version>
```

## 릴리스 프로세스

1. 변경 사항 개발 및 테스트 완료
2. `Daeha.RnUI.csproj`의 `<Version>` 업데이트
3. README / NUGET_README 업데이트 (필요 시)
4. 커밋 메시지에 버전 명시: `release: v0.8.1`
5. `dotnet pack src/Daeha.RnUI`로 NuGet 패키지 생성
6. NuGet 배포

## 커밋 메시지 규칙 (버전 관련)

```
release: v0.8.1          ← 릴리스 커밋
feat: Add RnGantt         ← MINOR 증가 대상
fix: Fix RnDialog focus   ← PATCH 증가 대상
breaking: Rename params   ← MAJOR 증가 대상
```

## 버전 이력

| 버전 | 날짜 | 주요 변경 |
|------|------|-----------|
| 0.8.1 | 2026-03-11 | README/NUGET_README 수정, 문서 정확도 개선 |
| 0.8.0 | - | 초기 NuGet 배포 |
