using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class MovementRigidbody2D : MonoBehaviour   // Player게임오브젝트에 어태치
{
    [Header("LayerMask")]
    [SerializeField] LayerMask groundCheckLayer; //바닥체크 위한 충돌레이어
    [SerializeField] LayerMask aboveCollisionLayer; // 머리 충돌 체크위한 레이어
    [SerializeField] LayerMask belowCollisionLayer; // 발충돌 레이어

    [Header("Move")]
    [SerializeField] float walkSpeed = 5;
    [SerializeField] float runSpeed = 8;
    [SerializeField] float jumpForce = 13;
    [SerializeField] float lowGravityScale = 1.5f;
    [SerializeField] float highGravityScale = 3f;

    float moveSpeed;

    //바닥에 착지 직전 조금 빨리 점프키를 눌렀을 경우, 바닥에 착지하면 바로 점프가 되게 하기.
    // 선입력 할 수 있는 한계시간을 나타내는 jumpBufferTime, 시간계산을 위한  jumpBufferCounter
    float jumpBufferTime = 0.2f;
    float jumpBufferCounter;

    //코요테타임
    float hangTime = 0.4f;
    float hangCounter;

    Vector2 collisionSize; // 머리, 발 위치에 생성하는 충돌 박스 크기
    Vector2 footPosition; // 발위치
    Vector2 headPosition; // 머리 위치

    Rigidbody2D rb;
    Collider2D coll;

    public bool IsLongJump { get; set; } = false;
    public bool IsGrounded { private set; get; } = false;  // 다른 곳에서는 읽기만 가능하게
    public Collider2D HitAboveObject {private set; get;} // 머리에 충돌 오브젝트 정보
    public Collider2D HitBelowObject {private set; get;} //발충돌 오브젝트정보

    public Vector2 Velocity => rb.velocity; // rb.velocity 값을 반환하는 람다식
    // 이것은 rb.velocity값을 반환받는 중계자 역할을 하는 읽기전용속성이다.
    // Vector2 currentSpeed = Velocity; 라고 하면, rb.velocity값을 반환해 준다.
    // 즉 Vector2 Velocity = rb.velocity; 하면 그 당시의 값을 저장하게 되고, 나중에 그 값을 할당할 때 그 값을 반환하지만,
    // 지금 처럼 하면, 항상 현재의 값을 읽어올 수 있다.

    void Awake()
    {
        moveSpeed = walkSpeed;

        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
    }

    void Update()
    {
        UpdateCollision();
        JumpHeight();
        JumpAdditive();
    }

    // x축, 속력(velocity) 설정: 외부클래스에서 호출
    public void MoveTo(float x)  //x 방향이동
    {
        // x의 절대값이 0.5이면 걷기(walkSpeed), 1이면 뛰기(runSpeed)
        moveSpeed = Mathf.Abs(x) != 1 ? walkSpeed : runSpeed;

        // x가 -0.5, 0.5값을 가질 때 x를 -1, 1로 변경
        if (x != 0) x = Mathf.Sign(x); // 부호변환 (양수:1, 음수:-1, 0: 0 반환)

        // x축 방향 속력을 x * moveSpeed로 설정
        rb.velocity = new Vector2(x * moveSpeed, rb.velocity.y);
    }

    void UpdateCollision()
    {
        // 플레이어 오브젝트의 콜라이더 min, center, max 위치 정보
        Bounds bounds = coll.bounds;

        //플레이어의 발에 생성하는 충돌 범위
        collisionSize = new Vector2((bounds.max.x - bounds.min.x) * 0.5f, 0.05f);
        // 즉, 콜라이더 좌우의 절반크기, 높이는 0.1

        // 플레이어의 머리, 발 위치
        headPosition = new Vector2(bounds.center.x, bounds.max.y);
        footPosition = new Vector2(bounds.center.x, bounds.min.y);

        // 플레이어가 바닥을 밟고 있는지 체크하는 충돌 박스
        IsGrounded = Physics2D.OverlapBox(footPosition, collisionSize, 0, groundCheckLayer);
        // Physics2D.OverlapBox()는 반환받는 변수의 타입에 따라 bool, Collider2D 등을 반환한다.

        //플레이어의 머리/발 에 충돌한 오브젝트 정보를 저장하는 충돌 박스
        HitAboveObject = Physics2D.OverlapBox(headPosition, collisionSize, 0, aboveCollisionLayer);
        HitBelowObject = Physics2D.OverlapBox(footPosition,collisionSize,0,belowCollisionLayer);
    }



    void OnDrawGizmos() // 머리쪽 콜라이더 보이게 하기
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(headPosition, collisionSize);

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(footPosition, collisionSize);
    }

    // 다른 클래스에서 호출하는 점프메소드 y축점프
    public void Jump()
    {
        jumpBufferCounter = jumpBufferTime;
        // jumpBufferCounter는 점프키(c)가 눌리면 무조건 초기값으로 세팅됨.

        // if (IsGrounded)  땅이 아니어도 점프가 가능해야 된다.
        // {
        //     rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        // }
    }
    public void JumpByPlatform(float force){
        rb.velocity = new Vector2(rb.velocity.x, force);
    }

    void JumpHeight()
    {
        //낮은 점프, 높은 점프 구현을 위한 중력계수(gravityScale) 조절 (Jump Up일때만 적용된다)
        //중력계수가 낮은 if문은 높은 점프가 되고, 중력계수가 높은 else문은 낮은 점프가 된다.
        if (IsLongJump && rb.velocity.y > 0)
        { // 점프키를 계속 누르고 있고,y속력이 0보다 클 경우(점프중)
            rb.gravityScale = lowGravityScale;

        }
        else
        {
            rb.gravityScale = highGravityScale;
        }
    }
    void JumpAdditive(){
        // Coyote Time
        if(IsGrounded) hangCounter = hangTime; 
        //이것은 플레이어가 땅에 있는 한 계속 유지되니, hangCounter >0 조건은 IsGrounded를 대신할 수도 있다.(아래에서 jumpBuffer 가 작동하려면, 적어도 바닥이어야 점프가 되도록 해야 된다.)

        else            hangCounter -= Time.deltaTime;


        //바닥 착지 직전 조금 빨리 점프키를 눌렀을 때, 바닥에 착지하면 바로 점프하도록 설정
        if(jumpBufferCounter >0) jumpBufferCounter -=Time.deltaTime;
        //시간을 경과시켜야 되니, 일단 빼는 것 먼저하고 아래 로직수행

        if(jumpBufferCounter >0 && hangCounter >0){ //  시간이 지나도 jumpBufferCounter가 여유가 있으면, 바닥에 닿으면 점프


            //점프 힘만큼 y축 방향 속력으로 설정
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpBufferCounter =0;  // 점프후에는 없앤다.
                hangCounter =0;
            
        }

        //즉, 공중에 떠 있을 때, 점프버튼을 누르더라도, 만약 0.1초 동안에 바닥에 착지되었다면 
        // 점프키를 다시 누르지 않아도 자동으로 점프가 된다.
    }

    // public void ResetVelocityY(){
    //     rb.velocity = new Vector2(rb.velocity.x, 0);
    // } 불필요하다.
}

