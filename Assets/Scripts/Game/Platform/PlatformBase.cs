using UnityEngine;

public abstract class PlatformBase : MonoBehaviour
{
    // 플랫폼과 플레이어가 충돌했는 지 체크(일정시간동안 다시 충돌처리 하지 않도록 충돌여부를 나타내는 bool 타입의 프로퍼티)
    public bool IsHit {protected set; get;} = false;

    public abstract void UpdateCollision(GameObject other); // other는 플랫폼과 충돌한 플레이어 게임오브젝트가 된다.
}
