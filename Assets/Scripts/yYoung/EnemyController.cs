using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveDistance = 2.0f;  // 앞뒤로 움직일 거리
    public float moveSpeed = 2.0f;      // 이동 속도
    private Vector3 startPosition;       // 적의 시작 위치
    private Vector3 targetPosition;      // 목표 위치
    private bool movingTowardsTarget = true; // 이동 방향 플래그
    private bool isMoving = true;        // 이동 중인지 여부

    public float knockbackForce = 10f;    // 넉백 힘
    public float knockbackDuration = 0f; // 넉백 지속 시간
    private bool isKnockedBack = false;  // 현재 넉백 상태 여부
    private Rigidbody2D rb;             // Rigidbody2D 참조

    private void Start()
    {
        // 시작 위치 저장
        startPosition = transform.position;
        targetPosition = startPosition + new Vector3(moveDistance, 0, 0); // 목표 위치 설정
        // Rigidbody2D 컴포넌트 가져오기
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D가 없습니다. EnemyController를 사용하려면 Rigidbody2D를 추가하세요.");
        }
        StartCoroutine(MoveEnemy());
    }

    private IEnumerator MoveEnemy()
    {
        while (true)
        {
            if (isMoving && !isKnockedBack) // 이동 중일 때만 이동
            {
                float step = moveSpeed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

                if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
                {
                    movingTowardsTarget = !movingTowardsTarget;
                    targetPosition = movingTowardsTarget
                        ? startPosition + new Vector3(moveDistance, 0, 0)
                        : startPosition - new Vector3(moveDistance, 0, 0);
                }
            }
            yield return null; // 다음 프레임까지 대기
        }
    }

    // 공격을 받았을 때 호출되는 메서드 (넉백 포함)
    public void TakeDamage(Vector2 bulletDirection)
    {
        Vector2 direction;
        if (!isKnockedBack)
        {
            if (bulletDirection.x > 0)
            {
                direction = new Vector2(-5,0);
                Debug.Log("x값은 양수입니다.");
                StartCoroutine(ApplyKnockback(direction));
            }
            else if (bulletDirection.x < 0)
            {
                direction = new Vector2(5,0);
                Debug.Log("x값은 음수입니다.");
                StartCoroutine(ApplyKnockback(direction));
            }
            else
            {
                direction = new Vector2(0,5);
                Debug.Log("x값은 0입니다.");
                StartCoroutine(ApplyKnockback(direction));
            }   
            
        }
    }

    private IEnumerator ApplyKnockback(Vector2 direction)
    {
        isKnockedBack = true; // 넉백 상태 활성화
        //Debug.Log($"넉백 방향2: {direction}");

        // 현재 속도를 초기화하고 
        rb.velocity = Vector2.zero;

        // 넉백 방향으로 힘을 가함
        rb.AddForce(direction.normalized * knockbackForce, ForceMode2D.Impulse);
        // 넉백 지속 시간 동안 대기
        yield return new WaitForSeconds(knockbackDuration);

        isKnockedBack = false; // 넉백 상태 해제

        rb.velocity = Vector2.zero; // 속도 초기화
    }
}
