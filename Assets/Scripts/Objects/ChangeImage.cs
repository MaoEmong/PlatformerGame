using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 화면 전환용 이미지
public class ChangeImage : MonoBehaviour
{
    [SerializeField]
    Vector2 m_speed;

	void Update()
    {
        GetComponent<Image>().material.mainTextureOffset += m_speed * Time.deltaTime;
    }
}
