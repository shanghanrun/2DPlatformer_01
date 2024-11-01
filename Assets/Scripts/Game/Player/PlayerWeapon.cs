using Unity.Mathematics;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    [SerializeField] Transform spawnPoint;

    public void StartFire(int direction){
        GameObject Sound = GameObject.Find("Sounds/Fire");
        if (Sound != null)
        {
            AudioSource audioSource = Sound.GetComponent<AudioSource>();
            if (audioSource != null) audioSource.Play();
        }

        GameObject clone = Instantiate(projectile, spawnPoint.position, Quaternion.identity);
        clone.GetComponent<PlayerProjectile>().Setup(direction);
    }
}
