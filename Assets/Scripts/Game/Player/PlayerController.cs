using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameController gameController;
    [SerializeField] KeyCode jumpKeyCode = KeyCode.C;
    [SerializeField] KeyCode fireKeyCode = KeyCode.Z;

    StageData stageData;
    MovementRigidbody2D movement;
    PlayerAnimator playerAnimator;
    PlayerWeapon weapon;
    PlayerData   playerData;
    int          lastDirectionX =1;  // 발사체 방향설정을 위해

    public void Setup(StageData stageData){
        this.stageData = stageData;
        transform.position = stageData.PlayerPosition;
    }

    void Awake(){
        movement = GetComponent<MovementRigidbody2D>();
        playerAnimator = GetComponentInChildren<PlayerAnimator>();
        weapon = GetComponent<PlayerWeapon>();
        playerData = GetComponent<PlayerData>();
    }

    void Update(){
        // 키입력: 좌우 방향키, x 키
        float x     = Input.GetAxisRaw("Horizontal");
        float offset = 0.5f + Input.GetAxisRaw("Sprint") * 0.5f;

        if (x != 0) lastDirectionX = (int)x;  // 좌우 방향키가 입력되면,  lastDirectionX 변수에 정수형태로 저장

        // 걷기일 땐 값의 볌위가 -0.5~0.5
        // 뛰기일 땐 값의 범위가 -1 ~ 1로 설정
        x *= offset;

        // 플레이어의 이동 제어(좌우)
        UpdateMove(x);
        //  플레이어의 점프제어
        UpdateJump();

        //에니메이션과 방향 바꾸기(움직임과 거의 동시에 일어나니 여기에 두어도 된다.)
        playerAnimator.UpdateAnimation(x);

        //머리/발에 충돌한 사건 처리
        UpdateAboveCollision();
        UpdateBelowCollision();

        //원거리 공격제어
        UpdateRangeAttack();
        // 플레이어가 낭떠러지로 추락했는지 검사
        IsUnderGround();
    }

    void UpdateMove(float x){
        //플레이어의 물리적 이동(좌/우)
        movement.MoveTo(x);

        //플레이어의 x축 이동 한계치 설정(PlayerLimitMinX ~ PlayerLimitMaxX)
        float xPosition = Mathf.Clamp(transform.position.x, stageData.PlayerLimitMinX, stageData.PlayerLimitMaxX);

        transform.position = new Vector2(xPosition, transform.position.y);

        
    }
    void UpdateJump(){
        if(Input.GetKeyDown(jumpKeyCode)){
            movement.Jump();
        }
        if(Input.GetKey(jumpKeyCode)){
            movement.IsLongJump = true;
        } else if(Input.GetKeyUp(jumpKeyCode)){
            movement.IsLongJump = false;
        }
    }

    void UpdateAboveCollision(){
        if (movement.HitAboveObject !=null){    
            TileBase tile = movement.HitAboveObject.GetComponent<TileBase>();

            if(tile !=null){
                if(!tile.IsHit){  // 이미 맞은 상태에서는 업데이트 안함.신규 충돌 취급
                    tile.UpdateCollision();
                }
            }
        }
        // 아래는 같은 의미의 코드이다.
        // if( movement.HitAboveObject.TryGetComponent<TileBase>(out var tile)){
        //     if(!tile.IsHit){
        //         tile.UpdateCollision();
        //     }
        // }  
    }
    void UpdateBelowCollision(){
        if( movement.HitBelowObject !=null){
            // Platform_03_OneWay
            if( Input.GetKey(KeyCode.DownArrow) && movement.HitBelowObject.TryGetComponent<PlatformEffectorExtension>(out var platform2)){
                    platform2.DownPlatform();
            }

            if( movement.HitBelowObject.TryGetComponent<PlatformBase>(out var platform)){
                if(!platform.IsHit){
                    platform.UpdateCollision(gameObject); // gameObject는 플레이어
                }
            }
        }
    }

    void UpdateRangeAttack(){
        if ( Input.GetKeyDown(fireKeyCode) && playerData.CurrentProjectile > 0){
            playerData.CurrentProjectile--;
            weapon.StartFire(lastDirectionX);
        }
    }

    void IsUnderGround(){
        if (transform.position.y < stageData.MapLimitMinY){
            // OnDie();
            gameController.LevelFailed();
        }
    }
    // public void OnDie(){
    //     gameController.LevelFailed();
    // }

    public void LevelComplete(){
        gameController.LevelComplete();
    }
}
