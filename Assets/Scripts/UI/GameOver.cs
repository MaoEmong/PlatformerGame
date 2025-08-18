using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 게임오버 텍스트 액션
// 타이틀에서 사용한 텍스트 액션과 동일
public class GameOver : MonoBehaviour
{
    [SerializeField]
    Text text;

    private char[] textArray;

    void Start()
    {
		textArray = text.text.ToCharArray();
		text.text = "";

		StartCoroutine(GameOverPopup());
    }

    IEnumerator GameOverPopup()
	{
		int index = 0;

		while (true)
		{
			yield return new WaitForSeconds(0.1f);

			if (textArray[index].ToString() != " ")
				Managers.Sound.Play("Effect/Title");

			text.text += textArray[index];
			if (index + 1 >= textArray.Length)
			{
				StartCoroutine(ReturnSelect());
				break;
			}
			index++;


		}
	}

	IEnumerator ReturnSelect()
	{
		yield return new WaitForSeconds(1.0f);
		GameObject go = Managers.Resource.Instantiate("UI/FadeInUI");
		Managers.Sound.Play("Effect/FadeIn");

		while (true)
		{
			yield return null;
			if (go.transform.GetChild(0).GetComponent<FadeInChange>().GetImage.fillAmount > 0.99f)
			{
				Managers.CallWaitForSeconds(0.1f, () => {
					Managers.GameData.SelectScene = Define.SceneType.StageSelect;
					Managers.Scene.LoadScene(Define.SceneType.StageSelect);
				});
				break;
			}
		}

	}
}
