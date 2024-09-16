[(UnityWebGL)GamePlayURL](https://coleyoung-game.github.io/Web_ShieldDive/)


낙하하다, 방패

# 개요
온리업이 아닌 온리 다운(feat. 항아리게임)


### 컨셉

멋진 캡틴아메리카처럼 방패로 다 부시면서 착지하는 것이 목표인 게임


### 비주얼과 스타일
픽셀 아트 스타일

게임 플레이

전체적인 플레이: 플레이어는 방패를 사용하여 (아래에서 위로) 떨어지는 물체나 장애물(트래플린, 공, 귀여운 내 뱃살 등 탄성 있어보이는 아무거나)을 처리하며, 최종적으로 목표 지점(공주에게 도착, 해피엔딩) 도달해야함 
/만약 회피나 처리 실패시 위로 붕~~~~~~~~~ 뜨게됨. (빡침 극대화)



방패: 방패는 방어와 공격 기능을 동시에 가지고 있으며, 다양한 상황에서 전략적으로 활용이 필요(공격하면 안되는 장애물 경우 공격시 위로 뜸)



장애물리스트
- 장애물은 크게 2가지로 나뉨.
- 파괴 가능, 파괴 불가능 -을 공격시 플레이어 올라감
- 피격 시 플레이어는 올라간다.
- 몬스터(파괴가능)
- 스파이크(파괴불가)
- 번개 - 혹은 레이저?(파괴불가)
- 폭발물(파괴불가)
- 몬스터 뱃살(파괴불가)?
- 트램폴린
- 커다린 드래곤?(파괴불가-회피필수, 이벤트성으로 주기적 출연)

이런느낌인데 휙 지나갈거라 한두모션만 있어도…(드래곤 힘들면 뺄까요..?ㅠ)
등등 추가예정(이라고 하지만 일단 여기까지만)

### 내려갈 때 플레이어 선택지(장애물 존재)
이동 : 키보드 좌 우 방향키 및 A D 

공격 : 스페이스바 

회피: ctrl키

내려갈 때 플레이어 선택지(장애물 존재)

회피- ctrl키 - (회피는 순간이동 및 무적/쿨타임 긺)

공격 - 스페이스바 - (몬스터 등) - 방패를 내리찍는(방찍누) 모션

<방어 - 스페이스바 - (폭발물은 공격xx) - 방패를 캐릭터가 올려서 막는 모션 - 보류>

장애물 부딪칠 시: 일정 범위로 올라가게됨.(기절모션, 부딪힌 시점~올라간 후 1초 동안 무적/ 이후 원래 게임대로) 


# 맵
하나의 맵으로 쭉 내려가서 바닥에 (공주에게)도달하는 것이 목표
온리업처럼 장애물 헛디딜 시 시작 지점 쪽으로 이동하게 됨(하지만 애는 온리다운이니깐 위로)

지원플렛폼: pc

# 일정:
준비 작업:
- 작업 환경 설정 (Unity 설치 및 프로젝트 초기화)
- 필요한 아트 자원 및 참고 자료 준비
- 기본 프로토타입 제작:
- 기본 플레이어 캐릭터(ad로 이동, 점프, 회피)와 방패 기능(공격/방어) 구현
- 간단한 낙하 메커니즘 구현
맵 디자인:
- 기본 맵 레이아웃 설계 및 구현
- 첫 번째 레벨의 플랫폼 및 장애물 배치
  
장애물 구현:
- 기본 장애물 (몬스터, 스파이크) 구현
- 장애물의 동작 및 상호작용 설정

애니메이션 및 그래픽:
- 플레이어와 장애물의 기본 애니메이션 추가 간단한 픽셀 아트로 기본 그래픽 구현

조작 및 UI 기본 설정:
- 키보드 조작 및 HUD 요소 구현
- 방어/공격 기능의 UI 표시

초기 테스트 및 버그 수정:
- 프로토타입을 테스트하여 기본 기능 확인
- 발견된 버그 수정 및 개선

게임 메커니즘 추가:
- 낙하 메커니즘과 장애물 상호작용 추가
- 방패의 방어 및 공격 기능 세부 조정

디자인 및 기능 개선:
- 장애물과 방패 기능의 개선
- 레벨 디자인 및 난이도 조절

게임 전체 플레이 테스트:
- 전체적인 게임 플레이 테스트
- 버그 수정 및 최종 조정

조정 및 개선:
- 플레이어 피드백을 바탕으로 조정
- 최종 버그 수정 및 개선

사운드 및 음악 추가:
- 게임 내 사운드 효과 및 배경 음악 추가
- 사운드의 조화 및 조정

# 맵
장애물 배치
- 실시간 배치?
- 시간이나 난이도에 따라 배치되는 장애물 수, 종류가 바뀜
- 플레이어 y 값 실시간 저장
- 지면에 공주를 구하면 된다.	

# 플레이어
- 카메라
- 낙하
    - 중력 없이 rigidbody보단 포지션 제어
### 공격
- 스페이스바
- 공격시 낙하속도 빨라짐 (몬스터 피격 성공 시 원래대로 낙하/피격 못할 시 위로)
- 좌, 우 이동 부드럽게
    - 키보드 좌 우 방향키 및 A D 
- 회피
    - ctrl버튼
    - (모션) 방패 안으로 들어감?
    - 잠깐 무적
    - 쿨타임이 있음.
- 기절 상태
    - 장애물 부딪힐 시: 일정 범위로 올라가게됨. (기절모션, 부딪힌 시점~올라간 후 1초 동안 무적/ 이후 원래 게임대로) 


# 장애물
- 파괴가능(몬스터)
- 파괴불가능(스파이크볼/번개/폭발물/몬스터 뱃살?)
- 피격
- (몬스터) 이동
- 온오프?
- UI
- 설정
- 메인메뉴
- 시간 표시?

- 후순위
- 카메라 흔들림
- 피격 /  idle 상태 카메라 무브


# 아카이빙

![2024-09-14 21 37 43](https://github.com/user-attachments/assets/2aa52508-cccf-4861-902f-d0ff16b8d783)



