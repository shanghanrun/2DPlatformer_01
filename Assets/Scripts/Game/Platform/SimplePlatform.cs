using UnityEngine;

public class SimplePlatform : PlatformBase
{
    public override void UpdateCollision(GameObject other)
    {
        Debug.Log($"{other.name}가 {gameObject.transform.parent.name}와 충돌했습니다!");
        // gameObject.name은 충돌체가 있는 platform
        // gameObject.transform.parent.name은 platform의 부모인 Platform_00_Moving
        // 충돌 처리 후 IsHit을 true로 설정하여 다시 충돌하지 않도록 함
        IsHit = true;

    }
}
