using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveDistance = 2.0f;  // 앞뒤로 움직일 거리
    public float moveSpeed = 2.0f;      // 이동 속도
    private Vector3 startPosition;       // 적의 시작 위치
    private Vector3 targetPosition;      // 목표 위치
    private bool movingTowardsTarget = true; // 이동 방향 플래그
    private bool isMoving = true;        // 이동 중인지 여부

    private void Start()
    {
        // 시작 위치 저장
        startPosition = transform.position;
        targetPosition = startPosition + new Vector3(moveDistance, 0, 0); // 목표 위치 설정
        StartCoroutine(MoveEnemy());
    }

    private IEnumerator MoveEnemy() // enemy가 계속해서 이동
    {
        while (true) // 무한 루프
        {
            if (isMoving) // 이동 중일 때만 이동
            {
                // 이동할 방향에 따라 이동
                float step = moveSpeed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

                // 목표 위치에 도달했는지 확인
                if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
                {
                    // 방향 전환
                    movingTowardsTarget = !movingTowardsTarget;

                    // 목표 위치 변경
                    targetPosition = movingTowardsTarget ? 
                        startPosition + new Vector3(moveDistance, 0, 0) : 
                        startPosition - new Vector3(moveDistance, 0, 0);
                }
            }

            yield return null; // 다음 프레임까지 대기
        }
    }

    // 공격을 받았을 때 호출되는 메서드
    public void TakeDamage()
    {
        // 1초 동안 이동을 멈춤
        StartCoroutine(StopMovement(1f)); // 1초 정지
    }

    private IEnumerator StopMovement(float duration)
    {
        isMoving = false; // 이동 멈춤
        // 1초 대기
        yield return new WaitForSeconds(duration);
        isMoving = true; // 이동 재개
    }
}
