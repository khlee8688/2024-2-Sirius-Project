using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 5f;           // 투사체의 속도
    public float arcHeight = 2f;       // 포물선의 높이
    public float lifetime = 2f;         // 투사체의 생존 시간
    private Vector3 velocity;           // 투사체의 속도 벡터
    private bool isMoving = true;       // 투사체 이동 여부

    private void Start()
    {
        // 투사체의 초기 속도 설정
        velocity = new Vector3(speed, arcHeight, 0);
        // 생존 시간 후에 자동으로 삭제
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        if (isMoving)
        {
            // 중력 효과를 적용
            velocity.y += Physics.gravity.y * Time.deltaTime; // 중력 적용
            transform.position += velocity * Time.deltaTime;   // 새로운 위치로 이동
        }
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
            // 충돌 후 투사체 비활성화
            Destroy(gameObject);
        }
        // Ground와 충돌했을 때
        else if (collision.CompareTag("Ground"))
        {
            Debug.Log("투사체가 땅에 떨어짐");
            // 충돌 후 2초 뒤에 삭제
            Destroy(gameObject);
        }
    }
}
