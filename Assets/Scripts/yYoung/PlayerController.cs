using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 3f;                      // 플레이어 이동 속도
    private Rigidbody2D rb;                            // 플레이어의 Rigidbody2D 컴포넌트
    private Vector2 movement;                          // 플레이어의 이동 방향
    public ParticleSystem muzzleFlash;                 // 발사 이펙트

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();              // Rigidbody2D 컴포넌트 초기화
    }

    private void Update()
    {
        // 입력 받기
        movement.x = Input.GetAxis("Horizontal");
        
        // 좌우 이동 입력
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
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
}
