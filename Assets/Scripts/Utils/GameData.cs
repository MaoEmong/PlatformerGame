using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData
{
	int StageMaxHp = 3;
	int score = 0;

	Define.SceneType select;

	bool stage1Clear = false;
	bool stage2Clear = false;
	bool stage3Clear = false;

	public int MaxHp { get { return StageMaxHp; } }

	public int Score { get { return score; } set { score = value; } }

	public bool Stage1Clear { get { return stage1Clear; } set { stage1Clear = value; } }
	public bool Stage2Clear { get { return stage2Clear; } set { stage2Clear = value; } }
	public bool Stage3Clear { get { return stage3Clear; } set { stage3Clear = value; } }

	public Define.SceneType SelectScene { get { return select; } set { select = value; } }
}
