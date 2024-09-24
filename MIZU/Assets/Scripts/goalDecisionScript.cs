using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goalDecisionScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //  tag‚ªgoal‚¾‚Á‚½Žž
        if (other.CompareTag("goal"))
        { 
        Debug.Log("goal");
        Destroy(gameObject);
        }
    }

}
