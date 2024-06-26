using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float rotationSpeed = 90f;  //  1秒で何度回転するか
    public float rotationDuration = 30f;  //  何秒回転するか

    private float rotationTime = 0f;
    public bool isRotating = true;

   // Start is called before the first frame update
   void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isRotating)
        {
            if (rotationTime < rotationDuration)
            {
                transform.Rotate(Vector3.back, rotationSpeed * Time.deltaTime);
                rotationTime += Time.deltaTime;
            }
            else
            {
                isRotating = false;
            }
        }
    }
}
