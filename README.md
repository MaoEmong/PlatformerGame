# 플랫포머어드벤처
Unity Tilemap으로 제작한 간단한 플랫포머 어드벤처. PC 키보드 조작 기반의 짧은 플레이 타임을 목표로 빠르게 개발했습니다.

# 게임 개요

장르: 2D 플랫포머 / 어드벤처(라이트)

플랫폼: PC (키보드 입력)

개발 기간: 짧은 스코프

핵심 루프: 이동/점프 → 적/함정 회피 → 과일오브젝트 획득(점수) → 골 도달

레벨 구성: Unity Tilemap을 사용해 타일 단위로 맵 제작(3개 스테이지)

# 주요 기능

Tilemap 기반 레벨

Tilemap + TilemapCollider2D 구성

키보드 입력(PC)

매니저 싱글톤 구조

적 캐릭터 상속 구조

EnemyBase(공통 이동/피해/사망 로직) → EnemyPlant, EnemyRino 파생

공통 인터페이스 유지로 확장/추가가 용이

월드 오브젝트 프리팹화

몬스터/세이브포인트/도착지점/코인 등 모든 상호작용 오브젝트를 프리팹으로 모듈화

# 기술 스택

Engine: Unity 2021.3.16f1 (LTS)

Language: C#

Systems: Tilemap, 2D Physics(BoxCollider2D, Rigidbody2D)

Patterns: Singleton(매니저), 상속(EnemyBase → Enemy*)
