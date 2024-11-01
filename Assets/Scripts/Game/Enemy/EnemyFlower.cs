using System.Collections;
using UnityEngine;

public class EnemyFlower : MonoBehaviour
{
    [SerializeField] float attackRate =2;
    float           currentTime =0;
    Animator        animator;

    void Awake(){
        animator = GetComponentInChildren<Animator>();
    }

    IEnumerator Start(){
        while(true){
            if( Time.time - currentTime > attackRate){
                animator.SetTrigger("onFire");

                currentTime = Time.time;
            }

            yield return null;
        }
    }
}
