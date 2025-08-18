using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �������� ���� ��
// �������� 1,2,3 ����
public class Stage : BaseScene
{
	// ��Ȱ ��ġ
	[SerializeField]
	Vector3 LastCheckPoint;

	// ����UI ������
	[SerializeField]
	GameObject StateUIprefab;

	// ���ӿ��� ������
	[SerializeField]
	GameObject GameOverprefab;

	// ����UI ������ ����
	public GameObject UIClone { get;private set; }

	// �÷��̾� ���� ��ȯ ��ġ
	[SerializeField]
	Transform SpawnPosition;

	protected override void Init()
	{
		base.Init();

		// ���� ���� ���� ���ӵ����Ϳ��� �ޱ�
		type = Managers.GameData.SelectScene;
		Managers.GetPlayer().transform.position = SpawnPosition.position;
		Managers.GetPlayer().GetComponent<PlayerController>().m_playerMove = true;
		Managers.GetPlayer().GetComponent<PlayerController>().PlayerRebirth();
		LastCheckPoint = SpawnPosition.position;
	}

	protected override void Start()
	{
		base.Start();

		// �޾ƿ� ������ ���� �ٸ� ��� ���
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

	// ��߿��� ȣ���ϴ� �޼���
	// ��Ȱ��ġ ����
	public void SetCheckPoint(Transform transform)
	{
		LastCheckPoint = transform.position;
	}

	// ���� ��Ȱ��ġ ��
	public Vector3 GetCheckPoint()
	{
		return LastCheckPoint;
	}

	// �÷��̾� ��� ��
	// ��Ʈ�� �����ִٸ� 
	// ��Ȱ��ġ���� �÷��̾� ��Ȱ
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
