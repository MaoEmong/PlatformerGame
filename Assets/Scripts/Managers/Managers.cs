using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �Ŵ��� ������
public class Managers : MonoBehaviour
{
	static Managers _instance;
	public static Managers Instance { get { return _instance; } }

	// ���ʴ�� ���ҽ�����, �������, ������, ���ӵ����Ͱ���
	ResourceManager _resource = new ResourceManager();
	SoundManager _sound = new SoundManager();
	SceneController _scene = new SceneController();
	GameData _gameData = new GameData();


	public static ResourceManager Resource { get { return Instance._resource; } }
	public static SoundManager Sound { get { return Instance._sound; } }
	public static SceneController Scene { get { return Instance._scene; } }
	public static GameData GameData { get { return Instance._gameData; } }

	public static GameObject Player;

	private void Awake()
	{
		Init();
	}
	static void Init()
	{
		if(_instance == null)
		{
			GameObject go = GameObject.Find("Managers");

			if(go == null)
			{
				go = new GameObject { name = "Managers" };
				go.AddComponent<Managers>();
			}

			DontDestroyOnLoad(go);

			_instance = go.GetComponent<Managers>();

			_instance._sound.Init();
			CreatePlayer();
		}
	}

	public static void Clear()
	{
		Sound.Clear();

	}

	// ���ٽ����� �ڷ�ƾ�� �۵���Ű�� �޼���
	public static void CallWaitForOneFrame(Action act)
	{
		Instance.StartCoroutine(DoCallWaitForOneFrame(act));
	}
	public static void CallWaitForSeconds(float seconds, Action act)
	{
		Instance.StartCoroutine(DoCallWaitForSeconds(seconds, act));
	}

	private static IEnumerator DoCallWaitForOneFrame(Action act)
	{
		yield return 0;
		act();
	}
	private static IEnumerator DoCallWaitForSeconds(float seconds, Action act)
	{
		yield return new WaitForSeconds(seconds);
		act();
	}

	// �÷��̾� ����
	private static void CreatePlayer()
	{
		Player = Managers.Resource.Instantiate("Player/Player");
		Player.transform.position = new Vector3(20, 20, 0);
		DontDestroyOnLoad (Player);
	}

	public static GameObject GetPlayer()
	{
		return Player;
	}

}
