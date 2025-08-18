using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� ��ũ��Ʈ
public class Fruits : MonoBehaviour
{
	// ȹ�� �ִϸ��̼� ������
	[SerializeField]
	GameObject CollectedPrefab;

	// ���� ȹ�� �� �����ϴ� ������
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
