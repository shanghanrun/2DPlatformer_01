using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    MovementRigidbody2D movement;
    float               originSpeed;
    float               some = 0.1f; // 가벼운 바닥충돌시 발생할 수 있는 감소 값

    public void Setup(int direction){  // direction은 x방향이다.
        movement = GetComponent<MovementRigidbody2D>(); //PlayerProjectile오브젝트에 Movement2D스크립트있다.  이 스크립트도  PlayerProjectile오브젝트에 어태치된다. 그러므로 동일위치
        movement.MoveTo(direction);

        originSpeed = Mathf.Abs(movement.Velocity.x);
    }

    void Update(){
        if ( movement.IsGrounded ) movement.Jump();

        if( Mathf.Abs(movement.Velocity.x) < (originSpeed - some)){
            Destroy(gameObject);
        }
    }
}
