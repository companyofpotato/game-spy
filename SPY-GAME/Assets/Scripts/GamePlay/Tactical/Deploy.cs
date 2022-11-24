using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Deploy : TacticalAction
{
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        selectedAgentId = -1;
        usableEquipment = new List<int>();
        usableEquipment.Add(0);
        type = 1;
        originalCost = 50;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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

        equipmentList = new List<bool>();
        for(int i = 0;i < EquipmentManager.count;i++)
        {
            equipmentList.Add(false);
            
            if(usableEquipment.Contains(i) == false)
            {
                continue;
            }

            id = i;
            GameObject tmp = Instantiate(equipmentPrefab, equipmentScrollContent);
            EquipmentButton tt = tmp.GetComponent<EquipmentButton>();
            int tmpId = id;
            tt.id = tmpId;
        }
        
        CalculateByCity();
    }

    public override void ChooseAgent(int id, Button self)
    {
        base.ChooseAgent(id, self);
        if(selectedAgentId != -1)
        {
            UpdateOddScreen();
        }
    }

    public override void CalculateByAgent()
    {
        agentBef = selectedAgent.exposure * 1 + Math.Abs(selectedAgent.appearance - 60) - selectedAgent.stealth;
        agentSuccess = (int)(selectedAgent.stealth * 0.66 + selectedAgent.narration * 0.33);
        agentAft = 0;
        agentEscape = (selectedAgent.stealth + selectedAgent.narration) / 2 - selectedAgent.exposure;

        agentBefReal = selectedAgent.exposureReal * 1 + Math.Abs(selectedAgent.appearance - 60) - selectedAgent.stealthReal;
        agentSuccessReal = (int)(selectedAgent.stealthReal * 0.66 + selectedAgent.narrationReal * 0.33);
        agentAftReal = 0;
        agentEscapeReal = (selectedAgent.stealthReal + selectedAgent.narrationReal) / 2 - selectedAgent.exposureReal;
    }

    public override void CalculateByCity()
    {
        cityBef = 0;
        citySuccess = 0;
        cityAft = 0;
        cityEscape = 0;
        
        if(selectedCity.CheckBuilding(3) || selectedCity.CheckBuilding(4))
        {
            cityBef += 10;
            cityAft += 10;
        }
    }

    public override Action MakeAction()
    {
        return new Action(type, selectedAgentId, baseCity.id, selectedCity.id, 0, equipmentList, befReal, successReal, aftReal, escapeReal);
    }
}