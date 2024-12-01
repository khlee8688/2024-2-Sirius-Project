using System.Collections;
using UnityEngine;

public class ProjectileEffect : MonoBehaviour
{
    private int damage;
    private bool slowEffect;
    private float slowDuration;
    private bool stunEffect;
    private float stunDuration;

    public void Initialize(int damage, bool slowEffect, float slowDuration, bool stunEffect, float stunDuration)
    {
        this.damage = damage;
        this.slowEffect = slowEffect;
        this.slowDuration = slowDuration;
        this.stunEffect = stunEffect;
        this.stunDuration = stunDuration;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);

                if (slowEffect)
                {
                    enemy.ApplySlow(slowDuration, 0.5f); // W 스킬 감속
                }

                if (stunEffect)
                {
                    enemy.ApplyStun(stunDuration); // E 스킬 기절
                }
            }

            Destroy(gameObject); // 투사체 소멸
        }
    }
}