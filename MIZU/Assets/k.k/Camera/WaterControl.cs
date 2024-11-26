using UnityEngine;

public class WaterControl : MonoBehaviour
{
    public GameObject water; // 上下する水オブジェクト
    public float ascendAmount = 1.0f; // 上昇時の移動量
    public float descendAmount = 1.0f; // 下降時の移動量
    public float moveSpeed = 2.0f; // 移動速度
    public bool isAscending = true; // 上昇するか下降するか(Trueなら上がる)
    private bool isDescending = false;

    private Vector3 targetPosition; // 水の目標位置
    void Start()
    {
        // 初期位置を設定
       // targetPosition = water.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // プレイヤーが触れた場合のみ動作
        {
            isDescending = false;
            // 上昇または下降する目標位置を設定
            float direction = isAscending ? ascendAmount : -descendAmount;
            targetPosition = water.transform.position + new Vector3(0, direction, 0);
        Collider col = this.GetComponent<Collider>();
        col.enabled = false;
        isDescending = true;
        }
        
    }

    void Update()
    {
        if (isDescending)
        {
// 水を目標位置に向かって移動させる
        water.transform.position = Vector3.Lerp(water.transform.position, targetPosition, Time.deltaTime * moveSpeed);
        
            
        }
        
    }
}
