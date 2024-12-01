using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public int hp = 3;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI gameOverText;
    private Camera mainCamera;

    private void Start()
    {
        UpdateHPText();
        gameOverText.gameObject.SetActive(false);
        mainCamera = Camera.main;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            TakeDamage(1);  // 충돌 시 HP 감소
        }
    }

    void TakeDamage(int damage)
    {
        hp -= damage;
        UpdateHPText();  // HP가 감소할 때마다 텍스트 업데이트

        if (hp <= 0)
        {
            GameOver();
        }
    }

    void UpdateHPText()
    {
        hpText.text = "HP: " + hp;  // HP 텍스트 업데이트
    }

    void GameOver()
    {
        // 게임 오버 텍스트만 활성화
        hpText.gameObject.SetActive(false);  // HP 텍스트 비활성화
        gameOverText.gameObject.SetActive(true);  // 게임 오버 텍스트 활성화
        gameOverText.text = "게임 오버";

        RemoveAllObjectsExceptGameOver();

        Time.timeScale = 0;  // 게임 정지
    }

    void RemoveAllObjectsExceptGameOver()
    {
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            if (obj != gameOverText.gameObject &&
                obj != gameOverText.transform.parent.gameObject &&
                obj != mainCamera.gameObject)
            {
                Destroy(obj);
            }
        }
    }
}