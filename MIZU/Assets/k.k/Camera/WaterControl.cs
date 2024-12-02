using UnityEngine;

public class WaterControl : MonoBehaviour
{
    public GameObject water; // 上下する水オブジェクト
    public float ascendAmount = 1.0f; // 上昇時の移動量
    public float descendAmount = 1.0f; // 下降時の移動量
    public float moveSpeed = 2.0f; // 移動速度
    public bool isAscending = true; // 上昇するか下降するか(Trueなら上がる)

    private bool isDescending = false;

    private bool isMoving = false; // 現在移動中かどうか
    private Vector3 targetPosition; // 水の目標位置
    private Vector3 startPosition; // 移動開始位置
    private float moveProgress = 0f; // 移動進捗（0～1）

    void Update()
    {
        if (isMoving)
        {
            // 移動進捗を更新
            moveProgress += Time.deltaTime * moveSpeed;

            // 進捗に基づいて位置を補間
            water.transform.position = Vector3.Lerp(startPosition, targetPosition, moveProgress);

            // 移動が完了したら停止
            if (moveProgress >= 1f)
            {
                isMoving = false;
                moveProgress = 0f;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isMoving) // 移動中でない場合のみ動作
        {
            isDescending = false;
            // 移動開始位置と目標位置を設定
            startPosition = water.transform.position;
            float direction = isAscending ? ascendAmount : -descendAmount;
            targetPosition = startPosition + new Vector3(0, direction, 0);

            Collider col = this.GetComponent<Collider>();
            col.enabled = false;
            isDescending = true;
            // 移動を開始
            isMoving = true;
        }
    }
}
