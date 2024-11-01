using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformEffectorExtension : MonoBehaviour
{
    PlatformEffector2D effector;

    void Awake(){
        effector = GetComponent<PlatformEffector2D>();
    }

    public void DownPlatform(){
        StartCoroutine(nameof(ReverseRotationalOffset));
    }

    IEnumerator ReverseRotationalOffset(){
        effector.rotationalOffset = 180;
        yield return new WaitForSeconds(0.5f);

        effector.rotationalOffset =0;
    }
}
