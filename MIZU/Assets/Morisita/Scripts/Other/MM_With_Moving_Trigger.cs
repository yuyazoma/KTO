using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MM_With_Moving_Trigger : MonoBehaviour
{
    [SerializeField, Header("æ‚ç‚ê‚é•û,e(‘«ê‚È‚Ç)")]
    private Transform movevParent;
    [SerializeField,Header("æ‚é•û,q(©•ª)")]
    private Transform withChild;


    string MOVE_GROUND = "MoveGround";

    private void OnTriggerEnter(Collider other)
    {
        print("TriggerName=" + other.name);

        if (other.gameObject.CompareTag(MOVE_GROUND))
        {
            print("Ú‘±");
            movevParent = other.transform;
            withChild.parent = movevParent;
            //withChild.transform.SetParent(movevParent);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        print("TriggerName=" + other.name);
        if (other.gameObject.CompareTag(MOVE_GROUND))
        {
            print("‰ğœ");
            withChild.parent=null;
            //withChild.transform.SetParent(null);
        }
    }
}
