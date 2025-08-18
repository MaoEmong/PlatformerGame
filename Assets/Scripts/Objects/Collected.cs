using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 과일 획득 애니메이션
public class Collected : MonoBehaviour
{
    private Animator animator;

	private void Start()
	{
		animator = GetComponent<Animator>();

		Managers.Sound.Play("Effect/Collected2");
	}

	// 애니메이션 재생 끝나면 해당 오브젝트 파괴
	void Update()
    {
        if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.99f)
		{
            Destroy(gameObject);
		}
    }
}
