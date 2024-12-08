using UnityEngine;
// 포물선을 그리며 날아가는 투사체
public class Projectile : MonoBehaviour
{
    public float lifetime = 2f;  // 투사체 생존 시간
    public float explosionRadius = 1.5f; // 폭발 반경
    public GameObject explosionPrefab; // 폭발 효과 프리팹

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
                Debug.Log("투사체가 적에게 명중!");
                Explode(); // 폭발 효과 및 범위 공격
                // enemy.TakeDamage(); // 적에게 피해 주기
            }
            Destroy(gameObject); // 적과 충돌 시 삭제
        }
        // 땅과 충돌했을 때
        else if (collision.CompareTag("Ground"))
        {
            Debug.Log("투사체가 땅에 떨어짐");
            Explode(); // 폭발 효과 및 범위 공격
            Destroy(gameObject);
        }
    }

    private void Explode()
    {
        // 폭발 효과 생성
        if (explosionPrefab != null)
        {
            GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            explosion.transform.localScale = new Vector3(3, 3, 3); // 폭발 크기 조정
            Destroy(explosion, 1.0f); // 폭발 효과 1초 후 삭제
        }

        // 폭발 범위 내 오브젝트 감지
        Collider2D[] collisions = Physics2D.OverlapCircleAll(transform.position, 1.5f); // 폭발 범위는 지름 3.0f의 원

        // Enemy 태그를 가진 오브젝트 감지
        foreach (Collider2D collision in collisions)
        {
            if (collision.CompareTag("Enemy"))
            {
                Debug.Log("폭발 범위 내 적 발견: " + collision.gameObject.name);

                knockBack(collision);
            }
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
                
                // 적에게 피해 주기
                enemy.TakeDamage(bulletDirection);
            }
    }
}
