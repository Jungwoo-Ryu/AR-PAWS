# AR-paws (DS 579)

Unity **2022.3 LTS** + **AR Foundation**을 권장합니다. 이 폴더는 **재사용 가능한 스크립트**를 담고 있으며, Unity 프로젝트 전체가 아닙니다.

## 프로젝트에 넣는 방법

1. Unity Hub에서 **3D (Built-in)** 또는 **3D (URP)** 프로젝트를 새로 만듭니다.
2. **Window → Package Manager**에서 다음 패키지를 추가합니다 (버전은 에디터가 제안하는 **5.0.x** 계열을 사용해도 됩니다).
   - `AR Foundation`
   - `Google ARCore XR Plugin` (Android)
   - `Apple ARKit XR Plugin` (iOS)
3. 공식 **AR Foundation 샘플** 또는 문서의 “Session + Plane” 구성을 참고해, 씬에 **XR Origin**(또는 환경에 맞는 세션 루트), **AR Session**, **AR Plane Manager**, **AR Raycast Manager**를 둡니다.
4. 이 저장소의 `Assets/ARPaws` 폴더를 통째로 프로젝트 `Assets` 아래로 복사합니다.
5. 빈 GameObject에 `PetNeedsController`를 추가합니다. Canvas 아래 슬라이더가 있다면 `PetNeedsUIBindings`로 연결합니다.
6. **XR Origin**(또는 Raycast Manager가 붙은 오브젝트)에 `ARPetsSurfacePlacer`를 추가하고, **Pet 프리팹** 필드에 임시 3D 모델을 넣습니다.

## 스크립트 요약

| 스크립트 | 역할 |
|----------|------|
| `PetNeedsController` | 배고픔·청결·기분·놀이 감쇠 및 먹이/쓰다듬기/미니게임 보정 |
| `PetNeedsPersistence` | `Application.persistentDataPath`에 JSON 저장 |
| `PetNeedsUIBindings` | UI 슬라이더·기분 텍스트 갱신 |
| `PetInteractionPanel` | 버튼으로 상호작용 호출 |
| `ARPetsSurfacePlacer` | 평면 첫 탭에 펫 스폰(에디터에서는 마우스 클릭) |

## 라이선스

과제용 프로토타입으로 제출 전, 서드파티 에셋(Sketchfab 등)의 **재배포 가능 여부**를 팀에서 확인하세요.
