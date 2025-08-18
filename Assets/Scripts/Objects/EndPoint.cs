using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 도착점 오브젝트
public class EndPoint : MonoBehaviour
{
	// 도착했는지 확인
	[SerializeField]
	private bool m_isFinished = false;

	// 파티클 시스템
	[SerializeField]
	ParticleSystem Effect;

	// 충돌검사
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.CompareTag("Player"))
		{
			if (!m_isFinished)
			{
				// 피니시 액션
				Finished();

				// 플레이어 움직임 제한
				Managers.GetPlayer().GetComponent<PlayerController>().m_playerMove = false;
				StartCoroutine(ReturnScene());
			}
		}
	}

	private void Finished()
	{
		// 파티클 재생(1회)
		m_isFinished = true;
		Effect.Play();
		GetComponent<Animator>().Play("EndPointAction");
		Managers.Sound.Play("Effect/EndPoint");

	}

	// 스테이지 선택 씬으로 돌아가기
	IEnumerator ReturnScene()
	{
		yield return new WaitForSeconds(4.0f);
		GameObject go = Managers.Resource.Instantiate("UI/FadeInUI");
		Managers.Sound.Play("Effect/FadeIn");

		// 현재 씬의 정보를 받아 게임데이터에 반영
		switch(Managers.GameData.SelectScene)
		{
			case Define.SceneType.Stage1:
				Managers.GameData.Stage1Clear = true;
				break;
			case Define.SceneType.Stage2:
				Managers.GameData.Stage2Clear = true;
				break;
			case Define.SceneType.Stage3:
				Managers.GameData.Stage3Clear = true;
				break;
		}

		while (true)
		{
			yield return null;
			if (go.transform.GetChild(0).GetComponent<FadeInChange>().GetImage.fillAmount > 0.99f)
			{
				Managers.CallWaitForSeconds(0.1f, () => {
					Managers.GameData.Score += Managers.GetPlayer().GetComponent<PlayerController>().Score;
					Managers.GameData.SelectScene = Define.SceneType.StageSelect;
					Managers.Scene.LoadScene(Define.SceneType.StageSelect);
				});
				break;
			}
		}
	}
}
