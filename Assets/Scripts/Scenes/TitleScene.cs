using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 타이틀 씬
public class TitleScene : BaseScene
{
	// 타이틀 문자 액션
	GameObject title;

	protected override void Init()
	{
		base.Init();
	}

	protected override void Start()
	{

		type = Define.SceneType.TitleScene;

		Managers.Sound.Play("BGM/Title", Define.Sound.Bgm);

		title = GameObject.FindObjectOfType<TitleText>().gameObject;
		if(title == null)
		{
			Debug.Log("title is NULL");
		}

		// 게임데이터에는 미리 스테이지 선택 씬으로 넣어두기
		// 타이틀씬으로 넣어둬도 상관x
		Managers.GameData.SelectScene = Define.SceneType.StageSelect;
	}

	private void Update()
	{		
		// 타이틀 문자 액션이 끝난 상태일때
		if(title.GetComponent<TitleText>().m_isReadyNext)
		{
			// 스페이스바 입력시 씬 전환
			if(Input.GetKeyDown(KeyCode.Space))
			{
				StartCoroutine(GotoNextScene());
			}
		}		
	}

	IEnumerator GotoNextScene()
	{
		GameObject go = Managers.Resource.Instantiate("UI/FadeInUI");
		Managers.Sound.Play("Effect/FadeIn");
		while(true)
		{
			yield return null;
			if(go.transform.GetChild(0).GetComponent<FadeInChange>().GetImage.fillAmount > 0.99f)
			{				
				Managers.CallWaitForSeconds(0.1f, () => { 
					Managers.Scene.LoadScene(Define.SceneType.StageSelect); 
				});
				break;
			}
		}
	}

	public override void Clear()
	{

	}
}
