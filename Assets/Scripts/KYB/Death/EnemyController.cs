using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform player;
    public float speed = 2f;
    public float retreatDistance = 2f;
    public float retreatTime = 1f;

    private bool isRetreating = false;
    private float retreatTimer = 0f;

    private void Update()
    {
        if (player != null)
        {
            if (!isRetreating)
            {
                Vector3 direction = (player.position - transform.position).normalized;
                transform.position += direction * speed * Time.deltaTime;
            }
            else
            {
                retreatTimer += Time.deltaTime;
                if (retreatTimer >= retreatTime)
                {
                    isRetreating = false;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartRetreat();
        }
    }

    private void StartRetreat()
    {
        Vector3 retreatDirection = (transform.position - player.position).normalized;
        transform.position += retreatDirection * retreatDistance;

        isRetreating = true;
        retreatTimer = 0f;
    }
}
