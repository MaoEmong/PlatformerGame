using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 닿으면 죽는 블럭
// 낙사위치에 설정하거나 몬스터에게 넣어둠
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
