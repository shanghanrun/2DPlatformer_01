using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField] UIPlayerData uiPlayerData;
    public int coin =0;
    public int projectile=0; // 개발을 위해 이게 더 낫다.
    public int maxProjectile = 10;  // 개발을 위해 이게 더 낫다.
    public bool[] stars = new bool[3]{false,false,false};
    public float invincibleTime = 0;

    public int Coin {
        set {
            coin = Mathf.Clamp(value,0,9999);  //코인 갯수를 0~9999개 까지 값만 갖도록
            uiPlayerData.SetCoin(coin);
        }
        get => coin;
    }
    public float InvincibleTime{
        set {
            invincibleTime = value;
            uiPlayerData.SetInvincible(invincibleTime);
        }
        get => invincibleTime;
    }
    

    public int CurrentProjectile{
        set {
            projectile = Mathf.Clamp(value,0, maxProjectile);
            uiPlayerData.SetProjectile(projectile, maxProjectile);
        }
        get => projectile;
    }  

    public bool[] Stars => stars;
    public void GetStar(int index){
        stars[index] = true;
        uiPlayerData.SetStar(index);
    }

    void Awake(){
        // Coin =0; // Coin set프로퍼티를 호출해서, 게임시작할 때, 화면에 코인갯수 출력되게 함
        CurrentProjectile = 0;
        InvincibleTime = 0;
    }
}
