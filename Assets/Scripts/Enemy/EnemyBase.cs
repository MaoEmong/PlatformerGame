using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 에너미 베이스 
// 등장하는 몬스터의 종류가 적어 굳이 사용하지 않음
public abstract class EnemyBase : MonoBehaviour
{
	[SerializeField]
	private Define.EnemyState _state = Define.EnemyState.Idle;

	protected Animator animator;
	protected SpriteRenderer spriteRenderer;

	public Define.EnemyState State { get { return _state; } set { _state = value; } }

}
