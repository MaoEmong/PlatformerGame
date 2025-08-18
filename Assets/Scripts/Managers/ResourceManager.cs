using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���ҽ� ���� Ž���� �Ŵ���
public class ResourceManager
{
	// ������ ��ο��� ������Ʈ�� �ҷ����� ���׸� �޼���
	public T Load<T>(string path) where T : Object
	{
		// ����Ƽ�� Resources Ŭ���� ���
		return Resources.Load<T>(path);
	}

	// ������ ��ο��� ������Ʈ�� �ҷ��� �����ϴ� �޼���
	public GameObject Instantiate(string path, Transform parent = null)
	{
		// �⺻ ��� : Assets/Resources/������ ���
		GameObject prefab = Load<GameObject>($"Prefabs/{path}");
		if (prefab == null)
		{
			// �ش� ������Ʈ�� ���ٸ� null��ȯ
			Debug.Log($"Failed to load prefab : {path}");
			return null;
		}

		// ������Ʈ ����
		return Object.Instantiate(prefab, parent);
	}

	// ������Ʈ �ı� �޼���
	public void Destroy(GameObject go)
	{
		if (go == null)
			return;

		Object.Destroy(go);
	}
}
