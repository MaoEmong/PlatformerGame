using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������ �״� ��
// ������ġ�� �����ϰų� ���Ϳ��� �־��
public class DeathBlock : MonoBehaviour
{

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.CompareTag("Player"))
		{
			Managers.Sound.Play("Effect/PlayerDeath");
			collision.gameObject.GetComponent<PlayerController>().PlayerDeath();

			Managers.CallWaitForSeconds(0.8f, () =>
			{				
				var scene = FindObjectOfType<Stage>();
				scene.ResetPosition();
			});

		}
	}


}
