using UnityEngine;
using UnityEngine.Tilemaps;
public class HiddenArea : MonoBehaviour
{
    Tilemap tilemap;

    void Awake(){
        tilemap = GetComponent<Tilemap>();
    }
    void OnTriggerEnter2D(Collider2D other){
        if( other.CompareTag("Player")){
            GameObject touchAreaSound = GameObject.Find("Sounds/TouchArea");
            if(touchAreaSound !=null){
                AudioSource audioSource = touchAreaSound.GetComponent<AudioSource>();
                if(audioSource !=null) audioSource.Play();
            }
            
            StopAllCoroutines(); // 중요한 거라서 일단 다른 코루틴 모두 정지부터.
            StartCoroutine(FadeEffect.Fade(tilemap, tilemap.color.a, 0));
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if ( other.CompareTag("Player")){ // 다시 나타나게
            StopAllCoroutines();
            StartCoroutine(FadeEffect.Fade(tilemap, tilemap.color.a, 1));
        }
    }
}
