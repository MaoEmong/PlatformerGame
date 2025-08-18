using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 애니메이션이 끝나면 알아서 파괴되는 메서드
public class DeleteAni : MonoBehaviour
{
    Animator animator;

	private void Start()
	{
		animator = GetComponent<Animator>();
	}

	void Update()
    {
        if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.99f)
		{
            Destroy(gameObject);
		}
    }
}
