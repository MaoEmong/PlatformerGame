using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 스테이지로 들어가는 문
public class StageDoor : MonoBehaviour
{
	// 자기자신이 가지는 이동할 씬의 데이터
    [SerializeField]
    Define.SceneType stage = Define.SceneType.Unknown;

	// 닫힌 문 이미지
    [SerializeField]
    Sprite CloseDoorSprite;

	// 열린 문 이미지
    [SerializeField]
    Sprite OpenDoorSprite;

	// 해당 문과 연결된 스테이지 클리어 유무
    public bool m_stageClear = false;

	// 플레이어와 충돌 유무
    private bool stageSelect = false;

	private void Start()
	{
		// 스테이지 클리어 유무에 따른 문 이미지 변화
        if (m_stageClear)
            GetComponent<SpriteRenderer>().sprite = OpenDoorSprite;
        else
            GetComponent<SpriteRenderer>().sprite = CloseDoorSprite;
	}

	private void Update()
	{
		// 현재 문과 연결된 스테이지 클리어시 return
		if (m_stageClear)
			return;

		// 현재 문과 충돌 중일 시
		if(stageSelect)
		{
			// 키입력 시 해당 스테이지로 이동
			if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
			{
				Debug.Log("Input!");
				stageSelect = false;
				Managers.Player.GetComponent<PlayerController>().m_playerMove = false;

				Managers.CallWaitForSeconds(0.2f, () =>
				{
					GetComponent<SpriteRenderer>().sprite = OpenDoorSprite;
					Managers.Sound.Play("Effect/OpenDoor");
					StartCoroutine(GotoNextScene());

				});
			}
		}
	}

	// 플레이어와 충돌시 true
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.CompareTag("Player"))
		{
			stageSelect = true;
		}
	}

	// 플레이어와의 충돌에서 벗어날 시 false
	private void OnTriggerExit2D(Collider2D collision)
	{
		if(collision.CompareTag("Player"))
		{
			stageSelect = false;
		}
	}

	// 선택한 스테이지로 이동
	IEnumerator GotoNextScene()
	{
		yield return new WaitForSeconds(0.8f);

		GameObject go = Managers.Resource.Instantiate("UI/FadeInUI");
		Managers.Sound.Play("Effect/FadeIn");
		while (true)
		{
			yield return null;
			if (go.transform.GetChild(0).GetComponent<FadeInChange>().GetImage.fillAmount > 0.99f)
			{
				Managers.CallWaitForSeconds(0.1f, () => 
				{
					// 게임 데이터의 씬에 이동할 씬데이터 넣어두기
					Managers.GameData.SelectScene = stage;
					Managers.Scene.LoadScene(stage);
				});
				break;
			}
		}
	}

	// 이미지 변환
	public void OpentheDoor()
	{
		GetComponent<SpriteRenderer>().sprite = OpenDoorSprite;

	}

	// 이미지 변환
	public void CloseTheDoor()
	{
		GetComponent<SpriteRenderer>().sprite = CloseDoorSprite;

	}
}
