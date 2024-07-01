using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhaseState : MonoBehaviour
{
    public enum State
    {
        Init,
        Liquid,
        Gas,
        Solid,
        Slime,
    }

    private State m_state;

    public PlayerPhaseState()
    {
        m_state = State.Init;
    }
        
    public void ChangeState(PlayerPhaseState.State state)
    {
        m_state=state;
    }

    public State GetState()
    {
        return m_state;
    }
}
