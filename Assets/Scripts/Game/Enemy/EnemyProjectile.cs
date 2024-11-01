using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Player")){
            other.GetComponent<PlayerHP>().DecreaseHP();
            Destroy(gameObject); //플레이엉에 부딪쳐도 자기 자신 소멸
        }
        if (other.CompareTag("Ground")){
            Destroy(gameObject); // 땅에 부딪치면 소멸
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
