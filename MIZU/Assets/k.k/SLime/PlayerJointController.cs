using UnityEngine;
using System;
using System.Collections;

public class PlayerJointController : MonoBehaviour
{
    private CharacterJoint characterJoint;
    public static event Action<bool> OnConnectionStateChanged;

    // 接続後のクールダウン時間（秒）
    public float cooldownTime = 3f;
    private bool canConnect = true;

    void OnCollisionEnter(Collision collision)
    {
        if (canConnect && collision.gameObject.CompareTag("Arm"))
        {
            Rigidbody armRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            if (armRigidbody != null)
            {
                ConnectJoint(armRigidbody);
            }
            else
            {
                Debug.LogWarning("Arm オブジェクトに Rigidbody がありません。");
            }
        }
    }

    private void ConnectJoint(Rigidbody connectedBody)
    {
        characterJoint = gameObject.AddComponent<CharacterJoint>();
        characterJoint.connectedBody = connectedBody;
        OnConnectionStateChanged?.Invoke(true);
        Debug.Log("プレイヤーと Arm オブジェクトがジョイントで接続されました。");
        StartCoroutine(DisconnectAfterDelay());
        StartCoroutine(CooldownTimer());
    }

    private IEnumerator DisconnectAfterDelay()
    {
        yield return new WaitForSeconds(2f);
        Destroy(characterJoint);
        OnConnectionStateChanged?.Invoke(false);
        Debug.Log("CharacterJointが削除されました");
    }

    private IEnumerator CooldownTimer()
    {
        canConnect = false;
        yield return new WaitForSeconds(cooldownTime);
        canConnect = true;
        Debug.Log("接続可能になりました");
    }
}