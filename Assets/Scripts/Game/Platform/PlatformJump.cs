using UnityEngine;

public class PlatformJump : PlatformBase
{
    [SerializeField] float jumpForce =22;
    [SerializeField] float resetTime = 0.5f; //다시 점프가 가능하게 하는 시간

    Animator animator; // 발판 에니메이션
    GameObject other; // player로 해도 되는데, 만약 여러 player가 혹시라도 있다면 
    //심지어 물체가 여기에 닿아도 점프되게 한다.

    void Awake(){
        animator = GetComponent<Animator>();
    }

    public override void UpdateCollision(GameObject other)
    {
        if( IsHit ) return;

        IsHit = true;
        this.other = other;  //  맵버변수랑 인자변수가 같아서 이렇게 함.
        // 이것은  other 에 player를 할당하는 거지, other자체가 플레이어는 아니다.

        animator.SetTrigger("onJump"); // 발판의 에니메이션 재생을 위해
    }

    public void JumpAction(){
       other.GetComponent<MovementRigidbody2D>().JumpByPlatform(jumpForce); 
       other = null; //  점프되는 오브젝트 대상 없앰(플레이어가 없어지는 것은 아니다.)

       Invoke(nameof(Reset), resetTime);
    }

    void Reset(){
        IsHit = false; //이러면 에니메이션도 중단, 모든 것 원상태
    }
}
