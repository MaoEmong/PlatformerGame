using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	// ���� ĳ���� ����
	[SerializeField]
	private Define.FrogAni CurrentAni = Define.FrogAni.Frog_Idle;

	private Animator animator;
	private Rigidbody2D rigidbody;

	private float hAxis;    // �¿� �Է�

	// �÷��̾� �̵� ���� 
	public bool m_playerMove = true;

	// ���� ����
	[SerializeField]
	private bool m_isJump = false;
	private bool m_isFall = false;

	// ������ �ӵ�
	[SerializeField]
	private float moveSpeed;
	// ���� �ӵ�(�����Ҷ� ���� ��)
	[SerializeField]
	private float jumpSpeed;

	private int HpCount;

	int _score = 0;
	public int Score { get { return _score; } set { _score = value; } }

	// ����׼� ������
	[SerializeField]
	GameObject Deathprefab;
	// ��Ȱ�׼� ������
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

	// Ű�Է�
	private void OnKeboard()
	{
		// �ƹ��� Ű�� �Էµ��� �ʾ��� ���
		if(!Input.anyKey)
		{
			// �������� �ƴϰ� ���� �������� ���� �ƴ϶�� Idle
			if(!m_isJump && CurrentAni != Define.FrogAni.Frog_Fall)
				CurrentAni = Define.FrogAni.Frog_Idle;
		}

		// ������ ������ ���ٸ�
		if (m_playerMove)
		{
			InputMoveKey();
		}

	}

	private void InputMoveKey()
	{
		// �¿� Ű�Է�
		hAxis = Input.GetAxisRaw("Horizontal");

		if (hAxis != 0)
		{
			// ������������ �ƴϰų� �������� �ƴҶ� ������´� �����̴���
			if(CurrentAni != Define.FrogAni.Frog_Fall && !m_isJump)
				CurrentAni = Define.FrogAni.Frog_Move;

			// �Է¹��� ���� �¿� �Ǻ�
			if (hAxis < 0)
				gameObject.GetComponent<SpriteRenderer>().flipX = true;
			else if (hAxis > 0)
				gameObject.GetComponent<SpriteRenderer>().flipX = false;
		}
		// �÷��̾� �̵�
		MovePlayer();

		// �����̽��� ����
		if (Input.GetKeyDown(KeyCode.Space))
		{
			// �������� �ƴҶ��� ����
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

	// ���� ���¿� ���� �÷��̾� �ִϸ��̼�
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

	// �÷��̾� ����׼�
	public void PlayerDeath()
	{
		m_playerMove = false;
		GetComponent<SpriteRenderer>().enabled = false;
		Instantiate(Deathprefab,transform.position,Quaternion.identity);

	}

	// �÷��̾� ��Ȱ �׼Ť�
	public void PlayerRebirth()
	{
		Instantiate(Rebirthprefab, transform.position, Quaternion.identity);

		Managers.CallWaitForSeconds(0.5f, () => 
		{
			m_playerMove = true;
			GetComponent<SpriteRenderer>().enabled = true;
		});
	}

	// �浹�˻�
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

	// ���� �������� ������ �˻�
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
