using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    GameObject player;

    // 카메라 기본값
    Vector3 cameraPos = new Vector3(0, 0, -20);

    // 카메라 움직임 속도
    [SerializeField]
    float moveSpeed = 5.0f;

    // 카메라 이동 제한 크기의 중심점
    [SerializeField]
    Vector2 center;
    // 카메라 이동 제한 크기
    [SerializeField]
    Vector2 mapSize;

    float height;
    float width;
    void Start()
    {
        // 플레이어 데이터 받아오기
        player = Managers.GetPlayer();

        // 카메라의 화면 크기 설정
        height = Camera.main.orthographicSize;
        width = height * Screen.width / Screen.height;
    }

    void LateUpdate()
    {
        CameraMove();
    }

    private void CameraMove()
	{
        // 카메라의 선형보간
        transform.position = Vector3.Lerp(transform.position, 
            player.transform.position + cameraPos, 
            moveSpeed * Time.deltaTime);

        // 카메라 가두기
        float lx = mapSize.x - width;
        float clampX = Mathf.Clamp(transform.position.x, -lx + center.x, lx + center.x);

        float ly = mapSize.y - height;
        float clampY = Mathf.Clamp(transform.position.y, -ly + center.y, ly + center.y);

        transform.position = new Vector3(clampX, clampY, -20f);
    }

    // 가두는 공간 그리기
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, mapSize * 2);
    }
}
