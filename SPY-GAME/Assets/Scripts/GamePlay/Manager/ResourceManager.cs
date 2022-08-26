using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    [SerializeField]
    public int money {get; private set;}

    [SerializeField]
    public int info {get; private set;}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeValue(int changedMoney, int changedInfo)
    {
        money = changedMoney;
        info = changedInfo;
    }
}
