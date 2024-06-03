using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInputSystem : MonoBehaviour
{

    private TestPlayerInput _testPlayerInput;

    // Start is called before the first frame update
    void Start()
    {
        _testPlayerInput=new TestPlayerInput();
        _testPlayerInput.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if (_testPlayerInput.Player.Fire.triggered)
            print("fire‚Ì“ü—Í‚ª‚³‚ê‚Ü‚µ‚½");
        if (_testPlayerInput.Player.Pause.triggered)
            Debug.LogWarning("Pause‚Ì“ü—Í‚ª‚³‚ê‚Ü‚µ‚½");
    }
}
