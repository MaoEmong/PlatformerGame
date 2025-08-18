using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
	// Plant몬스터의 총알 
	// 단순 충돌검사만 함
	// 움직이는건 Plant의 스크립트에서 진행
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
