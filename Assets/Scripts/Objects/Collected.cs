using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� ȹ�� �ִϸ��̼�
public class Collected : MonoBehaviour
{
    private Animator animator;

	private void Start()
	{
		animator = GetComponent<Animator>();

		Managers.Sound.Play("Effect/Collected2");
	}

	// �ִϸ��̼� ��� ������ �ش� ������Ʈ �ı�
	void Update()
    {
        if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.99f)
		{
            Destroy(gameObject);
		}
    }
}
