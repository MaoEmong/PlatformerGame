using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 스테이지 관리 씬
// 스테이지 1,2,3 통합
public class Stage : BaseScene
{
	// 부활 위치
	[SerializeField]
	Vector3 LastCheckPoint;

	// 스탯UI 프리팹
	[SerializeField]
	GameObject StateUIprefab;

	// 게임오버 프리팹
	[SerializeField]
	GameObject GameOverprefab;

	// 스텟UI 관리용 변수
	public GameObject UIClone { get;private set; }

	// 플레이어 최초 소환 위치
	[SerializeField]
	Transform SpawnPosition;

	protected override void Init()
	{
		base.Init();

		// 현재 씬의 정보 게임데이터에서 받기
		type = Managers.GameData.SelectScene;
		Managers.GetPlayer().transform.position = SpawnPosition.position;
		Managers.GetPlayer().GetComponent<PlayerController>().m_playerMove = true;
		Managers.GetPlayer().GetComponent<PlayerController>().PlayerRebirth();
		LastCheckPoint = SpawnPosition.position;
	}

	protected override void Start()
	{
		base.Start();

		// 받아온 정보에 따라 다른 브금 재생
		switch(type)
		{
			case Define.SceneType.Stage1:
				Managers.Sound.Play("BGM/Stage1", Define.Sound.Bgm);

				break;

			case Define.SceneType.Stage2:
				Managers.Sound.Play("BGM/Stage2", Define.Sound.Bgm);

				break;

			case Define.SceneType.Stage3:
				Managers.Sound.Play("BGM/Stage3", Define.Sound.Bgm);

				break;
		
		
		}

		LastCheckPoint = Managers.GetPlayer().transform.position;

		UIClone = Instantiate(StateUIprefab);
	}

	// 깃발에서 호출하는 메서드
	// 부활위치 조정
	public void SetCheckPoint(Transform transform)
	{
		LastCheckPoint = transform.position;
	}

	// 현재 부활위치 값
	public Vector3 GetCheckPoint()
	{
		return LastCheckPoint;
	}

	// 플레이어 사망 시
	// 하트가 남아있다면 
	// 부활위치에서 플레이어 부활
	public void ResetPosition()
	{
		var ui = UIClone.GetComponent<PlayerStateUI>();

		if(ui.heartlist.Count <= 0)
		{
			Instantiate(GameOverprefab);
		}
		else
		{
			Managers.Sound.Play("Effect/GoBack");
			Managers.GetPlayer().transform.position = LastCheckPoint;
			Managers.GetPlayer().GetComponent<PlayerController>().PlayerRebirth();
			ui.DeleteHeart();
		}
	}

	public override void Clear()
	{

	}
}
