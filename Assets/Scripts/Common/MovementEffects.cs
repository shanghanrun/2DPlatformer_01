using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementEffects : MonoBehaviour
{
    MovementRigidbody2D         movement;

    //플레이어가 이동할 때 발밑에 나오는 이펙트
    [SerializeField] ParticleSystem  footStepEffect;
    ParticleSystem.EmissionModule    footEmission;
    [SerializeField] ParticleSystem  landingEffect;
    bool                            wasOnGround;


    void Awake(){
        movement = GetComponentInParent<MovementRigidbody2D>();
        footEmission = footStepEffect.emission;
    }
    void Update(){
        //플레이어가 바닥을 밟고 있고, 좌우 이동속도가 0이 아닐 경우
        if(movement.IsGrounded && movement.Velocity.x !=0){
            footEmission.rateOverTime =30;
        } else{
            footEmission.rateOverTime =0; //공중에 떠 있거나, 제자리에 멈춘 경우
        }

        //! 바로 직전 프레임에 공중에 있었고, 이번 프레임에 바닥을 밟고 있고,
        // y속력이 0 이하일 때 바닥에 "착지"로 판단하고 이펙트 재생
        if( !wasOnGround && movement.IsGrounded && movement.Velocity.y <=0){
            landingEffect.Stop();
            landingEffect.Play();
        }

        wasOnGround = movement.IsGrounded; // 이것에 의해 항상 업데이트 된다.
    }
}
