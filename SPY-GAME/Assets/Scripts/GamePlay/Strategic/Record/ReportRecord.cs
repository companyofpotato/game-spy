using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Linq;

public class ReportRecord : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField numberText;

    [SerializeField]
    private Transform reportContent;

    [SerializeField]
    private GameObject reportPrefab;

    private List<List<string>> reportListList;

    // Start is called before the first frame update
    void Start()
    {
        //numberText.text = "0";
        reportListList = new List<List<string>>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetVariables()
    {
        reportListList = ActionManager.reportListList;
        numberText.text = $"{ActionManager.currentTurn - 1}";
    }

    public void ShowReport(int n)
    {
        Transform[] childList = reportContent.GetComponentsInChildren<Transform>();
        
        int childCount = childList.Length;
        for(int i = 3;i < childCount;i++)
        {
            Destroy(childList[i].gameObject);
        }
        
        List<string> reportList = new List<string>();
        
        reportList = reportListList[n];

        TextMeshProUGUI tmpText = reportContent.GetComponentInChildren<TextMeshProUGUI>();
        tmpText.text = $"Report of Turn {n}.";

        foreach(string item in reportList)
        {
            GameObject tmp = Instantiate(reportPrefab, reportContent);
            tmpText = tmp.GetComponentInChildren<TextMeshProUGUI>();
            tmpText.text = item;
        }
    }

    public void ValueChanged()
    {
        ShowReport(int.Parse(numberText.text));
    }

    public void PlusTurn()
    {
        int num = int.Parse(numberText.text);

        if(num >= ActionManager.currentTurn - 1)
            return;

        num++;
        numberText.text = num.ToString();
    }

    public void MinusTurn()
    {
        int num = int.Parse(numberText.text);

        if(num < 1)
            return;

        num--;
        numberText.text = num.ToString();
    }
}
