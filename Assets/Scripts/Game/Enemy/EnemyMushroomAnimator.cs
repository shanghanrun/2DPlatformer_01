using UnityEngine;

public class EnemyMushroomAnimator : MonoBehaviour
{
    [SerializeField] GameObject parent;
    public void OnDieEvent(){
        Destroy(parent);
    }
}
