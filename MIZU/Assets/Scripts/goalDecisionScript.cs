using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goalDecisionScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //  tagがgoalだった時
        if (other.CompareTag("goal"))
        { 
        Debug.Log("goal");
        Destroy(gameObject);
        }
    }

}
