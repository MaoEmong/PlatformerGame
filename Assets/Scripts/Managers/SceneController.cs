using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// 씬관리 스크립트
// SceneManager는 이미 유니티자체로 있기때문에
// 이름 변경
public class SceneController
{
    public BaseScene CurrentScene { get { return GameObject.FindObjectOfType<BaseScene>(); } }

    // enum으로 받은 값의 이름과 동일한 씬으로 전환
    public void LoadScene(Define.SceneType type)
    {
        Managers.Clear();

        SceneManager.LoadScene(GetSceneName(type));
    }

    // 인수로 받은 씬 이름 받기
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
