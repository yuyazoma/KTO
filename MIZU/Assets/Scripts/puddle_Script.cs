using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MM_PlayerPhaseState))]

public class puddle_Script : MonoBehaviour
{

    private float contactTime = 0f;  //  接触した時間
    private bool isColliding = false;  //  オブジェクトが接触しているかのフラグ
    public float destroyTime = 2f;  //  オブジェクトが破壊される時間

    MM_PlayerPhaseState _pState;

    private void OnCollisionEnter(Collision collision)
    {
        if(_pState.GetState() == MM_PlayerPhaseState.State.Liquid)
        {
            isColliding = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.CompareTag("puddle"))
        {
            isColliding = false;
            contactTime = 0f;  //  離れたら接触時間をリセットする
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isColliding)
        {
            contactTime += Time.deltaTime;

            if(contactTime >= destroyTime)
            {
                Destroy(gameObject);
            }
        }
    }
}
