using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    public GameObject player1Model;   // プレイヤー1モデル
    public GameObject player2Model;   // プレイヤー2モデル

    private Collider player1Collider;
    private Collider player2Collider;


    // 複数の衝突したオブジェクトを保持するためのリスト
    public List<Collider> hitCollidersList { get; private set; } = new List<Collider>();


    // Start is called before the first frame update
    void Start()
    {
        if (player1Model != null)
        {
            player1Collider = player1Model.GetComponent<Collider>();
        }
        if (player2Model != null)
        {
            player2Collider = player2Model.GetComponent<Collider>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player1Collider != null)
        {
            CheckCollisions(player1Collider, "Player");
        }
        if (player2Collider != null)
        {
            CheckCollisions(player2Collider, "Player 2");
        }
    }

    // 衝突を確認するメソッド
    private void CheckCollisions(Collider playerCollider, string playerName)
    {
        // 前回の衝突リストをクリア
        hitCollidersList.Clear();

        Collider[] hitColliders = Physics.OverlapSphere(playerCollider.transform.position, playerCollider.bounds.extents.magnitude);

        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider != playerCollider)
            {
                Debug.Log(playerName + " collided with " + hitCollider.gameObject.name);
                hitCollidersList.Add(hitCollider);  // 衝突したオブジェクトをリストに追加
            }
        }
    }
}
