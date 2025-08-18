using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��׶���
public class Background : MonoBehaviour
{
	// ��׶��� �̵� �ӵ�
	[SerializeField] Vector2 m_speed;

	// �̹��� ����
	Material material;
	
	private void Start()
	{
		material = GetComponent<SpriteRenderer>().material;
	}

	private void Update()
	{
		// �̹��� ���������� ��ġ�� ����
		if (material.mainTextureOffset.x >= 1)
			material.mainTextureOffset = Vector2.zero;
		material.mainTextureOffset += m_speed * Time.deltaTime; 
	}
}
