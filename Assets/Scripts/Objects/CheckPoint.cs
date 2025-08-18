using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 체크포인트 오브젝트
public class CheckPoint : MonoBehaviour
{
	// 체크 확인
    [SerializeField]
    private bool m_isChecked = false;

	// 충돌검사
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.CompareTag("Player"))
		{
			// 체크하지 않았었다면
			if(!m_isChecked)
			{
				// 체크
				Managers.Sound.Play("Effect/FlagOut");
				FlagOut();
				SetStageCheck();
			}
		}
	}

	// 깃발 내거는 애니메이션
	public void FlagOut()
	{
		m_isChecked = true;
		GetComponent<Animator>().Play("FlagOut");
	}

	// Stage를 찾아 해당 스크립트 내의 세이브포인트에
	// 현재 위치값 설정
	private void SetStageCheck()
	{
		Stage stage = FindObjectOfType<Stage>();
		stage.SetCheckPoint(transform);

	}
}
