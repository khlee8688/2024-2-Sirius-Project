using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp = 5;
    public float moveSpeed = 2f;
    private bool isSlowed = false;
    private bool isStunned = false;
    private Vector2 movementDirection = Vector2.right;
    public float directionChangeInterval = 3f;

    void Start()
    {
        movementDirection = Vector2.right;
        StartCoroutine(ChangeDirectionCoroutine());
    }

    void Update()
    {
        if (!isStunned)
        {
            // 적이 스턴 상태가 아니라면 움직임
            transform.Translate(movementDirection * moveSpeed * Time.deltaTime);
        }
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        Debug.Log($"Enemy HP: {hp}");
        if (hp <= 0)
        {
            Die();
        }
    }

    public void ApplySlow(float duration, float slowFactor) // 슬로우
    {
        if (!isSlowed)
        {
            StartCoroutine(SlowCoroutine(duration, slowFactor));
        }
    }

    public void ApplyStun(float duration)
    {
        if (!isStunned)
        {
            StartCoroutine(StunCoroutine(duration));
        }
    }

    private IEnumerator SlowCoroutine(float duration, float slowFactor)
    {
        isSlowed = true;
        float originalSpeed = moveSpeed;
        moveSpeed *= slowFactor;
        Debug.Log("적이 느려집니다");

        yield return new WaitForSeconds(duration);

        moveSpeed = originalSpeed;
        isSlowed = false;
        Debug.Log("적이 움직입니다");
    }

    private IEnumerator StunCoroutine(float duration) // 스턴
    {
        isStunned = true;
        Debug.Log("적이 스턴되었습니다");

        yield return new WaitForSeconds(duration);

        isStunned = false;
        Debug.Log("적이 움직입니다");
    }

    private IEnumerator ChangeDirectionCoroutine()
    {
        while (true)
        {
            movementDirection = movementDirection == Vector2.right ? Vector2.left : Vector2.right;
            Debug.Log($"Enemy direction changed: {movementDirection}");
            yield return new WaitForSeconds(directionChangeInterval);
        }
    }

    private void Die()
    {
        Debug.Log("적이 죽었습니다.");
        Destroy(gameObject);
    }
}