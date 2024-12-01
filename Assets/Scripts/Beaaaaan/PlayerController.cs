using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRigidbody;
    private BoxCollider2D playerCollider;
    private RaycastHit2D hit;
    private SpriteRenderer spriteRenderer; // 스프라이트 방향 제어용
    public float jumpForce = 1000f;
    public float speed = 8f;
    public float stamina = 20f; //스테미너
    public float staminaDrainRate = 10f; //스테미너 소모 비율
    public float staminaRecoveryRate = 5f; //스테미너 회복 비율
    private float sprintCoolTime = 0f;
    private bool isCoolTime = false;
    private float xInput, xSpeed;

    private int jumpCount = 0; // 누적 점프 횟수
    private bool isGrounded = false;
    private bool isCrouching = false;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        xInput = Input.GetAxis("Horizontal");

        // 스프라이트 방향 전환
        if (xInput > 0)
        {
            spriteRenderer.flipX = false; // 오른쪽
        }
        else if (xInput < 0)
        {
            spriteRenderer.flipX = true; // 왼쪽
        }

        if(Input.GetKey(KeyCode.DownArrow)){
            isCrouching = true;
            transform.localScale = new Vector3(8, 4, 8); //스케일 줄이기
        }
        else{
            isCrouching = false;
            transform.localScale = new Vector3(8, 8, 8); //스케일 원래대로
        }
        
        if(Input.GetKey(KeyCode.LeftShift) && stamina > 0 && !isCoolTime){
            xSpeed = xInput * speed * 2;
            stamina -= staminaDrainRate * Time.deltaTime;
            GameManager.instance.showStamina((int)stamina);
            if(stamina <= 0){
                stamina = 0; //최소 스테미너 제한
                isCoolTime = true;
            } 

        }
        else{
            xSpeed = xInput * speed;
            stamina += staminaRecoveryRate * Time.deltaTime;
            GameManager.instance.showStamina((int)stamina);
            if(stamina > 20){
                stamina = 20; //최대 스테미너 제한
                isCoolTime = false;
            }
        }

        playerRigidbody.velocity = new Vector2(xSpeed, playerRigidbody.velocity.y);

        if(Input.GetKeyDown(KeyCode.Space) && isGrounded && jumpCount < 1){
        jumpCount++;
        playerRigidbody.velocity = Vector2.zero;
        playerRigidbody.AddForce(new Vector2(0, jumpForce));
       }

       if(Input.GetKeyUp(KeyCode.Space) && playerRigidbody.velocity.y > 0){
        playerRigidbody.velocity = playerRigidbody.velocity * 0.5f;
       }

       hit = Physics2D.Raycast(transform.position, Vector2.down, 2.5f, LayerMask.GetMask("Ground"));
       isGrounded = hit.collider != null;

       if(isGrounded){
        jumpCount = 0;
       }
    }

    void OnDrawGizmos()
    {
        Vector2 rayStart = transform.position; // Ray 시작 위치
        Vector2 rayDirection = Vector2.down;   // Ray 방향
        float rayLength = 2.5f;                // Ray 길이

        Gizmos.color = Color.green;            // Gizmos 색상
        Gizmos.DrawLine(rayStart, rayStart + rayDirection * rayLength); // Ray 선 그리기
    }

}

