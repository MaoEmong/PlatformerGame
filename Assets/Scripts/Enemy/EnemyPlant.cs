using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Plant��ũ��Ʈ
public class EnemyPlant : EnemyBase
{
    // �÷��̾� Ÿ��
    Transform player;

    // �÷��̾���� �Ÿ� üũ
    [SerializeField]
    float playerDistance;

    // ���� ��Ÿ�
    [SerializeField]
    float attackRange;

    // ���� ������
    [SerializeField]
    float attackDelay;

    // ���� �����Ҽ� �ִ��� Ȯ�ο�
    [SerializeField]
    bool readyAttack = true;

    // �߻��� �Ѿ� ������
    [SerializeField]
    GameObject bulletprefab;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        // ���� ���� �� �÷��̾� ������ �ޱ�
        player = Managers.GetPlayer().transform;
    }

    void Update()
    {
        // �Ÿ� ���
        playerDistance = Vector2.Distance(player.position, transform.position);
        // �¿���� Ȯ��
        spriteFlip();
        // ���� �غ�
        ShootReady();
    }

    private void spriteFlip()
	{
        // ���� �÷��̾��� ��ġ���� �ڽ��� ��ġ���� ���ؼ� �̹��� ����
        if(transform.position.x < player.position.x)
            spriteRenderer.flipX = true;
        else
            spriteRenderer.flipX = false;
	}

    private void ShootReady()
	{
        // �÷��̾���� �Ÿ��� ���ݻ緯�⺸�� �������
        if(playerDistance < attackRange)
		{
            // �����غ� ��ٸ� ����
            if(readyAttack)
			{
                animator.Play("PlantAttack");
                readyAttack = false;
                StartCoroutine(ShootBullet());
                // �ڷ�ƾ ���ٽ� / Managers.CallWaitForSeconds(���ð�, ���ٽ�);
				Managers.CallWaitForSeconds(attackDelay, () => { readyAttack = true; });
			}                
		}
	}

    // �Ѿ� �߻�
    IEnumerator ShootBullet()
	{
        // �Ѿ� ������ ������
        GameObject clone = null;
        while(true)
		{
            yield return null;
            // ���� �ִϸ��̼��� ���� ������ ��ٸ�
            if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.8f)
			{
                // �Ѿ� ����
                clone = Instantiate(bulletprefab, transform.position, Quaternion.identity);
                // �Ѿ� ��ġ�� ����
                clone.transform.position += new Vector3(0, 1.5f, 0);

                break;
			}
		}
        // �Ѿ��� ���� ���� ����
        var offset = player.position - clone.transform.position;
        offset = offset.normalized;

        // �Ѿ��� ����� �ð� ����
        float curTime = 0f;

        while(true)
		{
            yield return null;

            // 2f�� �ð��� ������ �Ѿ��� ���ְ� �ڷ�ƾ ����
            if(curTime > 2f)
			{
                Destroy(clone);
                break;
			}
            // �Ǵ� �Ѿ��� �̹� ������ٸ� ����
            else if(clone == null)
			{
                break;
			}

            clone.transform.position += offset * 6.0f * Time.deltaTime;

            curTime += Time.deltaTime;
		}
	}
}
