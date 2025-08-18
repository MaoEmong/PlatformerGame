using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���ʹ� ���̽� 
// �����ϴ� ������ ������ ���� ���� ������� ����
public abstract class EnemyBase : MonoBehaviour
{
	[SerializeField]
	private Define.EnemyState _state = Define.EnemyState.Idle;

	protected Animator animator;
	protected SpriteRenderer spriteRenderer;

	public Define.EnemyState State { get { return _state; } set { _state = value; } }

}
