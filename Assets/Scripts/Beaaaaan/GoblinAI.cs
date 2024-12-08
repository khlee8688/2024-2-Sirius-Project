using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinAI : MonoBehaviour
{
    public float detectionRange = 5f;  // 감지 범위
    public float moveSpeed = 3f;       // 몬스터 이동 속도
    private Transform target;          // 추적할 타겟

    private Rigidbody2D rb;
    private Vector2 movement;

    private SpriteRenderer spriteRenderer; // 스프라이트 방향 제어용

    private float groundY;             // 몬스터가 땅에 붙어있을 Y축 위치
    private bool isGrounded = false;   // 땅에 닿았는지 여부

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  // Rigidbody2D 초기화
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        DetectPlayer(); // 플레이어 감지

        if (target != null)
        {
            // 타겟 방향 계산 (Y축은 무시)
            Vector2 direction = (target.position - transform.position).normalized;
            movement = new Vector2(direction.x, 0); // X축 이동만 허용, Y축 제거

             // 스프라이트 방향 설정
            FlipSprite(direction.x);
        }
        else
        {
            movement = Vector2.zero; // 타겟이 없으면 정지
        }
    }

    void FixedUpdate()
    {
        // 몬스터 이동
        MoveEnemy();
    }

    void DetectPlayer()
    {
        // 주변의 모든 Collider2D를 감지
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, detectionRange);

        foreach (Collider2D collider in hitColliders)
        {
            if (collider.CompareTag("Player"))
            {
                target = collider.transform; // 타겟 설정
                return;
            }
        }

        target = null; // 타겟이 없으면 초기화
    }

    void MoveEnemy()
    {
        if (movement != Vector2.zero && isGrounded)
        {
            // Y축은 항상 고정된 값을 유지 (groundY)
            rb.MovePosition(new Vector2(rb.position.x + movement.x * moveSpeed * Time.fixedDeltaTime, groundY));
        }
    }

    void FlipSprite(float directionX)
    {
        if (directionX > 0)
        {
            // 오른쪽으로 이동 중
            spriteRenderer.flipX = false;
        }
        else if (directionX < 0)
        {
            // 왼쪽으로 이동 중
            spriteRenderer.flipX = true;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // 땅에 닿았을 때 groundY 설정 (Layer 또는 Tag로 땅을 구분)
        if (collision.gameObject.CompareTag("Ground") && !isGrounded)
        {
            groundY = transform.position.y; // 땅의 Y축 위치 저장
            isGrounded = true;
            Debug.Log("Ground detected! Y position: " + groundY);
        }
    }

    // 감지 범위 시각화 (디버그용)
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}



