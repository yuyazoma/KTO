using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MM_SolidModelSet : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> solidModels;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchSolidModel(int changeNum)
    {

    }

    public void AddSolidModel(GameObject gameObject)
    {
        solidModels.Add(gameObject);
    }
}
