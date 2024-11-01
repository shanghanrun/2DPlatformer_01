using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBase : MonoBehaviour
{
    [SerializeField] bool canBounce = false; //해당 타일에게 Bounce가능 여부를 각각 부여한다.
    float startPositionY; // 타일의 최초 y 위치

    //타일과 플레이어가 충돌했는지 체크 (일정시간동안 다시 충돌체크를 하지 않도록)
    public bool IsHit {private set; get;} = false;

    void Awake(){
        startPositionY = transform.position.y;
    }
    public virtual void UpdateCollision(){ // 플레이어가 타일에 충돌했을 경우 호출되는 메소드
        Debug.Log($"{gameObject.name} 타일 충돌!!.  canBounce ={canBounce}, IsHit ={IsHit}");
        if( canBounce){  // bounce가능한 타일일 경우에만 bounce 되는 행동실행됨
            print("OnBounce 실행할 예정");
            IsHit = true;
            StartCoroutine(nameof(OnBounce));
        }
    }

    IEnumerator OnBounce(){
        print("OnBounce 실행");
        float maxBounceAmount = 0.35f; // 타일이 충돌해서 올라가는 최대 높이
        yield return StartCoroutine(MoveToY(startPositionY, startPositionY +maxBounceAmount));
        yield return StartCoroutine(MoveToY(startPositionY +maxBounceAmount, startPositionY));
        IsHit = false;
    }
    IEnumerator MoveToY(float start, float end){
        // print("MoveToY 실행");
        float percent =0;
        float bounceTime = 0.2f;

        while (percent <1){
            percent += Time.deltaTime / bounceTime;

            Vector3 position = transform.position;
            position.y = Mathf.Lerp(start, end, percent);
            transform.position = position;

            yield return null;
        }
    }
}
