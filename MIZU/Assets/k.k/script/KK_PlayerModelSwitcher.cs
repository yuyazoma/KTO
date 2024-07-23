using UnityEngine;

public class KK_PlayerModelSwitcher : MonoBehaviour
{
    public GameObject liquidModel;   // 液体状態のモデル
    public GameObject gasModel;      // 気体状態のモデル
    public GameObject solidModel;    // 固体状態のモデル
    public GameObject slimeModel;    // スライム状態のモデル

    private GameObject currentModel; // 現在表示しているモデル

    void Start()
    {
        // 初期状態を液体モデルに設定
        SwitchToModel(liquidModel);
    }

    public void SwitchToModel(GameObject newModel)
    {
        // 現在のモデルがあれば非アクティブにする
        if (currentModel != null)
        {
            currentModel.SetActive(false);
        }

        // 新しいモデルを設定してアクティブにする
        currentModel = newModel;
        currentModel.SetActive(true);
    }
}
