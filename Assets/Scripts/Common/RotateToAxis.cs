using UnityEngine;

public class RotateToAxis : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Vector3 axis = Vector3.forward;
    [SerializeField] float rotateSpeed = 200;

    void Update(){
        target.Rotate(axis, rotateSpeed * Time.deltaTime); // 1초에 200 스피드 각도만큼 회전.
    }
}
