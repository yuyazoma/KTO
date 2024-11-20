using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class TargetGroupWeight : MonoBehaviour
{
    public CinemachineTargetGroup targetGroup;
    public Transform player1;
    public Transform player2;

    void Update()
    {
        if (player1.position.x > player2.position.x)
        {
            SetWeights(2f, 1f);
        }
        else
        {
            SetWeights(1f, 2f);
        }
    }

    private void SetWeights(float weight1, float weight2)
    {
        if (targetGroup != null)
        {
            targetGroup.m_Targets[0].weight = weight1;
            targetGroup.m_Targets[1].weight = weight2;
        }
    }
}
