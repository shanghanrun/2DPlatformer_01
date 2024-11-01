using UnityEngine;

public class EnemyFlowerAnimator : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform spawnPoint;
    [SerializeField] Vector3 moveDirection = Vector3.down;

    public void OnFireEvent(){
        /*GameObject clone = */ Instantiate(projectilePrefab, spawnPoint.position, Quaternion.identity);
        // clone.GetComponent<MovementTransform2D>().MoveTo(moveDirection);


    }
}
