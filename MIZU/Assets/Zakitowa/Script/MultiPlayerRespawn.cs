using UnityEngine;

public class MultiPlayerRespawn : MonoBehaviour
{
    [System.Serializable]
    public class PlayerData
    {
        public Transform player;           // プレイヤーのTransform
        public Transform initialRespawnPoint; // 初期リスポーン地点
        public Transform respawnPoint;    // 通常リスポーン地点
    }

    [SerializeField] private PlayerData[] players; // プレイヤーごとのリスポーン情報
    [SerializeField] private float fallThreshold = -10f; // 落下とみなす高さ

    private void Start()
    {
        // ゲーム開始時に初期リスポーン
        InitialRespawnAll();
    }

    private void Update()
    {
        // 全プレイヤーの位置を監視し、落下していたらリスポーン
        foreach (var playerData in players)
        {
            if (playerData.player.position.y < fallThreshold)
            {
                RespawnAll();
                break;
            }
        }
    }

    // 初期リスポーン
    private void InitialRespawnAll()
    {
        foreach (var playerData in players)
        {
            playerData.player.position = playerData.initialRespawnPoint.position;
            Debug.Log($"{playerData.player.name} has been moved to the initial respawn point.");
        }
    }

    // 通常リスポーン
    private void RespawnAll()
    {
        foreach (var playerData in players)
        {
            playerData.player.position = playerData.respawnPoint.position;
            Debug.Log($"{playerData.player.name} has been respawned.");
        }
    }
}
