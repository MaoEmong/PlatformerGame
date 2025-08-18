using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// üũ����Ʈ ������Ʈ
public class CheckPoint : MonoBehaviour
{
	// üũ Ȯ��
    [SerializeField]
    private bool m_isChecked = false;

	// �浹�˻�
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.CompareTag("Player"))
		{
			// üũ���� �ʾҾ��ٸ�
			if(!m_isChecked)
			{
				// üũ
				Managers.Sound.Play("Effect/FlagOut");
				FlagOut();
				SetStageCheck();
			}
		}
	}

	// ��� ���Ŵ� �ִϸ��̼�
	public void FlagOut()
	{
		m_isChecked = true;
		GetComponent<Animator>().Play("FlagOut");
	}

	// Stage�� ã�� �ش� ��ũ��Ʈ ���� ���̺�����Ʈ��
	// ���� ��ġ�� ����
	private void SetStageCheck()
	{
		Stage stage = FindObjectOfType<Stage>();
		stage.SetCheckPoint(transform);

	}
}
