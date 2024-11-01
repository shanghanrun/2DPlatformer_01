using UnityEngine;

public class PropsSign : MonoBehaviour
{
    [SerializeField] GameObject guideObject;

    void OnTriggerEnter2D(Collider2D other){
        if ( other.CompareTag("Player")){
            guideObject.SetActive(true);
        }
    }
    void OnTriggerExit2D(Collider2D other) {
        if ( other.CompareTag("Player")){
            guideObject.SetActive(false);
        }
    }
}
