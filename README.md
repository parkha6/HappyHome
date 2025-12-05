

---

# 🎮 핵 앤 슬래시 로그라이트

2D 횡스크롤 핵 앤 슬래시 로그라이트 프로젝트

---

## 🎨 그래픽 & 배경 정보

* 배경: AI & 에셋 기반
* 필드 오브젝트: 바닥 타입 구성
* 그래픽 스타일: 카툰 렌더링

---

## 📝 에셋 저작권

* **폰트**: [여기어때 잘난체](https://gccompany.co.kr/font)

---

# 🛠️ Unity ProjectH — README

이 문서는 Unity 기반 2D 프로젝트의 핵심 시스템 및 스크립트 구조를 정리한 설명서입니다.

---

# 📌 개요

`ProjectH` 는 다음 기능들을 포함하는 2D Unity 게임 시스템 패키지입니다:

* NPC 상호작용(F 키)
* 맵 구역 표시 및 일시정지 UI
* 스킬 히트박스(범위 검사) 시스템
* 플레이어/몬스터 충돌 처리
* 드랍 시스템(골드, 스킨, 윙 등)
* 인벤토리 및 UI 구조
* 강화 UI 및 스탯 처리 구조

---
# 🗂️ 팀원 소개
<img width="1137" height="518" alt="Image" src="https://github.com/user-attachments/assets/7b2fc846-132f-4701-9abc-6ef0aca981e3" />

---

# 🗂️ 와이어프레임

<img width="3411" height="1947" alt="Image" src="https://github.com/user-attachments/assets/41dedf0e-9602-477a-9031-bee88abc2322" />

---

# ⚙️ 코어 시스템 (Core System)

## **GameManager**

* 씬 전환, 일시정지, 재시작 등 게임 전체 흐름 제어
* 아이템 드랍 및 로딩 처리 등 전반적 라이프사이클 관리

## **ItemManager**

* 아이템 로딩·관리·통화 처리 등 아이템 관련 기능 총괄
* CurrencyWallet과 연동해 플레이어 자원 추적

## **MonoSingleton**

* 각종 매니저·플레이어 객체를 안정적으로 관리하는 싱글톤 베이스

## **PlayerManager**

* 플레이어가 가지고 있어야 할 정보들을 관리하는 스크립트.



---

# 🚶 플레이어 시스템 (Player System)

플레이어는 SoC(책임 분리) 원칙으로 구성됨:

## **Player**

* 이동, 공격, 상호작용 등 플레이어 전체 로직 제어

## **PlayerMovement**

* 입력 기반 이동, 중력, 방향 전환 처리

## **PlayerAttack**

* 근접 공격, 스킬 발동, 히트 판정

## **PlayerSkillController**

* 스킨 타입(바퀴벌레/메뚜기/사마귀)에 따른 스킬 데이터 관리 및 발동



---

# 🤖 적 시스템 (Enemy System)

## **Enemy**

* 피격 처리, 사망 처리 등 적의 핵심 동작 담당
* Die() → 드랍 생성 → 오브젝트 제거

## **EnemySaveData**

* 적 체력 및 난이도 조정용 데이터 저장 구조

---

# 💥 스킬 시스템 (Skill System)

## **BaseSkill**

* 모든 스킬의 기본 흐름(쿨타임, 발동, 종료)을 정의한 추상 클래스

## **스킨 전용 스킬**

* **CockroachSkill / GrasshopperSkill / MantisSkill**
* 각 곤충 스킨만의 고유 스킬 기능 구현

## **SkillHitBoxData**

* 히트박스 범위 계산 및 적 감지 로직
* OverlapBox/Raycast 기반 탐지
* Debug.DrawLine으로 시각화 가능

---

# 🧱 충돌 & 트리거 시스템

* 플레이어·몬스터 충돌 처리
* 넉백, 통과, 접촉 데미지 등 상황에 따른 트리거 핸들링
* 몬스터 사망 시 SpawnDrop → Destroy 로 드랍 관리

---

# 🤝 상호작용 시스템 (Interaction System)

## **IInteractable / IDamageable**

* 상호작용 및 데미지 처리 인터페이스

## **PlayerInteract**

* 플레이어 앞 대상 인식 → 인터페이스 구현 여부에 따라 행동 실행

## **NPCInteractOpener**

* 강화/진화 NPC 접근 시 UI 자동 오픈

---

# 🛠️ 아이템 & 드랍 시스템 (Item / Drop System)

## **CurrencyDrop / SkinDropPickup**

* 드랍 아이템 획득 처리(자동 습득 가능)

## **CurrencyWallet**

* 플레이어 골드/스킨/윙 자원 관리

## **ItemLoader**

* ScriptableObject 기반 아이템 데이터 로딩 관리

---

# 📜 NPC & 강화·진화 시스템

## **EnhanceNPC**

* 스킨 강화 기능
* 재화 계산 및 PlayerStatus 적용

## **EvolveNPC**

* 진화 조건 충족 시 새로운 스킨/능력 해금
* EvolutionUpgradeData 기반 처리

---

# 🗺️ 맵 & UI 시스템

## **Door**

* 맵 전환 및 스테이지 이동 관리

## **InterectingUi**

* 상호작용 가능 시 `F` 키 UI 출력

## **EasyRandom**

* 필드 오브젝트 랜덤 배치 유틸리티

## **MainSceneUI / PlayerUI**

* HP, 스태미나, 경험치, 스킨 등 HUD 표시

---

# 📁 스크립트 구조

```
Scripts/
├─ Character/
│    ├─ CharacterData/
│    ├─ PermanentStats/
│    ├─ SkinUpdateState/
├─ Enemy/
│    ├─ Enemy/
│    ├─ EnemySaveData/
├─ Enum/
│    ├─ Consts/
│    ├─ Enum/
├─ Interface/
│    ├─ IDamageable/
│    ├─ IInteractable/
├─ Item/
│    ├─ CurrencyDrop/
│    ├─ CurrencyWallet/
│    ├─ ItemLoader/
│    ├─ SkinDropPickup/
├─ Manager/
│    ├─ GameManager/
│    ├─ ItemManager/
│    ├─ MonoSingleton/
├─ map/
│    ├─ Door/
│    ├─ EasyRandom/
│    ├─ InterectingUi/
├─ Npc/
│    ├─ EnhanceNPC/
│    ├─ EvolveNPC/
│    ├─ NPCInteractOpener/
├─ Player/
│    ├─ Player/
│    ├─ PlayerAttack/
│    ├─ PlayerInteract/
│    ├─ PlayerItemData/
│    ├─ PlayerManager/
│    ├─ PlayerMovement/
│    ├─ PlayerSaveData/
│    ├─ PlayerSkillController/
├─ ScriptableObject/
│    ├─ EquipmentItemData/
│    ├─ EvolutionUpgradeData/
│    ├─ itemData/
│    ├─ SkinEnhanceData/
├─ Skill/
│    ├─ BaseSkill/
│    ├─ CockroachSkill/
│    ├─ GrasshopperSkill/
│    ├─ MantisSkill/
│    ├─ SkillHitBoxData/
├─ UI/
│    ├─ MainSceneUI/
└─   ├─ PlayerUi/
```

---

# 📝 주요 시스템 요약

### ✔ 상호작용 & 맵 UI

* F 키 표시
* 현재 구역 동그라미 UI

### ✔ 스킬 시스템

* 범위 검사
* 히트박스/적 감지 로직
* 시각화 디버깅 가능

### ✔ 충돌 시스템

* 넉백/접촉 데미지/통과 처리
* 드랍 생성 후 몬스터 제거

### ✔ 드랍/인벤토리

* 골드/조각 드랍
* 슬롯 UI · 강화 UI 갱신

---

# 🎮 조작법

| 키       | 기능   |
| ------- | ---- |
| **← →** | 이동   |
| **X**   | 공격   |
| **A**   | 스킬 1 |
| **S**   | 스킬 2 |
| **C**   | 점프   |
| **Z**   | 대쉬   |
| **F**   | 상호작용 |
| **ESC** | 일시정지 |

---

