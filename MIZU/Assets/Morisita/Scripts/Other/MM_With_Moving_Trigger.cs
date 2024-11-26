using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MM_With_Moving_Trigger : MonoBehaviour
{
    // èÊÇ¡ÇΩÇ‡ÇÃÇÃVelocityÇé©ï™ÇÃVelocityÇ…â¡éZÇµÇƒìÆÇ©Ç∑
    // â°ÇæÇØ
    [SerializeField]
    private Rigidbody otherRigidbody;
    [SerializeField]
    private bool isOnMoveGround;
    [SerializeField]
    private Vector3 originalVelocity;
    [SerializeField]
    private Vector3 addVelocity;

    string MOVE_GROUND = "MoveGround";
    Rigidbody _rb;
    MM_Test_Player test_Player;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        test_Player=GetComponent<MM_Test_Player>();
     
        Init();
        originalVelocity = new(test_Player.GetVelocity().x,0f,0f);

    }
    private void OnEnable()
    {
        Init();
    }

    private void Update()
    {
        if (isOnMoveGround)
        {
            _rb.velocity=originalVelocity+addVelocity;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        print("TriggerName=" + other.name);

        if (other.gameObject.CompareTag(MOVE_GROUND))
        {
            print("ê⁄ë±");
            isOnMoveGround = true;
            otherRigidbody = other.gameObject.GetComponent<MM_Get_Parent_Rigidbody>().rb;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (isOnMoveGround)
        {
            addVelocity = otherRigidbody.velocity;
            originalVelocity = new(test_Player.GetVelocity().x, 0f, 0f);

        }
    }


    private void OnTriggerExit(Collider other)
    {
        print("TriggerName=" + other.name);
        if (other.gameObject.CompareTag(MOVE_GROUND))
        {
            print("âèú");

            Init();
        }
    }

    void Init()
    {
        isOnMoveGround = false;

        originalVelocity = Vector3.zero;
        addVelocity = Vector3.zero;

        otherRigidbody=null;
    }
}
