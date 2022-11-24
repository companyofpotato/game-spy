using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class TargetButton : MonoBehaviour
{
    TacticalAction instance;
    Button[] buttons;
    public int id = -1;
    // Start is called before the first frame update
    void Start()
    {
        instance = gameObject.GetComponentInParent<TacticalAction>();
        buttons = gameObject.GetComponentsInChildren<Button>();
        buttons[0].GetComponentInChildren<TextMeshProUGUI>().text = PersonManager.personList[id].codename;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TargetSelected()
    {
        instance.ChooseTarget(id, buttons[1]);
    }

    public void InfoClicked()
    {
        instance.ClickInformationButton(true, id, buttons[0]);
    }
}
