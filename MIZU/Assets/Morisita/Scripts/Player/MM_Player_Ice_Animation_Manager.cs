using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MM_Player_Ice_Animation_Manager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private MM_Test_Player playerTest;

    Animator _animator;
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerIceAnim();   
    }

    void PlayerIceAnim()
    {
        float pSpeed_x=playerTest.GetAbsSpeed().x;

        _animator.SetFloat("Speed", pSpeed_x);
    }
}
