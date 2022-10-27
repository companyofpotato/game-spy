using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Persuasion : MonoBehaviour
{
    public City baseCity {get; private set;}
    public City selectedCity {get; private set;}

    public int selectedAgentId {get; private set;}
    public Person selectedAgent {get; private set;}
    public int selectedTargetId {get; private set;}
    public Person selectedTarget {get; private set;}
    private Person shownPerson;

    public Equipment selectedEquipment {get; private set;}//지금 당장은 안 쓰지만 두자.
    private Equipment shownEquipment;

    public List<bool> equipmentList {get; private set;}
    private List<int> usableEquipment;

    private int bef;
    private int success;
    private int aft;
    private int escape;

    private int agentBef;
    private int agentSuccess;
    private int agentAft;
    private int agentEscape;

    private int targetBef;
    private int targetSuccess;
    private int targetAft;
    private int targetEscape;

    private int cityBef;
    private int citySuccess;
    private int cityAft;
    private int cityEscape;

    private int befReal;
    private int successReal;
    private int aftReal;
    private int escapeReal;

    private int agentBefReal;
    private int agentSuccessReal;
    private int agentAftReal;
    private int agentEscapeReal;

    private int targetBefReal;
    private int targetSuccessReal;
    private int targetAftReal;
    private int targetEscapeReal;

    private int moneySuccess;
    private int moneySuccessReal;

    private Button previouslySelectedAgentButton;
    private Button previouslySelectedTargetButton;

    private TextMeshProUGUI[] oddTexts;
    private TextMeshProUGUI[] personTexts;
    private TextMeshProUGUI[] equipmentTexts;

    [SerializeField]
    private int actionCost = 50;

    [SerializeField]
    private Transform agentScrollContent;

    [SerializeField]
    private Transform targetScrollContent;

    [SerializeField]
    private GameObject personPrefab;

    [SerializeField]
    private GameObject personInformation;

    [SerializeField]
    private GameObject oddInformation;

    [SerializeField]
    private GameObject equipmentInformation;

    [SerializeField]
    private TMP_InputField moneyText;

    [SerializeField]
    private GameObject confirmationView;

    [SerializeField]
    private GameObject lowMoneyView;

    [SerializeField]
    private GameObject unselectedAgentView;

    [SerializeField]
    private GameObject unselectedTargetView;

    // Start is called before the first frame update
    void Start()
    {
        selectedAgentId = -1;
        selectedTargetId = -1;

        equipmentList = new List<bool>();
        usableEquipment = new List<int>();

        oddTexts = oddInformation.GetComponentsInChildren<TextMeshProUGUI>();
        personTexts = personInformation.GetComponentsInChildren<TextMeshProUGUI>();
        equipmentTexts = equipmentInformation.GetComponentsInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetScrollContentList()
    {
        Transform[] childList = agentScrollContent.GetComponentsInChildren<Transform>();
        int childCount;

        if(childList != null)
        {
            childCount = childList.Length;
            for(int i = 1;i < childCount;i++)
            {
                Destroy(childList[i].gameObject);
            }
        }

        childList = targetScrollContent.GetComponentsInChildren<Transform>();

        if(childList != null)
        {
            childCount = childList.Length;
            for(int i = 1;i < childCount;i++)
            {
                Destroy(childList[i].gameObject);
            }
        }
    }

    public void ResetInfo()
    {
        selectedAgentId = -1;
        selectedTargetId = -1;
        ResetOddScreen();
        moneyText.text = actionCost.ToString();
    }

    //선택된 도시의 정보를 PersuasionView에 반영한다.
    public void ReflectCityInfo(City city1, City city2)
    {
        ResetScrollContentList();
        ResetInfo();

        baseCity = city1;
        selectedCity = city2;
        int count, id;

        count = baseCity.personList.Count;
        for(int i = 0;i < count;i++)
        {
            id = baseCity.personList[i];
            if(PersonManager.CheckAgent(id) && PersonManager.CheckAvailable(id) && PersonManager.CheckFriendly(id))
            {
                GameObject tmp = Instantiate(personPrefab, agentScrollContent); // 프리팹 생성
                Button[] buttons = tmp.GetComponentsInChildren<Button>();
                int tmpId = id;
                buttons[0].onClick.AddListener(() => UpdatePersonInformationScreen(tmpId));
                buttons[0].GetComponentInChildren<TextMeshProUGUI>().text = PersonManager.personList[id].codename;
                buttons[1].onClick.AddListener(() => ChooseAgent(tmpId, buttons[1])); // 프리팹에 버튼 함수 할당
            }
        }

        count = selectedCity.personList.Count;
        for(int i = 0;i < count;i++)
        {
            id = selectedCity.personList[i];
            if(PersonManager.CheckTargeted(id) == false && PersonManager.CheckPersuadable(id))
            {
                GameObject tmp = Instantiate(personPrefab, targetScrollContent); // 프리팹 생성
                Button[] buttons = tmp.GetComponentsInChildren<Button>();
                int tmpId = id;
                buttons[0].onClick.AddListener(() => UpdatePersonInformationScreen(tmpId));
                buttons[0].GetComponentInChildren<TextMeshProUGUI>().text = PersonManager.personList[id].codename;
                buttons[1].onClick.AddListener(() => ChooseTarget(tmpId, buttons[1])); // 프리팹에 버튼 함수 할당
            }
        }

        CalculateByCity();
    }

    //요원 버튼이 눌릴 경우 호출되는 함수
    public void ChooseAgent(int id, Button self)
    {
        if(selectedAgentId < 0)
        {
            selectedAgentId = id;
            previouslySelectedAgentButton = self;
            selectedAgent = PersonManager.personList[id];
            self.GetComponentInChildren<Image>().color = Color.green;
            CalculateByAgent();
        }
        else if(selectedAgentId == id)
        {
            selectedAgentId = -1;
            self.GetComponentInChildren<Image>().color = Color.white;
            ResetOddScreen();
        }
        else
        {
            previouslySelectedAgentButton.GetComponentInChildren<Image>().color = Color.white;
            selectedAgentId = id;
            previouslySelectedAgentButton = self;
            selectedAgent = PersonManager.personList[id];
            self.GetComponentInChildren<Image>().color = Color.green;
            CalculateByAgent();
        }
        if(selectedAgentId != -1 && selectedTargetId != -1)
        {
            UpdateOddScreen();
        }
    }

    public void ChooseTarget(int id, Button self)
    {
        if(selectedTargetId < 0)
        {
            selectedTargetId = id;
            previouslySelectedTargetButton = self;
            selectedTarget = PersonManager.personList[id];
            self.GetComponentInChildren<Image>().color = Color.green;
        }
        else if(selectedTargetId == id)
        {
            selectedTargetId = -1;
            self.GetComponentInChildren<Image>().color = Color.white;
            ResetOddScreen();
        }
        else
        {
            previouslySelectedTargetButton.GetComponentInChildren<Image>().color = Color.white;
            selectedTargetId = id;
            previouslySelectedTargetButton = self;
            selectedTarget = PersonManager.personList[id];
            self.GetComponentInChildren<Image>().color = Color.green;
        }
        if(selectedAgentId != -1 && selectedTargetId != -1)
        {
            CalculateByTarget();
            UpdateOddScreen();
        }
    }

    
    public void UpdatePersonInformationScreen(int id)
    {
        HideEquipmentInformation();
        ShowPersonInformation();
        shownPerson = PersonManager.personList[id];
        
        personTexts[1].text = shownPerson.firstName;
        personTexts[3].text = shownPerson.familyName;
        personTexts[5].text = shownPerson.codename;
        personTexts[7].text = shownPerson.age.ToString();
        if(shownPerson.gender)
            personTexts[9].text = "Male";
        else if(shownPerson.gender)
            personTexts[9].text = "Female";
        if(shownPerson.sexualHetero && shownPerson.sexualHomo)
            personTexts[11].text = "bisexual";
        else if(shownPerson.sexualHetero)
            personTexts[11].text = "Heterosexual";
        else if(shownPerson.sexualHomo)
            personTexts[11].text = "Homosexaul";
        personTexts[13].text = shownPerson.appearance.ToString();
        personTexts[15].text = shownPerson.status.ToString();//문자열로 수정하기
        if(shownPerson.belong > 0)
            personTexts[17].text = "Friendly";
        else if(shownPerson.belong < 0)
            personTexts[17].text = "Enemy";
        else
            personTexts[17].text = "Neutrality";
        if(shownPerson.belong == 0)
            personTexts[19].text = "X";
        else
            personTexts[19].text = shownPerson.passion.ToString();
        if(shownPerson.belong == 0)
            personTexts[21].text = "X";
        else
            personTexts[21].text = shownPerson.isAgent ? "Agent" : "Informant";
        personTexts[23].text = CityManager.cityList[shownPerson.location].name;
        personTexts[25].text = shownPerson.exposure.ToString();
        personTexts[27].text = shownPerson.rank.ToString();
        personTexts[29].text = shownPerson.aim.ToString();
        personTexts[31].text = shownPerson.stealth.ToString();
        personTexts[33].text = shownPerson.handicraft.ToString();
        personTexts[35].text = shownPerson.analysis.ToString();
        personTexts[37].text = shownPerson.narration.ToString();

        personTexts[39].text = shownPerson.trait.ToString();//추후에 수정 필요
        personTexts[41].text = shownPerson.perk.ToString();//추후에 수정 필요

        for(int i = 0, idx = 1;i < 20;i++)
        {
            if(shownPerson.CheckReveal(i) == false)
            {
                personTexts[idx].text = "???";
            }
            if(i == 5)
            {
                i++;
                if(shownPerson.CheckReveal(i) == false)
                {
                    personTexts[idx].text = "???";
                }
            }
            idx += 2;
        }

        for(int i = 0;i < 7;i++)
        {
            if(shownPerson.CheckReal(i))
            {
                switch(i)
                {
                    case 0 : personTexts[18].color = Color.blue; personTexts[19].color = Color.blue; break;
                    case 1 : personTexts[24].color = Color.blue; personTexts[25].color = Color.blue; break;
                    case 2 : personTexts[28].color = Color.blue; personTexts[29].color = Color.blue; break;
                    case 3 : personTexts[30].color = Color.blue; personTexts[31].color = Color.blue; break;
                    case 4 : personTexts[32].color = Color.blue; personTexts[33].color = Color.blue; break;
                    case 5 : personTexts[34].color = Color.blue; personTexts[35].color = Color.blue; break;
                    case 6 : personTexts[36].color = Color.blue; personTexts[37].color = Color.blue; break;
                }
            }
        }
    }

    public void UpdateEquipmentInformationScreen(int id)
    {
        HidePersonInformation();
        ShowEquipmentInformation();
        shownEquipment = EquipmentManager.equipmentList[id];

        equipmentTexts[1].text = shownEquipment.name;
        equipmentTexts[3].text = shownEquipment.description;
        if(shownEquipment.bef < 0)
            equipmentTexts[5].text = shownEquipment.bef.ToString() + "%";
        else
            equipmentTexts[5].text = "+" + shownEquipment.bef.ToString() + "%";
        if(shownEquipment.success < 0)
            equipmentTexts[7].text = shownEquipment.success.ToString() + "%";
        else
            equipmentTexts[7].text = "+" + shownEquipment.success.ToString() + "%";
        if(shownEquipment.aft < 0)
            equipmentTexts[9].text = shownEquipment.aft.ToString() + "%";
        else
            equipmentTexts[9].text = "+" + shownEquipment.aft.ToString() + "%";
        if(shownEquipment.escape < 0)
            equipmentTexts[11].text = shownEquipment.escape.ToString() + "%";
        else
            equipmentTexts[11].text = "+" + shownEquipment.escape.ToString() + "%";
    }

    public void CalculateByAgent()
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

    public void CalculateByTarget()
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

    public void CalculateByCity()
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

    public void ResetOddScreen()
    {
        oddTexts[1].text = "X";
        oddTexts[3].text = "X";
        oddTexts[5].text = "X";
        oddTexts[7].text = "X";
    }

    public void UpdateOddScreen()
    {
        bef = agentBef + targetBef + cityBef;
        success = agentSuccess + targetSuccess + citySuccess + moneySuccess;
        aft = agentAft + targetAft + cityAft;
        escape = agentEscape + targetEscape + cityEscape;

        oddTexts[1].text = bef.ToString() + "%";
        oddTexts[3].text = success.ToString() + "%";
        oddTexts[5].text = aft.ToString() + "%";
        oddTexts[7].text = escape.ToString() + "%";

        befReal = agentBefReal + targetBefReal + cityBef;
        successReal = agentSuccessReal + targetSuccessReal + citySuccess + moneySuccessReal;
        aftReal = agentAftReal + targetAftReal + cityAft;
        escapeReal = agentEscapeReal + targetEscapeReal + cityEscape;
    }
    
    public void PlusMoney()
    {
        int num = int.Parse(moneyText.text);
        num *= 2;
        moneyText.text = num.ToString();
    }

    public void MinusMoney()
    {
        int num = int.Parse(moneyText.text);
        num /= 2;
        moneyText.text = num.ToString();
    }

    public void Execute()
    {
        actionCost = int.Parse(moneyText.text);
        if(selectedAgentId < 0)
        {
            unselectedAgentView.gameObject.SetActive(true);
        }
        else if(ResourceManager.money < actionCost)
        {
            lowMoneyView.gameObject.SetActive(true);
        }
        else if(selectedTargetId < 0)
        {
            unselectedTargetView.gameObject.SetActive(true);
        }
        else
        {
            Action newAction = new Action(4, selectedAgentId, selectedCity.id, selectedTargetId, 0, equipmentList, befReal, successReal, aftReal, escapeReal);//4는 Persuasion의 type이다.
            ActionManager.AddAction(newAction);
            PersonManager.ChangeStatus(selectedAgentId, 4);
            PersonManager.ChangeIsTargeted(selectedTargetId, 4);
            ResourceManager.ChangeMoney(ResourceManager.money - actionCost);

            confirmationView.gameObject.SetActive(true);
        }
    }
    
    public void HideConfirmationView()
    {
        confirmationView.gameObject.SetActive(false);
    }

    public void ShowPersonInformation()
    {
        personInformation.gameObject.SetActive(true);
    }

    public void HidePersonInformation()
    {
        personInformation.gameObject.SetActive(false);
    }

    public void ShowEquipmentInformation()
    {
        equipmentInformation.gameObject.SetActive(true);
    }

    public void HideEquipmentInformation()
    {
        equipmentInformation.gameObject.SetActive(false);
    }

    public void HideInformationScreen()
    {
        personInformation.gameObject.SetActive(false);
        equipmentInformation.gameObject.SetActive(false);
    }
}
