using UnityEngine;

public class PlatformMoving : MonoBehaviour
{
    [SerializeField] Transform target; // 실제 이동하는 발판의 Transform

    void OnCollisionEnter2D(Collision2D other){
        //발판에 충돌한 플레이어오브젝트()의 부모를 발판으로 설정(달라붙게)
        other.transform.SetParent(target.transform);
    }

    void OnCollisionExit2D(Collision2D other){
        other.transform.SetParent(null);
    }

}
