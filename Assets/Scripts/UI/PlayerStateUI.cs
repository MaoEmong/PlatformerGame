using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ������������ ����ϴ� UI
public class PlayerStateUI : MonoBehaviour
{
	// ��Ʈ ������
	public GameObject HeartPrefab;

	// ��Ʈ �������� ������ parant
	// ����Ƽ�� ���ı���� ����Ͽ� ��Ʈ �������� �߰��ϸ�
	// �ڵ����� �����Ͽ� ȭ�鿡 ������
	[SerializeField]
	Transform prefabParant;

	// ��Ʈ ���� ����
	public List<GameObject> heartlist;
	private int heartCount;

	// ���� ǥ��
	[SerializeField]
	Text Score;

	private void Start()
	{
		// max�� ��ŭ ��Ʈ ������ �߰�
		heartlist = new List<GameObject>();
		heartCount = Managers.GameData.MaxHp;
		for (int i = 0; i < heartCount; i++)
		{
			heartlist.Add( Instantiate(HeartPrefab,prefabParant));
		}
	}

	private void Update()
	{
		TextRefresh();
	}

	// ���ھ� ����
	private void TextRefresh()
	{
		Score.text = $"Score : {Managers.GetPlayer().GetComponent<PlayerController>().Score}";
	}

	// ��Ʈ ������ �ϳ��� ����
	public void DeleteHeart()
	{
		Destroy(heartlist[heartlist.Count - 1]);
		heartlist.RemoveAt(heartlist.Count - 1);
		
	}
}
