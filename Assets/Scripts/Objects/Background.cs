using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 백그라운드
public class Background : MonoBehaviour
{
	// 백그라운드 이동 속도
	[SerializeField] Vector2 m_speed;

	// 이미지 정보
	Material material;
	
	private void Start()
	{
		material = GetComponent<SpriteRenderer>().material;
	}

	private void Update()
	{
		// 이미지 정보값에서 위치값 설정
		if (material.mainTextureOffset.x >= 1)
			material.mainTextureOffset = Vector2.zero;
		material.mainTextureOffset += m_speed * Time.deltaTime; 
	}
}
