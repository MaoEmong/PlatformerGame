using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 코뿔소 스크립트
// 좌우로만 움직이며
// 플레이어 발견 시 빠르게 움직임
public class EnemyRino : EnemyBase
{
	// 평상시 이동속도
	[SerializeField]
	float moveSpeed;
	// 플레이어 발견시 이동속도
	[SerializeField]
	float attackSpeed;

	// 진행방향 설정용
	[SerializeField]
	bool isLeft = false;
	// 플레이어 발견 유무
	[SerializeField]
	bool isChecked = false;

	// 좌우 두개의 레이캐스트
	RaycastHit2D[] hits = new RaycastHit2D[2];

	private void Start()
	{
		animator = GetComponent<Animator>();
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	private void Update()
	{
		// 레이캐스트 발사
		SetRay();
		// 레이캐스트 확인
		CheckRay();
		// 코뿔소 움직임
		RinoMove();
	}

	private void SetRay()
	{
		hits[0] = Physics2D.Raycast(transform.position, Vector3.left, 4f,LayerMask.GetMask("Player"));
		Debug.DrawRay(transform.position, Vector3.left*4f, Color.red);		
		hits[1] = Physics2D.Raycast(transform.position, Vector3.right, 4f, LayerMask.GetMask("Player"));
		Debug.DrawRay(transform.position, Vector3.right*4f, Color.red);
	}
	private void CheckRay()
	{
		// 이미 플레이어를 발견했었다면 return
		if (isChecked)
			return;

		for(int i = 0; i < hits.Length; i++)
		{
			if(hits[i])
			{
				if (hits[i].transform.CompareTag("Player"))
				{
					isChecked = true;
					Debug.Log("Hit!");
					break;
				}
			}
		}
	}
	private void RinoMove()
	{
		float speed;
		
		// 플레이어를 발견한 상태라면 빠른 움직임
		// 아니라면 기본 움직임
		if (isChecked)
			speed = attackSpeed;
		else
			speed = moveSpeed;

		// 현재 재생중인 애니메이션의 이름이 RinoRun일떄
		if (animator.GetCurrentAnimatorStateInfo(0).IsName("RinoRun"))
		{
			// 좌우 방향 확인하여 이동
			if (isLeft)
			{
				spriteRenderer.flipX = false;
				transform.position += Vector3.left * speed * Time.deltaTime;
			}
			else
			{
				spriteRenderer.flipX = true;
				transform.position += Vector3.right * speed * Time.deltaTime;
			}
		}
		// 현재 재생중인 애니메이션이 벽에 충돌한 상태일때
		else if(animator.GetCurrentAnimatorStateInfo(0).IsName("RinoHitWall"))
		{
			// 살짝 밀림
			if (isLeft)
			{
				spriteRenderer.flipX = false;
				transform.position += Vector3.right * (moveSpeed / 2) * Time.deltaTime;
			}
			else
			{
				spriteRenderer.flipX = true;
				transform.position += Vector3.left * (moveSpeed / 2) * Time.deltaTime;
			}
		}
	}

	// 충돌검사
	// 벽충돌만 검사하며
	// 플레이어 충돌은 DeathBlock이라는 다른 스크립트를 하나 더 씌워서 검사중
	private void OnTriggerEnter2D(Collider2D collision)
	{
		// 바닥이 아닌 벽 충돌 시
		if (collision.transform.CompareTag("WallSide"))
		{
			// 좌우 반전
			if (!isChecked)
				isLeft = !isLeft;
			else if (isChecked)
			{
				// 플레이어가 확인되어 빠르게 이동중일시
				// 벽꿍 애니메이션 출력
				animator.Play("RinoHitWall");
				isChecked = false;
				isLeft = !isLeft;

			}
		}
	}

}
