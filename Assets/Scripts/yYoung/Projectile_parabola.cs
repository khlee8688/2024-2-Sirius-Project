using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifetime = 2f;  // 생존 시간

    void Start()
    {
        // 생존 시간 후 투사체 삭제
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 적과 충돌했을 때
        if (collision.CompareTag("Enemy"))
        {
            EnemyController enemy = collision.GetComponent<EnemyController>();
            if (enemy != null)
            {
                Debug.Log("투사체 명중!");
                enemy.TakeDamage(); // 적에게 피해 주기
            }
            Destroy(gameObject); // 적과 충돌 시 즉시 삭제
        }
        // Ground와 충돌했을 때
        else if (collision.CompareTag("Ground"))
        {
            Debug.Log("투사체가 땅에 떨어짐");
            Destroy(gameObject);
        }
    }
}
