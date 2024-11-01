using UnityEngine;

public class PropsGoal : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Player")){
            GameObject touchAreaSound = GameObject.Find("Sounds/TouchArea");
            if (touchAreaSound != null)
            {
                AudioSource audioSource = touchAreaSound.GetComponent<AudioSource>();
                if (audioSource != null) audioSource.Play();
            }

            other.GetComponent<PlayerController>().LevelComplete();
            Debug.Log("Game Clear");
        }
    }
}
