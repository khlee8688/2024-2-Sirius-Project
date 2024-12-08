using System.Collections;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public float projectileSpeed = 10f;
    public int damage = 1; // �⺻ ����ü ���ط�
    public float slowAmount = 0.5f; // W ��ų�� ���� ����
    public float slowDuration = 2f; // W ��ų ���� ���� �ð�
    public float stunDuration = 1f; // E ��ų ���� ���� �ð�

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ShootProjectile(damage: 3); // Q ��ų: ū ����
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            ShootProjectile(slowEffect: true); // W ��ų: ���� ȿ��
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            ShootProjectile(stunEffect: true); // E ��ų: ���� ȿ��
        }
    }

    void ShootProjectile(int damage = 1, bool slowEffect = false, bool stunEffect = false)
    {

        GameObject projectile = new GameObject("Projectile");
        projectile.transform.position = transform.position;


        Rigidbody2D rb = projectile.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.velocity = transform.right * projectileSpeed;


        CircleCollider2D collider = projectile.AddComponent<CircleCollider2D>();
        collider.isTrigger = true;

        // ����ü ȿ�� �߰�
        ProjectileEffect effect = projectile.AddComponent<ProjectileEffect>();
        effect.Initialize(damage, slowEffect, slowDuration, stunEffect, stunDuration);

        // ����ü 5�� �� �ı�
        Destroy(projectile, 5f);
    }

}