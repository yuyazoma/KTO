using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MM_PlayerAnimationManager : MonoBehaviour
{
    [SerializeField]
    private MM_Test_Player tPlayer;
    [SerializeField]
    private MM_GroundCheck groundCheck;
    [SerializeField]
    private bool isJumping=false;

    Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.localPosition = new(0f,-0.3f,0);
        PlayJumpAnim();
    }

    void PlayJumpAnim()
    {
        float pSpeed_X = tPlayer.GetSpeed().x;
        float pSpeed_Y = tPlayer.GetSpeed().y;
        bool isGround = groundCheck.IsGround();


        _animator.SetFloat("Speed", pSpeed_X);

        if (pSpeed_Y > 0.1 && !isJumping)
        {
            _animator.SetBool("OnGround", false);
            _animator.Play("jump", 0, 0f);
            isJumping = true;
        }
        else if (isGround)
        {
            _animator.SetBool("OnGround", true);
            isJumping = false;
        }
        else
        {
            _animator.SetBool("OnGround", false);
            _animator.Play("jump", 0, 0.7f);
        }


    }
}
