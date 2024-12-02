using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MM_UICall : MonoBehaviour
{
    [SerializeField]
    private InputAction playerPauseInputAction;

    [SerializeField]
    public GameObject displayUI;
    [SerializeField]
    private Transform UIRoot;

    private GameObject createdUI;
    void Start()
    {
        playerPauseInputAction.performed += CreateUI;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateUI(InputAction.CallbackContext context)
    {
        print($"OnAny() : {context.control.device.name} {context.control.name}");
        if (createdUI==null)
        {
            InstantiateUI();
            MM_TimeManager.instance.StopTime();
        }
        else
        {
            Destroy(createdUI);
            MM_TimeManager.instance.MoveTime();
        }
    }

    public void InstantiateUI()
    {
        createdUI= Instantiate(displayUI, UIRoot);

    }
}
