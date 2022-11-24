using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Assassination : TacticalAction
{
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        selectedAgentId = -1;
        selectedTargetId = -1;
        selectedMethodId = -1;

        usableMethod = new List<int>();
        usableMethod.Add(1);
        usableMethod.Add(2);
        usableMethod.Add(3);
        usableMethod.Add(4);

        usableEquipment = new List<int>();
        usableEquipment.Add(0);
        usableEquipment.Add(5);
        
        type = 3;
        originalCost = 50;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //선택된 도시의 정보를 AssassinationView에 반영한다.
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
            if(PersonManager.CheckTargeted(id) == false)
            {
                GameObject tmp = Instantiate(targetPrefab, targetScrollContent);
                TargetButton tt = tmp.GetComponent<TargetButton>();
                int tmpId = id;
                tt.id = tmpId;
            }
        }

        equipmentList = new List<bool>();
        for(int i = 0;i < EquipmentManager.count;i++)
        {
            equipmentList.Add(false);
            
            if(usableEquipment.Contains(i) == true)
            {
                id = i;
                GameObject tmp = Instantiate(equipmentPrefab, equipmentScrollContent);
                EquipmentButton tt = tmp.GetComponent<EquipmentButton>();
                int tmpId = id;
                tt.id = tmpId;
            }
            else if(usableMethod.Contains(i) == true)
            {
                id = i;
                GameObject tmp = Instantiate(methodPrefab, methodScrollContent);
                MethodButton tt = tmp.GetComponent<MethodButton>();
                int tmpId = id;
                tt.id = tmpId;
            }
        }

        CalculateByCity();
    }

    public override void ChooseAgent(int id, Button self)
    {
        base.ChooseAgent(id, self);
        if(selectedAgentId != -1 && selectedMethodId != -1)
        {
            CalculateByMethod();
        }
        if(selectedAgentId != -1 && selectedTargetId != -1 && selectedMethodId != -1)
        {
            UpdateOddScreen();
        }
    }

    public override void ChooseTarget(int id, Button self)
    {
        base.ChooseTarget(id, self);
        if(selectedAgentId != -1 && selectedTargetId != -1 && selectedMethodId != -1)
        {
            UpdateOddScreen();
        }
    }

    public override void ChooseMethod(int id, Button self)
    {
        base.ChooseMethod(id, self);
        if(selectedAgentId != -1 && selectedMethodId != -1)
        {
            CalculateByMethod();
        }
        if(selectedAgentId != -1 && selectedTargetId != -1 && selectedMethodId != -1)
        {
            UpdateOddScreen();
        }
    }

    public override void CalculateByAgent()
    {
        agentBef = selectedAgent.exposure * 1 + Math.Abs(selectedAgent.appearance - 60) - selectedAgent.stealth;
        agentSuccess = selectedAgent.stealth / 2;
        agentAft = (int)(selectedAgent.exposure * 1.1) + Math.Abs(selectedAgent.appearance - 60) - selectedAgent.stealth / 2;
        agentEscape = (selectedAgent.stealth + selectedAgent.narration) / 2 - selectedAgent.exposure;

        agentBefReal = selectedAgent.exposureReal * 1 + Math.Abs(selectedAgent.appearance - 60) - selectedAgent.stealthReal;
        agentSuccessReal = selectedAgent.stealthReal / 2;
        agentAftReal = (int)(selectedAgent.exposureReal * 1.1) + Math.Abs(selectedAgent.appearance - 60) - selectedAgent.stealthReal / 2;
        agentEscapeReal = (selectedAgent.stealthReal + selectedAgent.narrationReal) / 2 - selectedAgent.exposureReal;
    }

    public override void CalculateByTarget()
    {
        targetBef = selectedTarget.analysis / 2;
        targetSuccess = -1 * (selectedTarget.stealth + selectedTarget.analysis) / 4;
        targetAft = 0;
        targetEscape = 0;

        targetBefReal = selectedTarget.analysisReal / 2;
        targetSuccessReal = -1 * (selectedTarget.stealthReal + selectedTarget.analysisReal) / 4;
        targetAftReal = 0;
        targetEscapeReal = 0;
    }

    public override void CalculateByMethod()
    {
        if(selectedMethodId == 1 || selectedMethodId == 2)
        {
            methodBef = selectedMethod.bef;
            methodSuccess = selectedMethod.success * selectedAgent.aim / 100;
            methodAft = selectedMethod.aft;
            methodEscape = selectedMethod.escape;

            methodSuccessReal = selectedMethod.success * selectedAgent.aimReal / 100;
        }
        else if(selectedMethodId == 3 || selectedMethodId == 4)
        {
            methodBef = selectedMethod.bef;
            methodSuccess = selectedMethod.success * selectedAgent.handicraft / 100;
            methodAft = selectedMethod.aft;
            methodEscape = selectedMethod.escape;

            methodSuccessReal = selectedMethod.success * selectedAgent.handicraftReal / 100;
        }
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

    public override Action MakeAction()
    {
        return new Action(type, selectedAgentId, selectedCity.id, selectedTargetId, selectedMethodId, equipmentList, befReal, successReal, aftReal, escapeReal);
    }

    public void ShowPersonScrolls()
    {
        agentScroll.gameObject.SetActive(true);
        targetScroll.gameObject.SetActive(true);
    }

    public void HidePersonScrolls()
    {
        agentScroll.gameObject.SetActive(false);
        targetScroll.gameObject.SetActive(false);
    }

    public void ShowEquipScrolls()
    {
        methodScroll.gameObject.SetActive(true);
        equipmentScroll.gameObject.SetActive(true);
    }

    public void HideEquipScrolls()
    {
        methodScroll.gameObject.SetActive(false);
        equipmentScroll.gameObject.SetActive(false);
    }
}
