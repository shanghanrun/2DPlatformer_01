using UnityEngine;

public class EnemyCollider : MonoBehaviour
{
    EnemyBase enemyBase;

    void Awake(){
        enemyBase = GetComponentInParent<EnemyBase>();
    }

    void OnTriggerEnter2D(Collider2D other){
        if ( enemyBase.IsDie ) return;

        if (other.CompareTag("Player")){
            PlayerHP playerHP = other.GetComponent<PlayerHP>(); //other(collider)와 같은 위치PlayerHP
            PlayerData playerData = other.GetComponent<PlayerData>();
            GameObject parent = transform.parent.gameObject ;

            if(parent.CompareTag("Frog")){ //! 개구리랑 부딛치면 invincibleTime이 저절로 감소 안한다.
                Debug.Log("개구리와 충돌");
                
                playerHP.invincibleTime =0;
                playerHP.DecreaseHP();
                playerData.InvincibleTime=0;
            } else{
                playerHP.DecreaseHP();
            }
        }
        else if (other.CompareTag("PlayerProjectile")){
            enemyBase.OnDie();
            Destroy(other.gameObject); // 발사체도 제거
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        var playerHP = other.GetComponent<PlayerHP>();
        if (playerHP.currentHP == 0) return;

        if (playerHP.currentHP > 0) playerHP.StopInvincibleCoroutine();
    }
}
