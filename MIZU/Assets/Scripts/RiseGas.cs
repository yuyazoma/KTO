using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 必要なコンポーネントを強制的にアタッチ
[RequireComponent(typeof(MM_PlayerPhaseState))]
[RequireComponent(typeof(Collider))]

public class RiseGas : MonoBehaviour
{
    public float riseSpeed = 5f;   //  上昇速度
    public float riseHeight = 30f; //  上昇する高さ
    public bool isRising = false;

    private Vector3 initialPosition;

    private MM_PlayerPhaseState _pState;

    public RotateObject rotateObjectScript;

    void Start()
    {
        // 必要なコンポーネントの取得
        _pState = GetComponent<MM_PlayerPhaseState>();

        // エラーチェック
        if (_pState == null)
        {
            Debug.LogError("MM_PlayerPhaseState コンポーネントが見つかりません！");
        }

        if (rotateObjectScript == null)
        {
            Debug.LogError("RotateObjectScript が設定されていません！");
        }

        // 初期位置を保存
        initialPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        //  トリガーに入ったオブジェクトがriseGasのタグを持っていて、RotateObjectが回転中の場合
        if (rotateObjectScript != null && rotateObjectScript.isRotating && other.CompareTag("riseGas"))
        {
            isRising = true;
            Debug.Log("RiseGas started rising due to trigger.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //  トリガーから出たオブジェクトがriseGasのタグを持っている、または RotateObjectが回転を停止した場合
        if (other.CompareTag("riseGas") || (rotateObjectScript != null && !rotateObjectScript.isRotating))
        {
            isRising = false;
            Debug.Log("RiseGas stopped rising due to trigger exit or rotation stopped.");
        }
    }

    void Update()
    {
       

        if (isRising)
        {
            //  上昇
            float step = riseSpeed * Time.deltaTime;
            Vector3 newPosition = transform.position + Vector3.up * step;
            transform.position = newPosition;

            //  目標高さに達したら上昇を停止
            Vector3 targetPosition = initialPosition + Vector3.up * riseHeight;
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                isRising = false;
                Debug.Log("RiseGas reached target height and stopped rising.");
            }
        }
    }
}
