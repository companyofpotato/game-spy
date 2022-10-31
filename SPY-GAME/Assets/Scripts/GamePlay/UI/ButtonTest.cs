using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTest : MonoBehaviour
{
    TacticalAction instance;
    // Start is called before the first frame update
    void Start()
    {
        //instance = gameObject.GetComponentInParent<TacticalAction>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Clicked()
    {
        //Debug.Log(instance.type);
    }
}
