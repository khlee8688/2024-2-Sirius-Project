using UnityEngine;

public class Projectile_attach : MonoBehaviour
{
    public float explosionRadius = 1.5f; // 폭발 반경
    public GameObject explosionPrefab; // 폭발 효과 프리팹

    private bool isAttached = false; // 충돌하여 붙어 있는 상태
    private Transform attachedObject; // 붙어 있는 오브젝트

    private void Update()
    {
        // 스페이스바 입력 시 폭발
        if (isAttached && Input.GetKeyDown(KeyCode.Space))
        {
            Explode();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 적과 충돌하거나 땅과 충돌했을 때
        if (collision.CompareTag("Enemy") || collision.CompareTag("Ground"))
        {
            Debug.Log($"투사체가 {collision.tag}에 충돌함");

            // 붙는 처리
            isAttached = true;
            attachedObject = collision.transform;

            // Rigidbody2D의 물리 동작 정지
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = Vector2.zero;
                rb.isKinematic = true;
            }

            // 투사체를 충돌한 오브젝트의 위치에 고정
            transform.position = collision.ClosestPoint(transform.position);
        }
    }

    private void Explode()
    {
        Debug.Log("폭발 발생!");

        // 폭발 효과 생성
        if (explosionPrefab != null)
        {
            GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            explosion.transform.localScale = new Vector3(3, 3, 3); // 폭발 크기 조정
            Destroy(explosion, 1.0f); // 폭발 효과 1초 후 삭제
        }

        // 폭발 범위 내 오브젝트 감지
        Collider2D[] collisions = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (Collider2D collision in collisions)
        {
            if (collision.CompareTag("Enemy"))
            {
                Debug.Log("폭발 범위 내 적 발견: " + collision.gameObject.name);

                // 적에게 넉백 효과
                KnockBack(collision);
            }
        }

        // 투사체 삭제
        Destroy(gameObject);
    }

    private void KnockBack(Collider2D collision)
    {
        EnemyController enemy = collision.GetComponent<EnemyController>();
        if (enemy != null)
        {
            // 충돌한 적과의 상대적 방향 계산
            // collision.transform.position 충돌한 오브젝트(적)의 위치.
            // transform.position: 현재 오브젝트(투사체)의 위치.
            Vector2 knockBackDirection = (collision.transform.position - transform.position).normalized;
            Debug.Log($"넉백 방향: {knockBackDirection}");
            enemy.TakeDamage(knockBackDirection); // 넉백 적용
        }
    }

    private void OnDrawGizmos()
    {
        // 폭발 반경 시각화
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
