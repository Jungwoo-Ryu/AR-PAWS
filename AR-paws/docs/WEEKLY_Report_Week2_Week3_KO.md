# AR-paws: Companion Reality — 주간 보고 (Week 2–3 초안)

> 제출 시 LMS/노션 형식에 맞게 복사해 쓰면 됩니다. **미디어(스크린샷·영상)**는 팀이 Unity 화면 또는 와이어프레임을 캡처해 각 주차에 **서로 다른 이미지**를 첨부하세요.

---

## Weekly Report — Week 2

### State of the Project

- **첨부 미디어(권장)**: 기획서의 **UI 와이어프레임** 스케치, 또는 Unity Hub에서 **새 AR 프로젝트 생성 직후**의 빈 씬/프로젝트 창 스크린샷.
- **한 줄 설명**: “AR Foundation 기반 Unity 프로젝트 생성 및 역할 분담·에셋 출처 정리 단계. 플레이 가능한 빌드는 아직 없음.”

### Responses and Reflection

지난주 동료 피드백이 있다면, **공통 경향**(예: “메뉴 vs 직접 조작의 균형”, “방 경계 설정 UX”)을 한 문단으로 요약하고, 그 피드백이 **GDD(기획)**의 어떤 항목(상호작용 우선순위, 주간 마일스톤)을 수정했는지 짧게 씁니다. 피드백이 아직 없다면: “Week 2에는 피드백 수집 전이므로, Week 3 제출 전에 discussion 게시판에 **방 경계 설정 방식(폴리곤 vs 박스)** 질문을 올릴 예정”처럼 **후속 질문**을 명시합니다.

초기 피치 대비 접근 변화가 있으면(예: “iOS만 먼저” → “Android 테스트폰 확보로 Android 우선”), 없다면 **Tamagotchi 루프 + AR 평면 배치** 방향에 대한 확신을 한두 문장으로 정리합니다.

### Accomplishments (팀원별)

| 팀원 | 이번 주 성과 |
|------|----------------|
| **Derek Chu** | Unity **2022.3 LTS** 기준으로 팀 에디터 버전 통일; AR Foundation 학습 자료·공식 샘플 링크 정리. |
| **Ken Ryu** | 에셋 리스트 주간 업데이트(아래 표 참고); Sketchfab/Unity Asset Store **CC 라이선스** 확인. |
| **(Student 3)** | 핵심 루프(배고픔·청결·기분·놀이) **상태 전이 표** 초안; UI에 표시할 지표 4종 확정. |
| **(Student 4)** | GitHub(또는 과제 지정 저장소) **폴더 구조** 초안, `.gitignore`에 Unity `Library/` 등 제외 규칙 반영. |

### Challenges

- AR **실기 테스트**용 기기(Android ARCore / iOS ARKit) 페어링 및 빌드 설정이 처음이라 시간이 걸릴 수 있음.
- 팀원 에디터/OS가 달라 **동일 Unity 버전** 맞추기.
- 스케치fab 모델의 **스케일·리깅**이 제각각이라, Week 3에 **임시 프리팹**으로 통일 필요.

### Plans for next week

- AR Session + Plane Detection이 되는 **최소 씬** 1개 완성.
- `ARPaws` 스크립트(욕구·저장)를 프로젝트에 임포트하고, **Canvas 슬라이더**와 연결.
- 바나나/공 프리팹을 씬에 올려 **배치만** 확인(애니메이션은 Week 4 이후 가능).

---

## Weekly Report — Week 3

### State of the Project

- **첨부 미디어(권장)**: Unity **Hierarchy**에 `XR Origin`, `AR Session`, `PetNeedsController`가 올라간 씬 스크린샷; 또는 **Game 뷰**에서 UI 슬라이더가 보이는 화면. (Week 2와 **다른** 이미지/클립.)
- **한 줄 설명**: “욕구 시스템·JSON 저장·평면 탭 배치용 스크립트를 프로젝트에 추가했고, AR 씬 연결 및 실제 단말 빌드는 진행 중.”

### Responses and Reflection

Week 2에 받은 질문/피드백이 있으면 **구체적으로 답한 내용**(예: “방 경계는 MVP에서 AR Plane 위 이동만 하고, 폴리곤 경계는 스코프 아웃”)을 적습니다. 새로 드는 질문이 있으면 **교수/조교에게 묻고 싶은 한 가지**를 명시합니다.

### Accomplishments (팀원별)

| 팀원 | 이번 주 성과 |
|------|----------------|
| **Derek Chu** | XR Origin 기준 **AR Plane** 인식 테스트; 디바이스 빌드 시 발생한 **권한/플레이어 설정** 이슈 목록화. |
| **Ken Ryu** | `PetNeedsController`·저장 로직과 UI 바인딩 스크립트를 씬에 붙이는 **작업 지시서**(체크리스트) 작성. |
| **(Student 3)** | 기분(Mood)과 수치 연동 규칙을 기획서에 반영; 이후 애니메이션 블렌드 트리에 쓸 **상태 머신** 초안. |
| **(Student 4)** | 저장소에 `AR-paws/Assets/ARPaws/Scripts` 반영, 팀원 로컬에서 **동일 커밋**으로 열리도록 정리. |

### Challenges

- 에디터만으로는 AR 입력이 제한되어 **실기**에서 탭 배치·UI 동시 검증 필요.
- JSON 저장 경로(`persistentDataPath`)가 기기마다 달라, **저장/로드 QA**를 짧게라도 할 계획.
- 3D 고양이 모델 리깅이 아직이면 **캡슐/큐브 프리팹**으로 자리만 잡아야 함.

### Plans for next week

- 고양이 프리팹 + 기본 **Idle/Eat** 애니메이션 연결(또는 Asset Store 애니메이션 임시 적용).
- 먹이/장난감과의 **충돌 또는 근접 상호작용** 프로토타입.
- (시간 허락 시) **미니게임 1종** 플레이스홀더.

---

## 에셋 리스트 업데이트 (Week 2–3 추가)

기존 표에 아래 행을 **추가**해 주간 업데이트로 제출하면 됩니다.

| Asset | Type | Self Created / Link |
|-------|------|---------------------|
| Pet Needs Core (`PetNeedsController` 등) | Code (C#) | Self — repo `Assets/ARPaws/Scripts` |
| Needs Save/Load (JSON) | Code (C#) | Self |
| AR Plane Tap Placer (`ARPetsSurfacePlacer`) | Code (C#) | Self |
| UI Stats 바인딩 | Code (C#) | Self |
| XR Origin + AR Session (베이스 씬) | Scene / Unity 설정 | Self (AR Foundation 템플릿 기반) |
| Placeholder Pet Mesh | 3D (임시) | Self — Primitive Capsule 또는 팀용 임시 모델 |

---

## Week 1 보충 (아직 제출 전이라면)

Week 1은 **기획/컨셉** 중심으로, “콘셉트 아트 1장 또는 프로젝트 개요 슬라이드 캡처”를 올리고, Reflection에서는 **Peridot / Nintendogs 비교**와 팀이 정한 **MVP 범위**를 1–3문단으로 정리하면 됩니다.
