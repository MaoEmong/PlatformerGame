using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
	// 씬 정보
	public enum SceneType
	{
		Unknown,
		TitleScene,
		StageSelect,
		Stage1,
		Stage2,
		Stage3,

	}

	// 사운드 종류
	public enum Sound
	{
		Bgm,
		Effect,
		MaxCount,
	}

	// 플레이어 상태
	public enum FrogAni
	{
		Frog_Idle,
		Frog_Move,
		Frog_Jump,
		Frog_Fall,

	}

	// 몬스터 상태
	// 사용안함
	public enum EnemyState
	{
		Idle,
		Attack,
		Move,

	}

}
