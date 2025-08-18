using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ÿ��Ʋ ��
public class TitleScene : BaseScene
{
	// Ÿ��Ʋ ���� �׼�
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

		// ���ӵ����Ϳ��� �̸� �������� ���� ������ �־�α�
		// Ÿ��Ʋ������ �־�ֵ� ���x
		Managers.GameData.SelectScene = Define.SceneType.StageSelect;
	}

	private void Update()
	{		
		// Ÿ��Ʋ ���� �׼��� ���� �����϶�
		if(title.GetComponent<TitleText>().m_isReadyNext)
		{
			// �����̽��� �Է½� �� ��ȯ
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
