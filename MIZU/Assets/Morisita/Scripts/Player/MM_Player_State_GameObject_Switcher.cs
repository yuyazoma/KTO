using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MM_Player_State_GameObject_Switcher : MonoBehaviour
{
    [SerializeField]
    private GameObject _waterObject;
    [SerializeField]
    private GameObject _gasObject;
    [SerializeField]
    private GameObject _solidObject;

    private void Start()
    {
        OnStateWater();
    }
    public void Switch(MM_PlayerPhaseState.State state)
    {
        if (state == MM_PlayerPhaseState.State.Liquid) OnStateWater();

        if (state == MM_PlayerPhaseState.State.Gas) OnStateGas();

        if (state == MM_PlayerPhaseState.State.Solid) OnStateSolid();

    }

    private void OnStateWater()
    {
        _waterObject.SetActive(true);
        _gasObject.SetActive(false);
        _solidObject.SetActive(false);
    }
    private void OnStateGas()
    {
        _waterObject.SetActive(false);
        _gasObject.SetActive(true);
        _solidObject.SetActive(false);
    }
    private void OnStateSolid()
    {
        _waterObject.SetActive(false);
        _gasObject.SetActive(false);
        _solidObject.SetActive(true);
    }
}
