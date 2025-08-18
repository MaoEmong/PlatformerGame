using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �������� ���� ��
public class SelectScene : BaseScene
{
	// �������� ���� ��
	[SerializeField]
	List<GameObject> Doors;

	// �������� Ŭ���� ���� Ȯ�ο� ���
	[SerializeField]
	List<GameObject> Flags;

	// �÷��̾� ���� ��ġ
	[SerializeField]
	Transform SpawnPosition;

	protected override void Init()
	{
		base.Init();

		// ���� ���� ������ StageSelect
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

		// ���ӵ����Ϳ��� �������� Ŭ���� ���� �޾ƿ�
		// ���� ��߿� �� ����
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
