using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 3f;
    private Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 입력 받기
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

    }

    void FixedUpdate()
    {
        // 이동
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision){
        //Debug.Log("충돌 감지됨: " + collision.gameObject.name); // 충돌한 오브젝트의 이름 로그 출력
    }
}

