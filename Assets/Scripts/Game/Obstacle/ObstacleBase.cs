using UnityEngine;
using UnityEngine.UI;

public class ObstacleBase : MonoBehaviour
{
    [SerializeField] bool isInstantDeath = true; // 무서운 무기에만 적용
    PlayerHP playerHP;
    // [SerializeField] UIPlayerData uiPlayerData;
    // [SerializeField] Image[] hpImages;

    // void Awake(){
    //     if(uiPlayerData == null){
    //         uiPlayerData = FindObjectOfType<UIPlayerData>();
    //     }
    //     if(hpImages == null || hpImages.Length ==0){
    //         //특정 부모 아래에 있는 이미지 가져오기
    //         GameObject playerHPObject = GameObject.Find("PlayerHP");
    //         if(playerHPObject !=null){
    //             hpImages = new Image[3];
    //             hpImages[0] = playerHPObject.transform.Find("HP00").GetComponent<Image>();
    //             hpImages[1] = playerHPObject.transform.Find("HP01").GetComponent<Image>();
    //             hpImages[2] = playerHPObject.transform.Find("HP02").GetComponent<Image>();
    //         }
    //         else{
    //             Debug.Log("PlayerHP 오브젝트를 찾을 수 없습니다.");
    //         }
    //     }

    // }

    void OnTriggerEnter2D(Collider2D other){
        // 플에이어와 충돌했을 때만 처리
        if (!other.CompareTag("Player")) return;

        playerHP = other.GetComponent<PlayerHP>();

        if (playerHP.currentHP  ==0) return; // 이미 죽은 경우는 처리 안함
        

        if( isInstantDeath  || playerHP.currentHP ==1){ //즉각적으로 죽는 장애물 만남
                if( playerHP.invincibleTime <=0){
                    playerHP.SetDead();
                } else{
                    playerHP.DecreaseHP();
                }
        } 
        else{  //currentHP가 1보다 클 경우
            playerHP.DecreaseHP();    
        }
    }

    void OnTriggerExit2D(Collider2D other){
        if(!other.CompareTag("Player")) return;
        
        var playerHP = other.GetComponent<PlayerHP>();
        if (playerHP.currentHP == 0) return;

        if (playerHP.currentHP >0) playerHP.StopInvincibleCoroutine();
    }

}
