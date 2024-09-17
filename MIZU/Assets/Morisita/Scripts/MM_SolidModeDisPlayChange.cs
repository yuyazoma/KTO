using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MM_SolidModeDisPlayChange : MonoBehaviour
{
    [SerializeField]
    PlayerInputManager piManager;
    [SerializeField]
    PlayerInput pInput;
    [SerializeField]
    MM_Test_Player m_t_player;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(piManager.playerCount==2)
        {
            
        }

    }
}
