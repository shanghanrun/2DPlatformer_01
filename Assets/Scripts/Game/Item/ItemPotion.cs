using UnityEngine;

public class ItemPotion : ItemBase{
    public override void UpdateCollision(Transform target){
        target.GetComponent<PlayerHP>().IncreaseHP();
        Debug.Log("HP 획득");
    }
}
