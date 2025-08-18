using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
	// Plant������ �Ѿ� 
	// �ܼ� �浹�˻縸 ��
	// �����̴°� Plant�� ��ũ��Ʈ���� ����
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
		{
			Destroy(gameObject);
		}
		else if (collision.CompareTag("Player"))
		{
			Managers.Sound.Play("Effect/PlayerDeath");
			collision.gameObject.GetComponent<PlayerController>().PlayerDeath();

			Managers.CallWaitForSeconds(0.8f, () =>
			{
				var scene = FindObjectOfType<Stage>();
				scene.ResetPosition();
			});

			Destroy(gameObject);
		}
	}
}
