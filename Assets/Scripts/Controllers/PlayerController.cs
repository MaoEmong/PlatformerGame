using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	// 현재 캐릭터 상태
	[SerializeField]
	private Define.FrogAni CurrentAni = Define.FrogAni.Frog_Idle;

	private Animator animator;
	private Rigidbody2D rigidbody;

	private float hAxis;    // 좌우 입력

	// 플레이어 이동 제한 
	public bool m_playerMove = true;

	// 현재 상태
	[SerializeField]
	private bool m_isJump = false;
	private bool m_isFall = false;

	// 움직임 속도
	[SerializeField]
	private float moveSpeed;
	// 점프 속도(점프할때 들어가는 힘)
	[SerializeField]
	private float jumpSpeed;

	private int HpCount;

	int _score = 0;
	public int Score { get { return _score; } set { _score = value; } }

	// 사망액션 프리팹
	[SerializeField]
	GameObject Deathprefab;
	// 부활액션 프리팹
	[SerializeField]
	GameObject Rebirthprefab;

	private void Start()
	{
		animator = GetComponent<Animator>();
		rigidbody = GetComponent<Rigidbody2D>();

		HpCount = Managers.GameData.MaxHp;
	}

	private void Update()
	{
		OnKeboard();

		PlayAni();
	}

	public int GetHpCount()
	{
		return HpCount;
	}

	// 키입력
	private void OnKeboard()
	{
		// 아무런 키도 입력되지 않았을 경우
		if(!Input.anyKey)
		{
			// 점프중이 아니고 현재 떨어지는 중이 아니라면 Idle
			if(!m_isJump && CurrentAni != Define.FrogAni.Frog_Fall)
				CurrentAni = Define.FrogAni.Frog_Idle;
		}

		// 움직임 제한이 없다면
		if (m_playerMove)
		{
			InputMoveKey();
		}

	}

	private void InputMoveKey()
	{
		// 좌우 키입력
		hAxis = Input.GetAxisRaw("Horizontal");

		if (hAxis != 0)
		{
			// 떨어지는중이 아니거나 점프중이 아닐때 현재상태는 움직이는중
			if(CurrentAni != Define.FrogAni.Frog_Fall && !m_isJump)
				CurrentAni = Define.FrogAni.Frog_Move;

			// 입력받은 값의 좌우 판별
			if (hAxis < 0)
				gameObject.GetComponent<SpriteRenderer>().flipX = true;
			else if (hAxis > 0)
				gameObject.GetComponent<SpriteRenderer>().flipX = false;
		}
		// 플레이어 이동
		MovePlayer();

		// 스페이스바 점프
		if (Input.GetKeyDown(KeyCode.Space))
		{
			// 점프중이 아닐때만 가능
			if(!m_isJump)
			{
				m_isJump = true;
				CurrentAni = Define.FrogAni.Frog_Jump;
				rigidbody.AddForce(Vector3.up * jumpSpeed, ForceMode2D.Impulse);
			}
		}
	}

	private void MovePlayer()
	{
		gameObject.transform.position += new Vector3(hAxis,0,0) * moveSpeed * Time.deltaTime;
	}

	// 현재 상태에 따른 플레이어 애니메이션
	private void PlayAni()
	{
		switch(CurrentAni)
		{
			case Define.FrogAni.Frog_Idle:
				animator.Play("Frog_Idle");
				break;

			case Define.FrogAni.Frog_Move:
				animator.Play("Frog_Move");
				break;

			case Define.FrogAni.Frog_Jump:
				animator.Play("Frog_Jump");
				break;

			case Define.FrogAni.Frog_Fall:
				animator.Play("Frog_Fall");
				break;
		}
	}

	// 플레이어 사망액션
	public void PlayerDeath()
	{
		m_playerMove = false;
		GetComponent<SpriteRenderer>().enabled = false;
		Instantiate(Deathprefab,transform.position,Quaternion.identity);

	}

	// 플레이어 부활 액셔ㄴ
	public void PlayerRebirth()
	{
		Instantiate(Rebirthprefab, transform.position, Quaternion.identity);

		Managers.CallWaitForSeconds(0.5f, () => 
		{
			m_playerMove = true;
			GetComponent<SpriteRenderer>().enabled = true;
		});
	}

	// 충돌검사
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
		{
			if (collision.gameObject.CompareTag("WallBottom") && m_isJump)
			{
				CurrentAni = Define.FrogAni.Frog_Idle;
				m_isJump = false;

				Managers.Sound.Play("Effect/Randing");
			}

			if (collision.gameObject.CompareTag("WallBottom") && m_isFall)
			{
				CurrentAni = Define.FrogAni.Frog_Idle;
				m_isFall = false;

				Managers.Sound.Play("Effect/Randing");
			}
		}
	}

	// 현재 떨어지는 중인지 검사
	private void OnCollisionExit2D(Collision2D collision)
	{
		if(collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
		{
			if (collision.gameObject.CompareTag("WallBottom"))
			{
				if (!m_isJump)
				{
					CurrentAni = Define.FrogAni.Frog_Fall;
					m_isFall = true;
				}
			}
		}

	}
}
