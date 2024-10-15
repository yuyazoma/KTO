using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MM_GroundCheck : MonoBehaviour
{
    //[Header("エフェクトがついた床を判定するか")] public bool checkPlatformGroud = true;

    private string groundTag = "Ground";
    private string waterTag = "puddle";
    //private string platformTag = "GroundPlatform";
    //private string moveFloorTag = "MoveFloor";
    //private string fallFloorTag = "FallFloor";
    private bool isGround = false;
    private bool isGroundEnter, isGroundStay, isGroundExit;
    private bool isWater = false;
    private bool isWaterEnter, isWaterStay, isWaterExit;


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

    public bool IsPuddle()
    {
        if (isWaterEnter || isWaterStay)
        {
            isWater = true;
        }
        else if (isWaterExit)
        {
            isWater = false;
        }
        return isWater;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag(groundTag))
        {
            isGroundEnter = true;
            isGroundExit = false;
        }
        if (collision.CompareTag(waterTag))
        {
            isWaterEnter = true;
            isWaterExit = false;
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.CompareTag(groundTag))
        {
            isGroundStay = true;
            isGroundExit = false;
        }
        if (collision.CompareTag(waterTag))
        {
            isWaterStay = true;
            isWaterExit = false;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag(groundTag))
        {
            isGroundEnter = false;
            isGroundStay = false;
            isGroundExit = true;
        }
        if (collision.CompareTag(waterTag))
        {
            isWaterEnter = false;
            isWaterStay = false;
            isWaterExit = true;
        }
    }
}