using UnityEngine;

public class RotateBetweenAAndB : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float rotateAngle = 40;
    [SerializeField] float rotateSpeed =2;

    void Update(){
        float angle = rotateAngle * Mathf.Sin(Time.time * rotateSpeed); // Sin은 -1~1 사이 => angle은 -40 ~ 40도
        target.rotation = Quaternion.Euler(new Vector3(0,0,angle));
    }
}
