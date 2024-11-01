using UnityEngine;

public class CameraFollowTarget : MonoBehaviour // 카메라에 어태치한다.
{
    [SerializeField] Transform targetTr;
    [SerializeField] bool x,y,z;
    StageData stageData;

    float offsetY;

    public void Setup(StageData stageData){
        this.stageData = stageData;
        transform.position = new Vector3(stageData.CameraPosition.x, stageData.CameraPosition.y, -10);
    }

    void Awake(){
        offsetY = Mathf.Abs(transform.position.y - targetTr.position.y);
    }

    void LateUpdate(){
        // true 축만 targetTr의 좌표를 따라가도록 설정  (transform 은 카메라 좌표이다.)
        transform.position = new Vector3((x ? targetTr.position.x : transform.position.x), 
                                        (y ? targetTr.position.y + offsetY : transform.position.y),
                                        (z ? targetTr.position.z : transform.position.z)
        );

        //카메라의 좌/우측 이동 범위를 넘어가지 않도록 설정
        Vector3 position = transform.position;
        position.x = Mathf.Clamp(transform.position.x, stageData.CameraLimitMinX, stageData.CameraLimitMaxX);
        transform.position = position;
    }
}
