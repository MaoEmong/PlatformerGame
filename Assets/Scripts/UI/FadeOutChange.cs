using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 화면 전환용 UI
// 화면을 비움
public class FadeOutChange : MonoBehaviour
{
    Image ChangeImage;

    float m_speed;

    void Start()
    {
        ChangeImage = GetComponent<Image>();

        ChangeImage.fillAmount = 1.0f;

        m_speed = 1.0f;

        StartCoroutine(FadeInDelay());
    }

    IEnumerator FadeInDelay()
    {
        while (true)
        {
            yield return null;
            ChangeImage.fillAmount -= m_speed * Time.deltaTime;

            if(ChangeImage.fillAmount <= 0.0f)
			{
                Destroy(transform.parent.gameObject);
                
			}
        }
    }
}
