using System.Collections;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public float projectileSpeed = 10f;
    public int damage = 1; // 기본 투사체 피해량
    public float slowAmount = 0.5f; // W 스킬의 감속 비율
    public float slowDuration = 2f; // W 스킬 감속 지속 시간
    public float stunDuration = 1f; // E 스킬 기절 지속 시간

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ShootProjectile(damage: 3); // Q 스킬: 큰 피해
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            ShootProjectile(slowEffect: true); // W 스킬: 감속 효과
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            ShootProjectile(stunEffect: true); // E 스킬: 기절 효과
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

        // 투사체 효과 추가
        ProjectileEffect effect = projectile.AddComponent<ProjectileEffect>();
        effect.Initialize(damage, slowEffect, slowDuration, stunEffect, stunDuration);

        // 투사체 5초 후 파괴
        Destroy(projectile, 5f);
    }

}