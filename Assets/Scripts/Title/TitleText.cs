using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleText : MonoBehaviour
{
	// �ϳ��� ������ �ؽ�Ʈ
	[SerializeField]
	Text titleText;

	// �극�� ȿ���� �ִ� �ؽ�Ʈ
	[SerializeField]
	Text pressText;

	// ���ڸ� �ϳ��� ���� ���� char�迭
	char[] TextArray;

	// ���� �׼� �������� Ȯ�̤�
	public bool m_isReadyNext = false;

	private void Start()
	{
		// ���� �ؽ�Ʈ�� ���ڹ迭�� �ְ� �ؽ�Ʈ�� ����α�
		TextArray = titleText.text.ToCharArray();
		titleText.text = "";

		// �극�� ȿ���� ���� �ؽ�Ʈ�� ���ٸ� ������
		if(pressText != null)
			pressText.enabled = false;

		StartCoroutine(DelayTime());
	}

	IEnumerator DelayTime()
	{
		yield return new WaitForSeconds(0.5f);

		StartCoroutine(PopText());
	}

	// ���� �׼�
	IEnumerator PopText()
	{
		int index = 0;

		// ������ �ð����� �ؽ�Ʈ�� ���ڰ� �ϳ��� �߰���
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

	// �극�� ȿ��
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

			// �ؽ�Ʈ�� ������ ������ �� ���̸� ��ȯ��
			if (pressText.color.a >= 0.9f || pressText.color.a <= 0.2f)
				breathDelay *= -1;
		}
	}
}
