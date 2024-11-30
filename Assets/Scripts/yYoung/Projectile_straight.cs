using UnityEngine;

// 직선으로 날아가는 투사체
public class StraightProjectile : MonoBehaviour
{
    public float lifetime = 2f;      // 생존 시간
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
            knockBack(collision);
            // 투사체 삭제
            Destroy(gameObject);
        }
        // Ground와 충돌했을 때
        else if (collision.CompareTag("Ground"))
        {
            Debug.Log("투사체가 땅에 떨어짐");
            Destroy(gameObject);
        }
    }

    private void knockBack(Collider2D collision){
        // EnemyController 스크립트 가져오기
            EnemyController enemy = collision.GetComponent<EnemyController>();

            if (enemy != null)
            {
                Debug.Log("투사체 명중!");
                // 충돌한 적과의 상대적 방향 계산
                // collision.transform.position 충돌한 오브젝트(적)의 위치.
                // transform.position: 현재 오브젝트(투사체)의 위치.
                Vector2 bulletDirection = (transform.position - collision.transform.position).normalized;

                Debug.Log($"넉백 방향1: {bulletDirection}");
                Debug.Log($"투사체의 위치: {transform.position}");
                Debug.Log($"적의 위치: {collision.transform.position}");
                
                // enemy.TakeDamage(); // 적에게 피해 주기
                enemy.TakeDamage(bulletDirection);
            }
    }
}




