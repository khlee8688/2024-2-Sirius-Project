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
            TakeDamage(1);  // �浹 �� HP ����
        }
    }

    void TakeDamage(int damage)
    {
        hp -= damage;
        UpdateHPText();  // HP�� ������ ������ �ؽ�Ʈ ������Ʈ

        if (hp <= 0)
        {
            GameOver();
        }
    }

    void UpdateHPText()
    {
        hpText.text = "HP: " + hp;  // HP �ؽ�Ʈ ������Ʈ
    }

    void GameOver()
    {
        // ���� ���� �ؽ�Ʈ�� Ȱ��ȭ
        hpText.gameObject.SetActive(false);  // HP �ؽ�Ʈ ��Ȱ��ȭ
        gameOverText.gameObject.SetActive(true);  // ���� ���� �ؽ�Ʈ Ȱ��ȭ
        gameOverText.text = "���� ����";

        RemoveAllObjectsExceptGameOver();

        Time.timeScale = 0;  // ���� ����
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