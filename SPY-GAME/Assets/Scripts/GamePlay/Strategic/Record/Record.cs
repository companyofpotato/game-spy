using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Linq;

public class Record : MonoBehaviour
{
    private AgentRecord agentInstance;
    private InformantRecord informantInstance;
    private PrisonerRecord prisonerInstance;
    private CityRecord cityInstance;
    private ReportRecord reportInstance;

    private Button[] buttons;

    [SerializeField]
    private GameObject agentArea;

    [SerializeField]
    private GameObject informantArea;

    [SerializeField]
    private GameObject prisonerArea;

    [SerializeField]
    private GameObject cityArea;

    [SerializeField]
    private GameObject reportArea;

    [SerializeField]
    private GameObject buttonArea;


    // Start is called before the first frame update
    void Start()
    {
        agentInstance = gameObject.GetComponent<AgentRecord>();
        informantInstance = gameObject.GetComponent<InformantRecord>();
        prisonerInstance = gameObject.GetComponent<PrisonerRecord>();
        cityInstance = gameObject.GetComponent<CityRecord>();
        reportInstance = gameObject.GetComponent<ReportRecord>();

        buttons = buttonArea.GetComponentsInChildren<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetVariables()
    {
        agentInstance.ResetVariables();
        informantInstance.ResetVariables();
        prisonerInstance.ResetVariables();
        cityInstance.ResetVariables();
        reportInstance.ResetVariables();
    }

    public void ResetScrollContentList()
    {
        agentInstance.ResetScrollContentList();
        informantInstance.ResetScrollContentList();
        prisonerInstance.ResetScrollContentList();
        cityInstance.ResetScrollContentList();
    }

    public void MakeStructList()
    {
        agentInstance.MakeStructList();
        informantInstance.MakeStructList();
        prisonerInstance.MakeStructList();
        cityInstance.MakeStructList();
    }

    public void ClickButton(int n)
    {
        HideAllArea();
        for(int i = 0;i < 5;i++)
        {
            buttons[i].GetComponent<Image>().color = Color.white;
        }
        buttons[n].GetComponent<Image>().color = Color.green;
    }

    public void HideAgentArea()
    {
        agentArea.gameObject.SetActive(false);
    }

    public void HideInformantArea()
    {
        informantArea.gameObject.SetActive(false);
    }

    public void HidePrisonerArea()
    {
        prisonerArea.gameObject.SetActive(false);
    }

    public void HideCityArea()
    {
        cityArea.gameObject.SetActive(false);
    }

    public void HideReportArea()
    {
        reportArea.gameObject.SetActive(false);
    }

    public void HideAllArea()
    {
        HideAgentArea();
        HideInformantArea();
        HidePrisonerArea();
        HideCityArea();
        HideReportArea();
    }
}
