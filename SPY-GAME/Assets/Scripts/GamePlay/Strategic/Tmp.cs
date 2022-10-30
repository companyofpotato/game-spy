using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Tmp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void hand()
    {
        gameObject.GetComponentInChildren<Image>().color = Color.green;
        gameObject.GetComponentInChildren<Transform>().SetSiblingIndex(1);
    }
}
