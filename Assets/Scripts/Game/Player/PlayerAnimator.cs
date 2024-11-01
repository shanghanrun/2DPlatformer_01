using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Player 아래의 Renderer에 Animator가 있다.
//그리고 이 스크립트는 Renderer에 어태치 될 거다.
public class PlayerAnimator : MonoBehaviour
{
    Animator animator;
    MovementRigidbody2D movement;

    void Awake(){
        animator = GetComponent<Animator>();
        movement = GetComponentInParent<MovementRigidbody2D>();
    }

    public void UpdateAnimation(float x){
        //좌우 방향키 입력이 있을 때
        if(x !=0){
            //플레이어 스프라이트 좌/우 반전
            SpriteFlipX(x);
        }
        animator.SetBool("isJump", !movement.IsGrounded);
        // 플레이어가 바닥을 밟고 있는 경우 -> isJump는 false;
        // 플레이어가 공중에 떠 있는 경우 --> isJump true;

        //바닥에 닿아 있으면
        if(movement.IsGrounded){
            //  왼쪽 방향키를 누르면 x값이 음수이므로, Mathf.Abs()로 절대값처리,
            //  velocityX가 0이면 "Idle", velocityX가 0.5면 "Walk", 1 이면 "Run" 재생
            animator.SetFloat("velocityX", Mathf.Abs(x));
        } else{
            // velocityY가 음수이면 "JumpDown", 양수이면 "JumpUp"재생
            animator.SetFloat("velocityY", movement.Velocity.y); //Velocity는 ref와 유사한 것.
        }
    }

    // SpriteRenderer 컴포넌트의 Flip 속성을 이용해 이미지를 반전했을 때는 이미지만 반전된다.
    //! 플레이어의 전방 특정 위치에 발사체를 생성하는 것과 같이
    //! 방향전환까지 더불어 필요할 때는 Transform.localScale.x를 -1, 1 과 같이 설정
    void SpriteFlipX(float x){
        transform.parent.localScale = new Vector3((x<0? -1:1), 1,1);
    }

}
