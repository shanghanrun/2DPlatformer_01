using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIPlayerData : MonoBehaviour
{
    [Header("HP")]
    [SerializeField] Image[] hpImages;
    [Header("COIN")]
    [SerializeField] TextMeshProUGUI textCoin;

    [Header("PROJECTILE")]
    [SerializeField] TextMeshProUGUI textProjectile;

    [Header("STAR")]
    [SerializeField] GameObject[] starObjects;
    [Header("INVINCIBLE")]
    [SerializeField] TextMeshProUGUI invincible;

    public void SetHP(int currentHP)
    {
        for (int i = 0; i < hpImages.Length; i++) //모두 순회하게 함
        {
            // 현재 HP보다 낮은 인덱스의 이미지만 활성화, 나머지는 비활성화
            hpImages[i].enabled = i < currentHP;
        }
    }


    public void SetCoin(int coinCount){
        textCoin.text = $"x {coinCount}";
    }
    public void SetProjectile(int current, int max){ // 10/10 형태
        textProjectile.text = $"{current}/{max}";

        if( ((float) current/max)<=0.3f ) textProjectile.color =Color.red;
        else                              textProjectile.color =Color.white;
    }

    public void SetStar(int index){
        starObjects[index].SetActive(true);
    }
    public void SetInvincible(float invincibleTime){
        if ( invincibleTime > 0){
            invincible.text = $"무적타임({invincibleTime.ToString("F2")})"; //소수 둘째자리
        } else{
            invincible.text = $"무적타임(0.00)";
        }
    }
}
