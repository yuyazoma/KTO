using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class MM_PlayerSpownTest : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private List<GameObject> player;
    [SerializeField]
    private float spownTime = 5f;
    [SerializeField]
    private Transform playerSpownPoint;

    void Update()
    {
        if (player != null)
        {
            foreach (var p in player)
                if (!p.activeSelf)
                {
                    StartCoroutine(Spown(p));
                }
        }
        else
        {
            print($"PlayerSpownTestError");
        }

    }

    IEnumerator Spown(GameObject p)
    {
        p.transform.position = playerSpownPoint.position;

        yield return new WaitForSeconds(spownTime);

        p.SetActive(true);
    }

    public void GetJoinPlayer(PlayerInput playerInput)
    {
        player.Add(playerInput.gameObject);
    }
    public void LeftJoinPlayer(PlayerInput playerInput)
    {
        //player.Remove(playerInput.gameObject);
    }

    public void SpownPointUpdate(Transform transform)
    {
        playerSpownPoint = transform;
    }
}
