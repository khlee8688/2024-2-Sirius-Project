using UnityEngine;

public class FireballAI : MonoBehaviour
{
    public float lifetime = 5f;  // 투사체 생존 시간

    void Start()
    {
        // 일정 시간 후 파괴
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // 플레이어와 충돌 확인
        if (collision.CompareTag("Player"))
        {
            Debug.Log("플레이어에 맞음!");
            // TODO: 플레이어에게 데미지를 입히는 로직 추가
        }

        // 충돌 시 투사체 파괴
        Destroy(gameObject);
    }
}

