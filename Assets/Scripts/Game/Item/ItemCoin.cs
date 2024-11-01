using UnityEngine;

public class ItemCoin : ItemBase
{
    public override void UpdateCollision(Transform target)
    {
        // target.GetComponent<PlayerData>().Coin = target.GetComponent<PlayerData>().Coin + 1;
        target.GetComponent<PlayerData>().Coin++;  // 얻어진 값을 증가시켜 할당
    }
}
