using UnityEngine;

public class MushroomAI : MonoBehaviour
{
    public GameObject projectilePrefab;  // 투사체 프리팹
    public Transform firePoint;         // 투사체 발사 위치
    public float detectionRange = 10f;  // 감지 거리
    public float fireRate = 2f;         // 발사 주기
    public float projectileSpeed = 5f; // 투사체 속도 (2D)

    private GameObject player;          // 플레이어 오브젝트
    private float lastFireTime;         // 마지막 발사 시간

    void Start()
    {
        // 플레이어 찾기
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (player != null && IsPlayerInRange())
        {
            // 발사 주기 체크
            if (Time.time - lastFireTime > fireRate)
            {
                FireProjectile();
                lastFireTime = Time.time;
            }
        }
    }

    // 플레이어가 범위 안에 있는지 확인
    private bool IsPlayerInRange()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);
        return distance <= detectionRange;
    }

    // 투사체 발사
    private void FireProjectile()
    {
        if (projectilePrefab != null && firePoint != null)
        {
            // 투사체 생성
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

            // 투사체 이동 (Rigidbody 2D 사용)
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // 발사 방향: 오른쪽으로 발사 (firePoint의 방향)
                rb.velocity = firePoint.right * projectileSpeed;
            }
        }
    }
}

