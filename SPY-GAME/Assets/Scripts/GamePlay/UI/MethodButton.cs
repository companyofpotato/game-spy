using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class MethodButton : MonoBehaviour
{
    TacticalAction instance;
    Button[] buttons;
    public int id = -1;
    // Start is called before the first frame update
    void Start()
    {
        instance = gameObject.GetComponentInParent<TacticalAction>();
        buttons = gameObject.GetComponentsInChildren<Button>();
        buttons[0].GetComponentInChildren<TextMeshProUGUI>().text = EquipmentManager.equipmentList[id].name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MethodSelected()
    {
        instance.ChooseMethod(id, buttons[1]);
    }

    public void InfoClicked()
    {
        instance.ClickInformationButton(false, id, buttons[0]);
    }
}
