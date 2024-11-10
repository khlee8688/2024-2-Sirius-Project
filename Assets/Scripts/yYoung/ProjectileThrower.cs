using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileThrower : MonoBehaviour
{
    public GameObject projectilePrefab; // 발사할 투사체 프리팹
    public Transform spawnPoint;        // 투사체가 생성될 위치
    public float launchForce = 500f;    // 투사체의 발사 힘
    public float cooldownTime = 2f;     // 3번 연속 발사 후 쿨다운 시간
    private int shotCount = 0;          // 발사 횟수 추적
    private bool isCooldown = false;    // 쿨다운 상태 여부

    void Update()
    {
        // 발사 입력 및 쿨다운 상태 확인
        if (Input.GetKeyDown(KeyCode.C) && !isCooldown)
        {
            LaunchProjectile();
            shotCount++;

            // 3번 연속 발사했을 때 쿨다운 시작
            if (shotCount >= 3)
            {
                StartCoroutine(StartCooldown());
            }
        }
    }

    void LaunchProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, spawnPoint.position, Quaternion.identity);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
    }

    IEnumerator StartCooldown()
    {
        isCooldown = true;
        shotCount = 0; // 발사 횟수 초기화
        yield return new WaitForSeconds(cooldownTime);
        isCooldown = false;
    }
}
