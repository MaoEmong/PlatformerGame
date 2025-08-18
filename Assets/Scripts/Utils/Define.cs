using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
	// �� ����
	public enum SceneType
	{
		Unknown,
		TitleScene,
		StageSelect,
		Stage1,
		Stage2,
		Stage3,

	}

	// ���� ����
	public enum Sound
	{
		Bgm,
		Effect,
		MaxCount,
	}

	// �÷��̾� ����
	public enum FrogAni
	{
		Frog_Idle,
		Frog_Move,
		Frog_Jump,
		Frog_Fall,

	}

	// ���� ����
	// ������
	public enum EnemyState
	{
		Idle,
		Attack,
		Move,

	}

}
