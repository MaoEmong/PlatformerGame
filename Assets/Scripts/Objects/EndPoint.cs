using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������ ������Ʈ
public class EndPoint : MonoBehaviour
{
	// �����ߴ��� Ȯ��
	[SerializeField]
	private bool m_isFinished = false;

	// ��ƼŬ �ý���
	[SerializeField]
	ParticleSystem Effect;

	// �浹�˻�
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.CompareTag("Player"))
		{
			if (!m_isFinished)
			{
				// �ǴϽ� �׼�
				Finished();

				// �÷��̾� ������ ����
				Managers.GetPlayer().GetComponent<PlayerController>().m_playerMove = false;
				StartCoroutine(ReturnScene());
			}
		}
	}

	private void Finished()
	{
		// ��ƼŬ ���(1ȸ)
		m_isFinished = true;
		Effect.Play();
		GetComponent<Animator>().Play("EndPointAction");
		Managers.Sound.Play("Effect/EndPoint");

	}

	// �������� ���� ������ ���ư���
	IEnumerator ReturnScene()
	{
		yield return new WaitForSeconds(4.0f);
		GameObject go = Managers.Resource.Instantiate("UI/FadeInUI");
		Managers.Sound.Play("Effect/FadeIn");

		// ���� ���� ������ �޾� ���ӵ����Ϳ� �ݿ�
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
