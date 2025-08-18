using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ȭ�� ��ȯ�� Ui
// ȭ���� ä��
public class FadeInChange : MonoBehaviour
{
    Image ChangeImage;

    float m_speed;

    public Image GetImage { get { return ChangeImage; } }

    void Start()
    {
        ChangeImage = GetComponent<Image>();

        ChangeImage.fillAmount = 0.0f;

        m_speed = 1.0f;

        StartCoroutine(FadeInDelay());
    }

    IEnumerator FadeInDelay()
	{
        while(true)
		{
            yield return null;
            ChangeImage.fillAmount += m_speed * Time.deltaTime;
		}
	}
}
