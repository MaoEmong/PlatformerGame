using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleText : MonoBehaviour
{
	// 하나씩 찍히는 텍스트
	[SerializeField]
	Text titleText;

	// 브레스 효과를 주는 텍스트
	[SerializeField]
	Text pressText;

	// 문자를 하나씩 찍어내기 위한 char배열
	char[] TextArray;

	// 문자 액션 끝났는지 확이ㄴ
	public bool m_isReadyNext = false;

	private void Start()
	{
		// 현재 텍스트를 문자배열에 넣고 텍스트는 비워두기
		TextArray = titleText.text.ToCharArray();
		titleText.text = "";

		// 브레스 효과를 받을 텍스트가 없다면 가리기
		if(pressText != null)
			pressText.enabled = false;

		StartCoroutine(DelayTime());
	}

	IEnumerator DelayTime()
	{
		yield return new WaitForSeconds(0.5f);

		StartCoroutine(PopText());
	}

	// 문자 액션
	IEnumerator PopText()
	{
		int index = 0;

		// 일정한 시간마다 텍스트에 문자가 하나씩 추가됨
		while(true)
		{
			yield return new WaitForSeconds(0.1f);

			if(TextArray[index].ToString() != " ")
				Managers.Sound.Play("Effect/Title");

			titleText.text += TextArray[index];
			if (index + 1 >= TextArray.Length)
			{
				if(pressText != null)
					StartCoroutine(BreathText());
				break;
			}
			index++;

			
		}
	}

	// 브레스 효과
	IEnumerator BreathText()
	{
		yield return new WaitForSeconds(0.5f);

		pressText.enabled = true;

		m_isReadyNext = true;

		float breathDelay = 0.2f;

		while(true)
		{
			yield return new WaitForSeconds(0.1f);

			pressText.color += new Color(0,0,0,breathDelay);

			// 텍스트의 투명도가 각각의 값 사이를 순환함
			if (pressText.color.a >= 0.9f || pressText.color.a <= 0.2f)
				breathDelay *= -1;
		}
	}
}
