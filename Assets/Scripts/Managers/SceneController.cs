using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// ������ ��ũ��Ʈ
// SceneManager�� �̹� ����Ƽ��ü�� �ֱ⶧����
// �̸� ����
public class SceneController
{
    public BaseScene CurrentScene { get { return GameObject.FindObjectOfType<BaseScene>(); } }

    // enum���� ���� ���� �̸��� ������ ������ ��ȯ
    public void LoadScene(Define.SceneType type)
    {
        Managers.Clear();

        SceneManager.LoadScene(GetSceneName(type));
    }

    // �μ��� ���� �� �̸� �ޱ�
    string GetSceneName(Define.SceneType type)
    {
        string name = System.Enum.GetName(typeof(Define.SceneType), type);
        return name;
    }

    public void Clear()
    {
        CurrentScene.Clear();
    }

}
