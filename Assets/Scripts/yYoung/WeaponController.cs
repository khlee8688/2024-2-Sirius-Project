using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour   // WeaponController이라는 Class를 선언, MonoBehavior를 상속받아 유니티 컴포넌트로 사용
{
    public Transform weaponTransform;        // 무기의 Transform
    public float swingDistance = 1.0f;       // 무기가 움직일 거리
    public float swingSpeed = 5.0f;          // 무기가 움직이는 속도

    private Vector3 initialPosition;         // 무기의 초기 위치
    private bool isSwinging = false;         // 현재 스윙 중인지 여부
    public bool isAttacking = false;         // 공격 상태 여부
    private Collider2D currentEnemy;         // 현재 충돌 중인 적의 Collider2D
    
    void Start()
    {
        // 무기의 초기 위치 저장, 공격이 끝난 후, 다시 이 위치로 돌아옴
        initialPosition = weaponTransform.localPosition;
    }

    void Update()
    {
        // X 키를 누르면 스윙 시작 (한 번만)
        // x키를 누르고 지금 스윙 중이 아니라면 스윙 모션 시작!
        if (Input.GetKeyDown(KeyCode.X) && !isSwinging)
        {
            StartCoroutine(SwingWeapon());  // 스윙 모션 실행
        }

        if (isAttacking && currentEnemy != null)
        {
            AttackEnemy(currentEnemy);
            isAttacking = false;  // 한 번 공격 후 공격 상태 종료
        }
    }

    private IEnumerator SwingWeapon()
    {
        // Debug.Log("공격!");  // 디버깅 용
        isSwinging = true;  // 스윙 시작
        isAttacking = true; // 공격 상태 시작

        // 무기를 앞으로 움직임
        float elapsedTime = 0f; // 스윙 애니메이션 진행 시간을 나타냄
        // 무기를 스윙 거리 (swingDistance)만큼 오른 쪽으로 이동시킴
        Vector3 targetPosition = initialPosition + new Vector3(swingDistance, 0, 0);

        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime * swingSpeed;
            /*
            Vector3.Lerp(startPosition, endPosition, t);
            t 값이 증가함에 따라 객체가 startPosition에서 endPosition으로 이동
            */
            // initialPosition에서 targetPosition으로 elapsedTime동안 이동
            weaponTransform.localPosition = Vector3.Lerp(initialPosition, targetPosition, elapsedTime);
            yield return null;  // 다음 프레임까지 대기
        }

        // 무기를 원래 위치로 복귀
        elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime * swingSpeed;
            // 타겟에서 기존위치로 이동
            weaponTransform.localPosition = Vector3.Lerp(targetPosition, initialPosition, elapsedTime);
            yield return null;
        }

        isSwinging = false;  // 스윙 종료
        isAttacking = false; // 공격 상태 종료
    }

    // 충돌 감지
    /*
    private void OnTriggerEnter2D(Collider2D collision) // 충돌이 발생했을 때, 실행
    {
        // Debug.Log("충돌 감지됨: " + collision.gameObject.name); // 충돌한 오브젝트의 이름 로그 출력
        // 공격 중이고 Enemy태그를 가지고 있는지 확인
        if (collision.CompareTag("Enemy") && isAttacking)
        {
            // 충돌한 객체에 EnemyController 스크립트가 있는지 확인
            // 해당 스크립트가 없다면 enemy에는 null이 반환됨
            EnemyController enemy = collision.GetComponent<EnemyController>();
            if (enemy != null)  // 해당 스크립트가 있어서 null이 반환되지 않은 경우
            {
                Debug.Log("적에게 공격함: " + enemy.name); // 적에게 공격할 때 로그 출력
                enemy.TakeDamage(); // 적이 1초동안 정지
            }
        }
    }
    */
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("범위 안으로 적이 들어옴");
            currentEnemy = collision;  // 적이 범위에 들어온 경우 저장
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && collision == currentEnemy)
        {
            Debug.Log("범위 밖으로 적이 나감");
            currentEnemy = null;  // 적이 범위에서 나가면 초기화
        }
    }

    private void AttackEnemy(Collider2D enemyCollider)
    {
        EnemyController enemy = enemyCollider.GetComponent<EnemyController>();
        if (enemy != null)
        {
            Debug.Log("적에게 공격 성공: " + enemy.name);
            enemy.TakeDamage();  // 적에게 데미지 적용
            //currentEnemy = null; // 한 번 공격 후 초기화

            // 1초 후에도 적이 여전히 범위 내에 있으면 currentEnemy를 다시 할당
            StartCoroutine(CheckEnemyStillInRange(enemyCollider));
        }
    }

    private IEnumerator CheckEnemyStillInRange(Collider2D enemyCollider)
    {
        yield return new WaitForSeconds(1f);  // 1초 대기

        // 여전히 적이 범위 내에 있으면 currentEnemy로 다시 할당
        if (enemyCollider != null && enemyCollider == currentEnemy)
        {
            currentEnemy = enemyCollider;
        }
    }
}
