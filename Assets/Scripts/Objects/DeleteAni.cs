using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �ִϸ��̼��� ������ �˾Ƽ� �ı��Ǵ� �޼���
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
