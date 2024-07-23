using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MM_OneWayTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private BoxCollider boxCollider;

    private void OnTriggerEnter(Collider other)
    {
        Physics.IgnoreCollision(other,boxCollider,true);
    }

    private void OnTriggerExit(Collider other)
    {
        Physics.IgnoreCollision(other,boxCollider,false);
    }
}
