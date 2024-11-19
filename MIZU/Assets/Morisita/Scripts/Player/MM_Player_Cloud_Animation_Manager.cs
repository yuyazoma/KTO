using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MM_Player_Cloud_Animation_Manager : MonoBehaviour
{
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
        PlayerCloudAnim();
    }

    void PlayerCloudAnim()
    {
        float pSpeed_y=playerTest.GetAbsSpeed().y;

        _animator.SetFloat("Speed_y", pSpeed_y);

    }
}
