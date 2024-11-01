using System.Collections;
using UnityEngine;

public class AutoDestroyer : MonoBehaviour
{
    [SerializeField] bool onlyDeactivate = true; // 비활성화만 시킴
    [SerializeField] float destroyTime =5; // 오브젝트가 생성된 후 일정시간 지낫거 자동삭제

    IEnumerator Start(){
        while(destroyTime >0){  // 이 시간 이상이 경과하게 하고
            destroyTime -= Time.deltaTime;
            yield return null;
        }

        if( onlyDeactivate) gameObject.SetActive(false); // 컴포넌트는 .enabled =false

        else Destroy(gameObject);
    }

}
