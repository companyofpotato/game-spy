using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Persuasion : TacticalAction
{
    
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        selectedAgentId = -1;
        selectedTargetId = -1;
        originalCost = 50;
        type = 4;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void ResetVariables()
    {
        moneyText.text = "50";
        base.ResetVariables();
    }

    //선택된 도시의 정보를 PersuasionView에 반영한다.
    public override void ReflectCityInfo(City city1, City city2)
    {
        ResetScrollContentList();
        ResetVariables();

        baseCity = city1;
        selectedCity = city2;
        int count, id;

        count = baseCity.personList.Count;
        for(int i = 0;i < count;i++)
        {
            id = baseCity.personList[i];
            if(PersonManager.CheckAgent(id) && PersonManager.CheckAvailable(id) && PersonManager.CheckFriendly(id))
            {
                GameObject tmp = Instantiate(agentPrefab, agentScrollContent);
                AgentButton tt = tmp.GetComponent<AgentButton>();
                int tmpId = id;
                tt.id = tmpId;
            }
        }

        count = selectedCity.personList.Count;
        for(int i = 0;i < count;i++)
        {
            id = selectedCity.personList[i];
            if(PersonManager.CheckTargeted(id) == false && PersonManager.CheckPersuadable(id))
            {
                GameObject tmp = Instantiate(targetPrefab, targetScrollContent);
                TargetButton tt = tmp.GetComponent<TargetButton>();
                int tmpId = id;
                tt.id = tmpId;
            }
        }

        CalculateByCity();
    }

    public override void ChooseAgent(int id, Button self)
    {
        base.ChooseAgent(id, self);
        if(selectedAgentId != -1 && selectedTargetId != -1)
        {
            UpdateOddScreen();
        }
    }

    public override void ChooseTarget(int id, Button self)
    {
        base.ChooseTarget(id, self);
        if(selectedAgentId != -1 && selectedTargetId != -1)
        {
            UpdateOddScreen();
        }
    }

    public override void CalculateByAgent()
    {
        agentBef = selectedAgent.exposure * 1 + Math.Abs(selectedAgent.appearance - 60) - selectedAgent.stealth;
        agentSuccess = selectedAgent.narration;
        agentAft = 0;
        agentEscape = (selectedAgent.stealth + selectedAgent.narration) / 2 - selectedAgent.exposure;

        agentBefReal = selectedAgent.exposureReal * 1 + Math.Abs(selectedAgent.appearance - 60) - selectedAgent.stealthReal;
        agentSuccessReal = selectedAgent.narrationReal;
        agentAftReal = 0;
        agentEscapeReal = (selectedAgent.stealthReal + selectedAgent.narrationReal) / 2 - selectedAgent.exposureReal;
    }

    public override void CalculateByTarget()
    {
        targetBef = selectedTarget.analysis / 2;
        if(PersonManager.CheckSexualMatch(selectedAgentId, selectedTargetId) && PersonManager.CheckPerk(selectedAgentId, 0) && PersonManager.CheckPerk(selectedTargetId, 0) == false)
            targetSuccess = selectedAgent.appearance / 2;
        else
            targetSuccess = 0;
        if(PersonManager.CheckTrait(selectedTargetId, 0))
            targetSuccess += 40;
        targetSuccess -= (selectedTarget.analysis + selectedTarget.narration + selectedTarget.passion) / 6;
        targetAft = 0;
        targetEscape = 0;

        targetBefReal = selectedTarget.analysisReal / 2;
        if(PersonManager.CheckSexualMatch(selectedAgentId, selectedTargetId) && PersonManager.CheckPerkReal(selectedAgentId, 0) && PersonManager.CheckPerkReal(selectedTargetId, 0) == false)
            targetSuccessReal = selectedAgent.appearance / 2;
        else
            targetSuccessReal = 0;
        if(PersonManager.CheckTraitReal(selectedTargetId, 0))
            targetSuccessReal += 40;
        targetSuccessReal -= (selectedTarget.analysisReal + selectedTarget.narrationReal + selectedTarget.passionReal) / 4;
        targetAftReal = 0;
        targetEscapeReal = 0;
    }

    public override void CalculateByCity()
    {
        cityBef = 0;
        citySuccess = 0;
        cityAft = 0;
        cityEscape = 0;

        
        if(selectedCity.CheckBuilding(3) || selectedCity.CheckBuilding(4))
        {
            cityBef -= 10;
            cityAft += 10;
            cityEscape += 10;
        }
    }

    public void CalculateByMoney()
    {
        int moneyResistence = 100;
        moneySuccess = int.Parse(moneyText.text) / moneyResistence;
        moneySuccessReal = int.Parse(moneyText.text) / moneyResistence;
        if(selectedAgentId != -1 && selectedTargetId != -1)
        {
            UpdateOddScreen();
        }
    }
    
    public void PlusMoney()
    {
        int num = int.Parse(moneyText.text);
        num *= 2;
        actionCost = num;
        moneyText.text = num.ToString();
        ChangeCostText();
        CalculateByMoney();
    }

    public void MinusMoney()
    {
        int num = int.Parse(moneyText.text);
        num /= 2;
        actionCost = num;
        moneyText.text = num.ToString();
        ChangeCostText();
        CalculateByMoney();
    }
    
    public override Action MakeAction()
    {
        return new Action(type, selectedAgentId, selectedCity.id, selectedTargetId, 0, equipmentList, befReal, successReal, aftReal, escapeReal);
    }
}
