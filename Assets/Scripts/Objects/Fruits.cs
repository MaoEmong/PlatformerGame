using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 과일 스크립트
public class Fruits : MonoBehaviour
{
	// 획득 애니메이션 프리팹
	[SerializeField]
	GameObject CollectedPrefab;

	// 과일 획득 시 증가하는 점수값
	[SerializeField]
	int addScore;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.CompareTag("Player"))
		{
			Instantiate(CollectedPrefab,transform.position,Quaternion.identity);

			Managers.GetPlayer().GetComponent<PlayerController>().Score += addScore;

			Destroy(gameObject);
		}
	}
}
