using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiseGas : MonoBehaviour
{
    public Transform targetObject; // 上に来たら上昇する対象物
    public float riseSpeed = 5f;   // 上昇速度
    public float riseHeight = 30f;  // 上昇する高さ
    public bool isRising = false;

    public RotateObject rotateObjectScript;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == targetObject  && rotateObjectScript.isRotating == true)
        {
            isRising = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform == targetObject || rotateObjectScript.isRotating == false)
        {
            isRising = false;
        }
    }

    private void Update()
    {
        if (isRising)
        {
            float step = riseSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(
                transform.position, transform.position + Vector3.up * riseHeight, step);
        }
    }
}
