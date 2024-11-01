using System.Collections; // Color 관련
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    [SerializeField] GameController gameController;
    [SerializeField] UIPlayerData uiPlayerData;
    //무적값을 보여주기 위해 추가한 것
    PlayerData playerData;

    [SerializeField] int max =3;
    public int currentHP; // 현재 체력 정보 알기위해 public
    SpriteRenderer spriteRenderer;
    Color originColor;
    public float invincibleTime =0; //  외부에서 관찰 가능하게 public
    public bool isInvincible= false; // 외부관찰 가능하게 [이건 계속 무적되게 하는 항목. 즉, 치트키 외에는 쓸 일이 없다.]
    public bool isDamaged = false; //충돌처리후 일정시간 동안 다시 데미지받지 않게 하는 플래그변수

    Coroutine invincibleCoroutine;


    void Awake(){
        currentHP = max;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        originColor = spriteRenderer.color;
        playerData = GetComponent<PlayerData>(); // Player플레이어 안의  PlayerHP랑 같이 있는 스크립트
    }

    // 무적 상태 및 색상 초기화를 한 곳에서 처리하는 함수
    void ResetInitialState()
    {
        spriteRenderer.color = originColor;
        isInvincible = false;
    }

    public void SetDead(){
        currentHP = 0; // 죽기전에 currentHP를 0 으로 만듬
        uiPlayerData.SetHP(0);
        Debug.Log("플레이어 사망");
        gameController.LevelFailed();
        
    }

    public void DecreaseHP(){ //currentHP가 1보다 클 경우를 확인 먼저 한다.
         
        // 무적상태에서는 체력이 감소하지 않는다.
        if ( isInvincible || isDamaged ) return;
        if ( currentHP <=1) {  // 새로추가
            // 잠시 3초간 깜박거리는 시간 가진 후 죽게
            invincibleTime = 3f; playerData.InvincibleTime = 3f; // UI보여주기용
            StartCoroutine(nameof(RunInvincibleTime));

            SetDead();
            return;
        }

        //! 코루틴 중복실행 방지: 코루틴이 이미 실행중인 경우 중단
        if (invincibleCoroutine != null) StopCoroutine(invincibleCoroutine);

        isDamaged = true;

        // 기존 코루틴이 중단된 이후에 시행여부 
        if (invincibleTime > 0) {
            invincibleCoroutine = StartCoroutine(nameof(RunInvincibleTime));
        }

        //! 무적시간이 없을 때 HP 를 감소하도록 한다.
        if(invincibleTime <=0){
            currentHP--;
            uiPlayerData.SetHP(currentHP);
            Debug.Log("현재HP : "+currentHP);
            // 감소할 때도 invincibleTime처럼 깜박이는 효과를 준다.
            invincibleTime = 1f; // 1초만 준다.
            invincibleCoroutine = StartCoroutine(nameof(RunInvincibleTime));
        }

        //일정 시간이 지나면 다시 데미지 받을 수 있도록 설정
        Invoke(nameof(ResetDamageFlag), 0.5f); //위에 있던 것을 아래로 옮김
        
    }

    public void IncreaseHP(){
        if (currentHP < max) {
            currentHP++;
            uiPlayerData.SetHP(currentHP);
            Debug.Log("현재HP :"+currentHP);
        }
    }

    void ResetDamageFlag(){
        isDamaged = false;
    }

    public void AddInvincibleTime(float time){ //내가 만든 것. 
        invincibleTime += time;  
        playerData.InvincibleTime += time;
    }

    public IEnumerator RunInvincibleTime(){  
        isInvincible = true;
        float blinkSpeed = 10;

        while ( invincibleTime >0){
            invincibleTime -= Time.deltaTime;
            playerData.InvincibleTime -= Time.deltaTime;

            Color color = spriteRenderer.color;
            color.a = Mathf.SmoothStep(0,1, Mathf.PingPong(Time.time *blinkSpeed, 1));
            spriteRenderer.color = color;

            yield return null;
        }   
        
        ResetInitialState();
    }

    public void StopInvincibleCoroutine(){
        if( invincibleCoroutine !=null){
            StopCoroutine(invincibleCoroutine);
            invincibleCoroutine = null;
        }
        ResetInitialState(); // 코루틴을 중단할 때 초기화
    }
}
