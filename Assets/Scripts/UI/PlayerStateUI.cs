using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 스테이지에서 사용하는 UI
public class PlayerStateUI : MonoBehaviour
{
	// 하트 프리팹
	public GameObject HeartPrefab;

	// 하트 프리팹을 관리할 parant
	// 유니티의 정렬기능을 사용하여 하트 프리팹을 추가하면
	// 자동으로 정렬하여 화면에 보여줌
	[SerializeField]
	Transform prefabParant;

	// 하트 갯수 관리
	public List<GameObject> heartlist;
	private int heartCount;

	// 점수 표출
	[SerializeField]
	Text Score;

	private void Start()
	{
		// max값 만큼 하트 프리팹 추가
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

	// 스코어 정렬
	private void TextRefresh()
	{
		Score.text = $"Score : {Managers.GetPlayer().GetComponent<PlayerController>().Score}";
	}

	// 하트 프리팹 하나씩 제거
	public void DeleteHeart()
	{
		Destroy(heartlist[heartlist.Count - 1]);
		heartlist.RemoveAt(heartlist.Count - 1);
		
	}
}
