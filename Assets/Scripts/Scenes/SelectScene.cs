using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 스테이지 선택 씬
public class SelectScene : BaseScene
{
	// 스테이지 선택 문
	[SerializeField]
	List<GameObject> Doors;

	// 스테이지 클리어 유무 확인용 깃발
	[SerializeField]
	List<GameObject> Flags;

	// 플레이어 스폰 위치
	[SerializeField]
	Transform SpawnPosition;

	protected override void Init()
	{
		base.Init();

		// 현재 씬의 정보는 StageSelect
		type = Define.SceneType.StageSelect;
		Managers.GetPlayer().transform.position = SpawnPosition.position;
		Managers.GetPlayer().GetComponent<PlayerController>().m_playerMove = true;
		Managers.GetPlayer().GetComponent<PlayerController>().PlayerRebirth();
		Managers.GetPlayer().GetComponent<PlayerController>().Score = 0;
	}

	protected override void Start()
	{
		base.Start();

		Managers.Sound.Play("BGM/StageSelect", Define.Sound.Bgm);

		// 게임데이터에서 스테이지 클리어 유무 받아와
		// 문과 깃발에 값 적용
		if(Managers.GameData.Stage1Clear)
		{
			var door = Doors[0].GetComponent<StageDoor>();
			door.OpentheDoor();
			door.m_stageClear = true;

			var flag = Flags[0].GetComponent<CheckPoint>();
			flag.FlagOut();
		}
		if (Managers.GameData.Stage2Clear)
		{
			var door = Doors[1].GetComponent<StageDoor>();
			door.OpentheDoor();
			door.m_stageClear = true;

			var flag = Flags[1].GetComponent<CheckPoint>();
			flag.FlagOut();
		}
		if (Managers.GameData.Stage3Clear)
		{
			var door = Doors[2].GetComponent<StageDoor>();
			door.OpentheDoor();
			door.m_stageClear = true;

			var flag = Flags[2].GetComponent<CheckPoint>();
			flag.FlagOut();
		}
	}

	public override void Clear()
	{

	}
}
