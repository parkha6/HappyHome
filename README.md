# 핵 앤 슬래시 로그라이트
2D 횡스크롤 핵 앤 슬래시 로그라이트  


---  


배경- AI, 에셋  
필드 오브젝트 바닥  
카툰 그래픽  


## 에셋 저작권
* 폰트  - [여기어때 잘난체](https://gccompany.co.kr/font)
# Unity ProjectH  


README: 이 저장소는 Unity 프로젝트 설명서입니다.  


---  


## 개요

`ProjectH` 는 2D Unity 게임 개발용 핵심 시스템들을 모아둔 샘플/작업용 패키지입니다. 이 패키지는 다음과 같은 기능들을 포함합니다:

* NPC 상호작용 시스템 (F 키로 상호작용)
* 지도 구간 표시 및 일시정지 화면에서의 현재 구역 표시 UI
* 스킬 범위(히트박스) 검사 및 스킬 시스템 예시
* 플레이어 및 몬스터 충돌/트리거 처리 개선 예시
* 드랍 생성(골드/스킨/윙 등) 시스템
* 인벤토리 UI 구조 및 슬롯 예시
* 강화(UI)와 관련된 데미지/스탯/업데이트 구조  



---  



## 와이어프레임
<img width="3411" height="1947" alt="Image" src="https://github.com/user-attachments/assets/41dedf0e-9602-477a-9031-bee88abc2322" />



---  


## ⚙️ 코어 시스템 (Core System)
# GameManager

* 게임의 전체적인 흐름을 제어하는 핵심 시스템입니다.
* 씬 전환, 일시정지(Pause), 재시작(Resume), 게임 종료 같은 공통 기능뿐만 아니라
* 아이템 드랍, 로딩 처리 등 다양한 매니저들과 연동되어 게임 전체의 라이프사이클을 총괄합니다.

# ItemManager

* 아이템 정보 관리, 드랍 데이터 로딩, 통화(골드·조각) 취급 등
* 게임 전반의 아이템 처리 흐름을 제어하는 시스템입니다.
* CurrencyWallet과 연동되어 플레이어가 획득한 자원을 추적합니다.

# MonoSingleton

* 플레이어, 플레이어 매니저, 각종 매니저 클래스 등을
* 싱글톤 패턴으로 안정적으로 생성·관리하도록 도와주는 베이스 클래스입니다.

  
---  


## 🚶 플레이어 시스템 (Player System)

* 플레이어는 책임 분리(SoC)를 기준으로 여러 기능 단위로 나누어 설계되어 있습니다.

# Player

* 플레이어의 핵심 컨트롤러로, 이동, 공격, 상호작용 등
* 다른 Player 계열 클래스들을 종합적으로 제어하는 메인 클래스입니다.

# PlayerMovement

* 키 입력·속도·중력·방향 전환 등
* 플레이어의 실제 이동 로직을 담당하며,
* Player나 PlayerInputHandler가 전달하는 입력 값을 기반으로 작동합니다.

# PlayerAttack

* 근접 공격, 스킬 발동, 히트 판정 등
* 전투와 관련된 모든 로직을 담당합니다.
* SkillHitBoxData를 활용하여 적을 감지하고 데미지를 적용합니다.

# PlayerSkillController

* 장착된 스킨(skin type)에 따라 서로 다른 스킬 데이터를 로딩하며, 다양한 곤충 스킬(바퀴벌레, 메뚜기, 사마귀)의 발동을 담당합니다.

# PlayerItemData

* 플레이어가 현재 보유한 장비, 포션, 스킨, 조각 등의 데이터를 통합 관리하는 클래스입니다.

  
---  


## 🤖 적 시스템 (Enemy System)
# Enemy

* 피격 처리,사망 등 적의 전체 동작을 제어하는 중심 클래스입니다.
* 적이 사망하면 Die()를 통해 드랍 생성 후 오브젝트를 제거합니다.

# EnemySaveData

* 체력, 강화 수치 등 일부 영구 데이터나 난이도 조정용 추가 값을 저장해두기 위한 데이터 구조입니다.

  
---  


## 💥 스킬 시스템 (Skill System)

* 스킬은 공통 구조 + 각 스킨의 고유 스킬로 구성됩니다.

# BaseSkill

* 모든 스킬의 공통된 흐름(쿨타임, 발동, 종료)을 정의한 기본 클래스입니다.

* 스킨 전용 스킬 (CockroachSkill / GrasshopperSkill / MantisSkill)

*각 곤충 스킨 전용 스킬로, BaseSkill을 상속하여 개별 히트박스, 이펙트, 이동 보조 기능 등이 구현됩니다.

# 스킬 히트박스

* 스킬 히트박스 계산, 범위 내 적 감지 기능을 담당하는 핵심 스크립트입니다.
* 스킬 발동 시 OverlapBox·Raycast 등을 통해 적을 탐지하고
* IDamageable 인터페이스로 데미지를 전달합니다.

* 또한 개발 편의성을 위해 Debug.DrawLine 등을 사용해
* 히트박스가 실제 게임 화면에서도 시각적으로 표시되도록 구성할 수 있습니다.


---  


## 🧱 충돌 & 트리거 처리 (Collision & Trigger System)

* 플레이어와 몬스터가 서로 충돌할지, 통과할지, 넉백이 발생할지 등은 Layer, Collision Matrix, OnTrigger… 함수 등을 조합해 처리합니다.

* 몬스터 Die() 시점에 드랍 생성 후 Destroy(gameObject)로 제거하며, SpawnDrop() 메서드를 통해 골드·스킨 조각·윙 조각 등을 상황에 따라 드랍합니다.

* 상황에 따라 플레이어가 몬스터와 충돌했을 때 “트리거를 무시하고 지나가도록” 처리하는 예시도 포함됩니다.

  
---  


## 🤝 상호작용 시스템 (Interaction System)
# IInteractable / IDamageable

* 상호작용 가능한 모든 오브젝트는 특정 인터페이스를 구현합니다.

# PlayerInteract

* 플레이어 앞의 NPC, 문, 아이템 등을 감지하고 인터페이스 구현 여부에 따라 적절한 상호작용을 실행합니다.

# NPCInteractOpener

* NPC에 접근하면 상호작용 UI를 열어주는 기능을 담당하며, 강화 NPC·진화 NPC와 직접 연결됩니다.

  
---  


## 🛠️ 아이템 & 드랍/제작 시스템 (Item & Drop System)
# CurrencyDrop / SkinDropPickup

* 적이 드랍한 아이템을 획득하는 역할을 담당합니다. 근접 시 자동으로 먹게 합니다.

# CurrencyWallet

* 플레이어가 보유한 통화를 집계·관리합니다. 골드, 스킨 조각, 윙 조각 등이 저장됩니다.

# ItemLoader

* 아이템 데이터(ScriptableObject 기반 아이템 정보)를 초기 로드하거나 특정 스킨에 필요한 데이터만 선별합니다.

  
---  


## 📜 NPC & 강화·진화 시스템 (Enhance / Evolve System)
# EnhanceNPC

* 스킨 강화 기능을 제공하는 NPC입니다. 필요 재화와 스탯 상승량을 계산해 PlayerStatus에 적용합니다.

# EvolveNPC

* 특정 조건을 만족하면 스킨 진화를 가능하게 하는 NPC로, EvolutionUpgradeData를 기반으로 성공 여부·재화 소모를 결정합니다.

  
---  

 
## 🗺️ 맵 & UI 시스템 (Map & UI System)
* Door

* 맵 전환, 스테이지 이동을 담당하며 플레이어가 접근했을 때만 활성화됩니다.

# InterectingUi

* 상호작용 가능할 때 화면에 F 버튼 UI를 띄우는 기능을 담당합니다.

# EasyRandom

* 맵 오브젝트를 랜덤 배치하거나, 특정 지형 기반으로 스폰을 조절하는 유틸리티 역할입니다.

# MainSceneUI / PlayerUI

* 플레이어의 체력, 스태미나, 경험치, 현재 스킨 등 게임 진행에 필요한 정보를 HUD 형태로 표시합니다.

  
---  


## 스크립트 구조

```text

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


## 주요 시스템 및 구성 요소 요약

### 상호작용 & 맵 UI

* 플레이어가 NPC나 외피(환경 오브젝트) 근처에 있을 때, 화면에 `F` 키로 가능한 상호작용을 표시합니다.
* 맵 UI는 플레이어의 현재 구간을 동그라미 표시로 알려주며, 일시정지 화면에서도 현재 구역을 쉽게 확인할 수 있게 합니다.

### 스킬 시스템 (범위 검사)

* 스킬 히트박스 계산과 범위 내 적 탐지 기능 포함.
* `SkillHitBoxData` 관련 스크립트가 존재하며, 스킬 발동 시 충돌 검사 및 데미지 적용 로직이 구현되어 있습니다.
* 디버그용 `Debug.DrawLine` 등의 시각화가 스테이지에서 보이도록 구성된 코드 예시가 포함될 수 있습니다.

### 충돌 & 트리거 처리

* 플레이어와 몬스터가 접촉했을 때 트리거를 무시하거나 통과하도록 처리하는 로직 예시(넉백, 접촉 데미지 등).
* `Die()` 함수에서 드랍 생성 후 `Destroy(gameObject)` 하는 기본 처리 포함.

### 드랍 시스템

* 몬스터 사망 시 골드, 스킨 파편, 윙 파편 등 프리팹 드랍을 생성.
* `SpawnDrop(GameObject prefab)` 유틸리티로 프리팹이 null이 아닐 경우 인스턴스화.

### 인벤토리 & UI

* 인벤토리 UI의 오브젝트 구조 예시(슬롯, 아이콘 버튼, 카운트 텍스트, 장착 텍스트 포함).
* 슬롯 기반 인터랙션과 UI 갱신(강화/스탯 변경 시 갱신) 샘플 스크립트 포함.

### 알려진 문제 및 참고사항

* 프로덕션 수준 코드는 아니므로 리팩토링(매직 넘버 상수화, 의존성 주입, 테스트 추가 등)을 권장합니다.

---

---

### 사용법 (요약)
🎮 조작 방법

* 방향키: 이동
* X: 공격
* A: 스킬 1
* S: 스킬 2
* C: 점프
* Z: 대쉬
* F: 상호작용
* ESC: 일시정지 메뉴 열기
