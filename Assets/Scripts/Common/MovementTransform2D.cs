using UnityEngine;

public class MovementTransform2D : MonoBehaviour
{
    [SerializeField] float moveSpeed =0;
    [SerializeField] Vector3 moveDirection = Vector3.zero;

    void Update(){
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }

    public void MoveTo(Vector3 direction){ // 외부에서 방향만 바꾸어줌
        moveDirection = direction;
    }
}
