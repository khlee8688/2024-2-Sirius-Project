using System.Collections;
using UnityEngine;

public class ProjectileThrower : MonoBehaviour
{
    public GameObject projectilePrefab_Parabola; // 포물선 투사체 프리팹
    public GameObject projectilePrefab_Straight; // 직선 투사체 프리팹
    public GameObject projectiliProfab_Attach;  // 점착 폭탄 투사체 프리팹
    public Transform spawnPoint;                 // 투사체 생성 위치

    public float straightSpeed = 20f;   // 직선 운동 속도
    public float parabolaSpeedX = 10f;  // 포물선 X축 속도
    public float parabolaSpeedY = 10f;  // 포물선 Y축 속도

    private bool parabolaMode = true;   // 포물선 모드 활성화 상태
    private bool straightMode = false;
    private bool attachMode = false;

    void Update()
    {
        // 모드 전환
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            parabolaMode = true;
            straightMode = false;
            attachMode = false;
            Debug.Log("포물선 모드 활성화");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            parabolaMode = false;
            straightMode = true;
            attachMode = false;
            Debug.Log("직선 모드 활성화");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            parabolaMode = false;
            straightMode = false;
            attachMode = true;
            Debug.Log("점착 모드 활성화");
        }

        // C 키로 투사체 발사
        if (Input.GetKeyDown(KeyCode.C))
        {
            LaunchProjectile();
        }
    }

    void LaunchProjectile()
    {
        if (parabolaMode && !straightMode && !attachMode)
        {
            // 포물선 투사체 생성
            GameObject projectile = Instantiate(projectilePrefab_Parabola, spawnPoint.position, spawnPoint.rotation);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = new Vector3(parabolaSpeedX, parabolaSpeedY, 0);
            }
        }
        else if(!parabolaMode && straightMode && !attachMode)
        {
            // 일직선 투사체 생성
            GameObject projectile = Instantiate(projectilePrefab_Straight, spawnPoint.position, spawnPoint.rotation);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // 2D 게임에서는 right 방향으로 발사
                rb.velocity = spawnPoint.right * straightSpeed;
                Debug.Log("발사 방향: " + spawnPoint.right + ", 속도: " + straightSpeed);
            }
        }
        else if(!parabolaMode && !straightMode && attachMode)
        {
            // 점착 투사체 생성
            GameObject projectile = Instantiate(projectiliProfab_Attach, spawnPoint.position, spawnPoint.rotation);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = new Vector3(parabolaSpeedX, parabolaSpeedY, 0);
            }
        }
        else{
            Debug.Log("투사체 결정 오류");
        }
    }
}
