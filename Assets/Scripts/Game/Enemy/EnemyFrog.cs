using System.Collections;
using UnityEngine;

public class EnemyFrog : EnemyBase  //EnemyBase는 OnDie관련
{
    [SerializeField]  LayerMask groundLayer;
    MovementRigidbody2D movement2D;
    new Collider2D      collider2D;
    SpriteRenderer      spriteRenderer;
    Animator            animator;
    int                 direction = -1;

    void Awake(){
        movement2D      = GetComponent<MovementRigidbody2D>();
        collider2D      = GetComponent<Collider2D>();
        animator        = GetComponentInChildren<Animator>();
        spriteRenderer  = GetComponentInChildren<SpriteRenderer>();

        StartCoroutine(nameof(Idle));  // 대기시간
    }

    public override void OnDie()
    {
        if ( IsDie ) return;

        IsDie = true;
        StopAllCoroutines();

        float destroyTime = 2;
        StartCoroutine(FadeEffect.Fade(spriteRenderer, 1, 0, destroyTime));
        Destroy(gameObject, destroyTime);
    }

    IEnumerator Idle(){
        float waitTime = 2;
        float time      =0;

        while( time < waitTime){
            time += Time.deltaTime;
            yield return null;
        }

        movement2D.Jump();
        animator.SetTrigger("onJump");

        StartCoroutine(nameof(Jump));
    }

    IEnumerator Jump(){
        yield return new WaitUntil(()=> !movement2D.IsGrounded);

        while(true){
            UpdateDirection();
            movement2D.MoveTo(direction);
            animator.SetFloat("velocityY", movement2D.Velocity.y);

            if (movement2D.IsGrounded){ // 점프했다가 바닥에 착지하면
                movement2D.MoveTo(0); // 제자리에 엄추게
                animator.SetTrigger("onLanding");

                StartCoroutine(nameof(Idle));

                yield break; // 점프코루틴 종료
            }
            yield return null; // 프레임 단위로 쉬기 위해 잠시 넘겨줌
        }
    }

    // 전방에 장애물이 존재하는지 판단하는 메소드
    void UpdateDirection(){ 
        Bounds bounds = collider2D.bounds;
        Vector2 size = new Vector2(0.1f, (bounds.max.y - bounds.min.y)* 0.8f);
        // Vector2 frontPosition = new Vector2(bounds.min.x, bounds.center.y);
        // Vector2 rearPosition = new Vector2(bounds.max.x, bounds.center.y);
        Vector2 position = new Vector2(direction == -1 ? bounds.min.x :bounds.max.x, bounds.center.y);

        if (Physics2D.OverlapBox(position, size, 0, groundLayer)){
            direction *= -1;  // 방향을 반대로 만들어주고
            spriteRenderer.flipX = !spriteRenderer.flipX; // 반대로 만들어준다.
        }
    }
}
