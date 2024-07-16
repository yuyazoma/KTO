using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MM_GroundCheck : MonoBehaviour
{
    //[Header("エフェクトがついた床を判定するか")] public bool checkPlatformGroud = true;

    private string groundTag = "Ground";
    //private string platformTag = "GroundPlatform";
    //private string moveFloorTag = "MoveFloor";
    //private string fallFloorTag = "FallFloor";
    private bool isGround = false;
    private bool isGroundEnter, isGroundStay, isGroundExit;


    //接地判定を返すメソッド
    public bool IsGround()
    {
        if (isGroundEnter || isGroundStay)
        {
            isGround = true;
        }
        else if (isGroundExit)
        {
            isGround = false;
        }
        return isGround;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag(groundTag))
        {
            isGroundEnter = true;
            isGroundExit = false;
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.CompareTag(groundTag))
        {
            isGroundStay = true;
            isGroundExit = false;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag(groundTag))
        {
            isGroundExit = true;
            isGroundEnter = false;
            isGroundStay = false;
        }
    }
}