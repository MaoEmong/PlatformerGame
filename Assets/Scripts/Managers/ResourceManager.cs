using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 리소스 파일 탐색용 매니저
public class ResourceManager
{
	// 설정한 경로에서 오브젝트를 불러오는 제네릭 메서드
	public T Load<T>(string path) where T : Object
	{
		// 유니티의 Resources 클래스 사용
		return Resources.Load<T>(path);
	}

	// 설정한 경로에서 오브젝트를 불러와 생성하는 메서드
	public GameObject Instantiate(string path, Transform parent = null)
	{
		// 기본 경로 : Assets/Resources/설정한 경로
		GameObject prefab = Load<GameObject>($"Prefabs/{path}");
		if (prefab == null)
		{
			// 해당 오브젝트가 없다면 null반환
			Debug.Log($"Failed to load prefab : {path}");
			return null;
		}

		// 오브젝트 생성
		return Object.Instantiate(prefab, parent);
	}

	// 오브젝트 파괴 메서드
	public void Destroy(GameObject go)
	{
		if (go == null)
			return;

		Object.Destroy(go);
	}
}
