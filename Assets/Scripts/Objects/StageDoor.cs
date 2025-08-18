using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���������� ���� ��
public class StageDoor : MonoBehaviour
{
	// �ڱ��ڽ��� ������ �̵��� ���� ������
    [SerializeField]
    Define.SceneType stage = Define.SceneType.Unknown;

	// ���� �� �̹���
    [SerializeField]
    Sprite CloseDoorSprite;

	// ���� �� �̹���
    [SerializeField]
    Sprite OpenDoorSprite;

	// �ش� ���� ����� �������� Ŭ���� ����
    public bool m_stageClear = false;

	// �÷��̾�� �浹 ����
    private bool stageSelect = false;

	private void Start()
	{
		// �������� Ŭ���� ������ ���� �� �̹��� ��ȭ
        if (m_stageClear)
            GetComponent<SpriteRenderer>().sprite = OpenDoorSprite;
        else
            GetComponent<SpriteRenderer>().sprite = CloseDoorSprite;
	}

	private void Update()
	{
		// ���� ���� ����� �������� Ŭ����� return
		if (m_stageClear)
			return;

		// ���� ���� �浹 ���� ��
		if(stageSelect)
		{
			// Ű�Է� �� �ش� ���������� �̵�
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

	// �÷��̾�� �浹�� true
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.CompareTag("Player"))
		{
			stageSelect = true;
		}
	}

	// �÷��̾���� �浹���� ��� �� false
	private void OnTriggerExit2D(Collider2D collision)
	{
		if(collision.CompareTag("Player"))
		{
			stageSelect = false;
		}
	}

	// ������ ���������� �̵�
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
					// ���� �������� ���� �̵��� �������� �־�α�
					Managers.GameData.SelectScene = stage;
					Managers.Scene.LoadScene(stage);
				});
				break;
			}
		}
	}

	// �̹��� ��ȯ
	public void OpentheDoor()
	{
		GetComponent<SpriteRenderer>().sprite = OpenDoorSprite;

	}

	// �̹��� ��ȯ
	public void CloseTheDoor()
	{
		GetComponent<SpriteRenderer>().sprite = CloseDoorSprite;

	}
}
