using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �ڻԼ� ��ũ��Ʈ
// �¿�θ� �����̸�
// �÷��̾� �߰� �� ������ ������
public class EnemyRino : EnemyBase
{
	// ���� �̵��ӵ�
	[SerializeField]
	float moveSpeed;
	// �÷��̾� �߽߰� �̵��ӵ�
	[SerializeField]
	float attackSpeed;

	// ������� ������
	[SerializeField]
	bool isLeft = false;
	// �÷��̾� �߰� ����
	[SerializeField]
	bool isChecked = false;

	// �¿� �ΰ��� ����ĳ��Ʈ
	RaycastHit2D[] hits = new RaycastHit2D[2];

	private void Start()
	{
		animator = GetComponent<Animator>();
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	private void Update()
	{
		// ����ĳ��Ʈ �߻�
		SetRay();
		// ����ĳ��Ʈ Ȯ��
		CheckRay();
		// �ڻԼ� ������
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
		// �̹� �÷��̾ �߰��߾��ٸ� return
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
		
		// �÷��̾ �߰��� ���¶�� ���� ������
		// �ƴ϶�� �⺻ ������
		if (isChecked)
			speed = attackSpeed;
		else
			speed = moveSpeed;

		// ���� ������� �ִϸ��̼��� �̸��� RinoRun�ϋ�
		if (animator.GetCurrentAnimatorStateInfo(0).IsName("RinoRun"))
		{
			// �¿� ���� Ȯ���Ͽ� �̵�
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
		// ���� ������� �ִϸ��̼��� ���� �浹�� �����϶�
		else if(animator.GetCurrentAnimatorStateInfo(0).IsName("RinoHitWall"))
		{
			// ��¦ �и�
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

	// �浹�˻�
	// ���浹�� �˻��ϸ�
	// �÷��̾� �浹�� DeathBlock�̶�� �ٸ� ��ũ��Ʈ�� �ϳ� �� ������ �˻���
	private void OnTriggerEnter2D(Collider2D collision)
	{
		// �ٴ��� �ƴ� �� �浹 ��
		if (collision.transform.CompareTag("WallSide"))
		{
			// �¿� ����
			if (!isChecked)
				isLeft = !isLeft;
			else if (isChecked)
			{
				// �÷��̾ Ȯ�εǾ� ������ �̵����Ͻ�
				// ���� �ִϸ��̼� ���
				animator.Play("RinoHitWall");
				isChecked = false;
				isLeft = !isLeft;

			}
		}
	}

}
