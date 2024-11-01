using UnityEngine;

public class EnemyMushroom : EnemyBase
{
    FollowPath followPath;
    SpriteRenderer spriteRenderer;
    Animator animator;

    void Awake(){
        followPath = GetComponent<FollowPath>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator =  GetComponentInChildren<Animator>(); //자식인 Renderer안에 있다.
    }

    void Update(){
        spriteRenderer.flipX = followPath.Direction ==1 ? true : false;
        animator.SetFloat("moveSpeed", (int)followPath.State);
    }

    public override void OnDie(){
        if( IsDie ) return;

        IsDie = true;

        followPath.Stop();
        animator.SetTrigger("onDie");
    }
}
