using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 3f;                      // 플레이어 이동 속도
    private Rigidbody2D rb;                            // 플레이어의 Rigidbody2D 컴포넌트
    private Vector2 movement;                          // 플레이어의 이동 방향

    public GameObject projectilePrefab;                // 투사체 프리팹
    public Transform launchPoint;                      // 투사체를 발사할 위치
    public ParticleSystem muzzleFlash;                 // 발사 이펙트

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();              // Rigidbody2D 컴포넌트 초기화
    }

    private void Update()
    {
        // 입력 받기
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        // C 키를 눌렀을 때 투사체 발사
        if (Input.GetKeyDown(KeyCode.C))
        {
            FireProjectile();
        }
    }

    private void FixedUpdate()
    {
        // 이동
        MovePlayer();
    }

    private void MovePlayer()
    {
        // Rigidbody2D를 사용하여 플레이어 이동
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    private void FireProjectile()
    {
        // 투사체를 발사
        GameObject projectile = Instantiate(projectilePrefab, launchPoint.position, Quaternion.identity);
        
        // 필요에 따라 각도나 회전 설정 가능
        if (movement != Vector2.zero) // 움직임이 있을 때만 회전 설정
        {
            float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg; // 각도 계산
            projectile.transform.rotation = Quaternion.Euler(0, 0, angle); // 각도에 따라 회전
        }

        /* 나중에 추가해줄 수 있음
            // 발사 이펙트 재생
            if (muzzleFlash != null)
            {
                muzzleFlash.Play();
            }
        */
        
    }
}
