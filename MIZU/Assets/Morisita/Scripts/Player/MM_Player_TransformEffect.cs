using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MM_Player_TransformEffect : MM_IEffectPlayer
{
    private MM_PlayerPhaseState.State oldState;
    private MM_PlayerPhaseState pState;
    void Start()
    {
        pState=GetComponent<MM_PlayerPhaseState>();
        SetParticleTransform(this.gameObject.transform);
        oldState = pState.GetState();
    }
    private void Update()
    {
        if (oldState != pState.GetState())
        {
            Play();
        }
        oldState = pState.GetState();
    }


    public override void Play()
    {
        ParticleInstantiate(_particleTransform);
    }
    public override void Play(float time)
    {
        ParticleInstantiate(_particleTransform, time);
    }


}
