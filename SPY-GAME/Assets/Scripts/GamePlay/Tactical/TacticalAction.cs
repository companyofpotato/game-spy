using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TacticalAction : MonoBehaviour
{
    public int type;
    // Start is called before the first frame update
    void Start()
    {
        type = 1289;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract void Function();
}
