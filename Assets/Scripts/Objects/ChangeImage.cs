using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ȭ�� ��ȯ�� �̹���
public class ChangeImage : MonoBehaviour
{
    [SerializeField]
    Vector2 m_speed;

	void Update()
    {
        GetComponent<Image>().material.mainTextureOffset += m_speed * Time.deltaTime;
    }
}
