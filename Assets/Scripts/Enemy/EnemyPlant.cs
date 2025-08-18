using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Plant스크립트
public class EnemyPlant : EnemyBase
{
    // 플레이어 타겟
    Transform player;

    // 플레이어와의 거리 체크
    [SerializeField]
    float playerDistance;

    // 공격 사거리
    [SerializeField]
    float attackRange;

    // 공격 딜레이
    [SerializeField]
    float attackDelay;

    // 현재 공격할수 있는지 확인용
    [SerializeField]
    bool readyAttack = true;

    // 발사할 총알 프리팹
    [SerializeField]
    GameObject bulletprefab;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        // 최초 시작 시 플레이어 데이터 받기
        player = Managers.GetPlayer().transform;
    }

    void Update()
    {
        // 거리 계산
        playerDistance = Vector2.Distance(player.position, transform.position);
        // 좌우반전 확인
        spriteFlip();
        // 공격 준비
        ShootReady();
    }

    private void spriteFlip()
	{
        // 현재 플레이어의 위치값과 자신의 위치값을 비교해서 이미지 반전
        if(transform.position.x < player.position.x)
            spriteRenderer.flipX = true;
        else
            spriteRenderer.flipX = false;
	}

    private void ShootReady()
	{
        // 플레이어와의 거리가 공격사러기보다 가까울경우
        if(playerDistance < attackRange)
		{
            // 공격준비가 됬다면 공격
            if(readyAttack)
			{
                animator.Play("PlantAttack");
                readyAttack = false;
                StartCoroutine(ShootBullet());
                // 코루틴 람다식 / Managers.CallWaitForSeconds(대기시간, 람다식);
				Managers.CallWaitForSeconds(attackDelay, () => { readyAttack = true; });
			}                
		}
	}

    // 총알 발사
    IEnumerator ShootBullet()
	{
        // 총알 관리용 프리팹
        GameObject clone = null;
        while(true)
		{
            yield return null;
            // 현재 애니메이션이 거의 진행이 됬다면
            if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.8f)
			{
                // 총알 생성
                clone = Instantiate(bulletprefab, transform.position, Quaternion.identity);
                // 총알 위치값 조절
                clone.transform.position += new Vector3(0, 1.5f, 0);

                break;
			}
		}
        // 총알의 진행 방향 설정
        var offset = player.position - clone.transform.position;
        offset = offset.normalized;

        // 총알이 사라질 시간 설정
        float curTime = 0f;

        while(true)
		{
            yield return null;

            // 2f의 시간이 지나면 총알을 없애고 코루틴 종료
            if(curTime > 2f)
			{
                Destroy(clone);
                break;
			}
            // 또는 총알이 이미 사라졌다면 종료
            else if(clone == null)
			{
                break;
			}

            clone.transform.position += offset * 6.0f * Time.deltaTime;

            curTime += Time.deltaTime;
		}
	}
}
