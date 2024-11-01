using UnityEngine;

public class ItemInvincibility : ItemBase
{
    [SerializeField] float time =0.2f;

    public override void UpdateCollision(Transform target)
    {
        target.GetComponent<PlayerHP>().AddInvincibleTime(time);
    }
}
