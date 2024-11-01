using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleAutoDestroy : MonoBehaviour
{
    ParticleSystem particle;

    void Awake(){
        particle = GetComponent<ParticleSystem>();
    }
    void Update(){
        if(!particle.isPlaying){ // 재생이 완료되면 본인삭제
            Destroy(gameObject);
        }
    }
}
