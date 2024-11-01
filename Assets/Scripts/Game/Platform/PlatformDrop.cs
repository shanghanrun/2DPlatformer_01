using System;
using System.Collections;
using UnityEngine;

public enum RespawnType{
    AfterTime =0, PlayerDead
}

public class PlatformDrop : PlatformBase
{
    [SerializeField] RespawnType respawnType = RespawnType.AfterTime;
    [SerializeField] float respawnTime =2;

    BoxCollider2D boxCollider;
    Rigidbody2D   rb;
    Vector3       originalPosition;

    void Awake(){
        boxCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        originalPosition = transform.position;
    }

    public override void UpdateCollision(GameObject other)
    {
        if( IsHit ) return;
        IsHit = true;
        StartCoroutine(nameof(Process));
    }

    IEnumerator Process(){
        //플레이어와 발판이 충돌했을 때 발판이 흔들리는 에니메이션 재생
        yield return StartCoroutine(nameof(ShakePlatform));

        // 발판이 아래로 추락
        DropPlatform();

        // 재생성이 가능한 발판이면 respawnTime 시간 이후에 재생성
        if ( respawnType == RespawnType.AfterTime){
            StartCoroutine(nameof(RespawnPlatform));
        }
        else{
            Destroy(gameObject, respawnTime); // 파괴전 잠시 시간여유는  respawnTime으로
        }
    }
    IEnumerator ShakePlatform(){
        //발판의 z축을 -5 ~5 각도로 1.5초동안 회전해서 발판 흔들리는 것처럼
        // x축은 상하반전, y축은 좌우반전,  z축은 회전
        float percent =0;
        float shakeAngle =5;
        float shakeSpeed = 10;
        float shakeTime = 1.5f;

        while(percent <1){
            percent += Time.deltaTime / shakeTime; // 이렇게 하면 shakeTime시간동안 0~1로 증가
            float z = Mathf.Lerp(-shakeAngle, shakeAngle, Mathf.PingPong(Time.time *shakeSpeed,1));
            transform.rotation = Quaternion.Euler(0,0,z);

            yield return null;
        }

        transform.rotation = Quaternion.identity; // 기본각도

    }
    void DropPlatform(){
        boxCollider.enabled = false;
        rb.isKinematic = false;  // 물리적으로 만들어서 추락하도록
    }
    IEnumerator RespawnPlatform(){
        yield return new WaitForSeconds(respawnTime); // 잠시 대기
        IsHit = false;
        transform.position = originalPosition;
        boxCollider.enabled = true;
        rb.isKinematic = true;
        rb.velocity = Vector2.zero; // 사실상 불필요. kinematic은 중력,충돌 영향 안받음.
    }
}
