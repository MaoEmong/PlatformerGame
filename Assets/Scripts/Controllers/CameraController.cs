using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    GameObject player;

    // ī�޶� �⺻��
    Vector3 cameraPos = new Vector3(0, 0, -20);

    // ī�޶� ������ �ӵ�
    [SerializeField]
    float moveSpeed = 5.0f;

    // ī�޶� �̵� ���� ũ���� �߽���
    [SerializeField]
    Vector2 center;
    // ī�޶� �̵� ���� ũ��
    [SerializeField]
    Vector2 mapSize;

    float height;
    float width;
    void Start()
    {
        // �÷��̾� ������ �޾ƿ���
        player = Managers.GetPlayer();

        // ī�޶��� ȭ�� ũ�� ����
        height = Camera.main.orthographicSize;
        width = height * Screen.width / Screen.height;
    }

    void LateUpdate()
    {
        CameraMove();
    }

    private void CameraMove()
	{
        // ī�޶��� ��������
        transform.position = Vector3.Lerp(transform.position, 
            player.transform.position + cameraPos, 
            moveSpeed * Time.deltaTime);

        // ī�޶� ���α�
        float lx = mapSize.x - width;
        float clampX = Mathf.Clamp(transform.position.x, -lx + center.x, lx + center.x);

        float ly = mapSize.y - height;
        float clampY = Mathf.Clamp(transform.position.y, -ly + center.y, ly + center.y);

        transform.position = new Vector3(clampX, clampY, -20f);
    }

    // ���δ� ���� �׸���
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, mapSize * 2);
    }
}
