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

    private InformationScreen informationScreenInstance;
    private int selectedInfoId;
    private bool prePerson;
    private Button previouslySelectedInfoButton;

    [SerializeField]
    private int actionCost = 50;

    [SerializeField]
    private Transform agentScrollContent;

    [SerializeField]
    private Transform targetScrollContent;

    [SerializeField]
    private GameObject personPrefab;

    [SerializeField]
    private GameObject informationScreen;

    [SerializeField]
    private GameObject oddInformation;

    [SerializeField]
    private TextMeshProUGUI costText;

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
        
        informationScreenInstance = informationScreen.GetComponent<InformationScreen>();
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

    public void ResetVariables()
    {
        selectedAgentId = -1;
        selectedTargetId = -1;
        selectedInfoId = -1;
        prePerson = true;
        moneyText.text = "50";
        ResetOddScreen();
        ChangeCostText();
    }

    public void ChangeCostText()
    {
        costText.text = $"$ {actionCost}";
    }

    //선택된 도시의 정보를 PersuasionView에 반영한다.
    public void ReflectCityInfo(City city1, City city2)
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
                GameObject tmp = Instantiate(personPrefab, agentScrollContent); // 프리팹 생성
                Button[] buttons = tmp.GetComponentsInChildren<Button>();
                int tmpId = id;
                buttons[0].onClick.AddListener(() => ClickInformationButton(true, tmpId, buttons[0]));
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
                buttons[0].onClick.AddListener(() => ClickInformationButton(true, tmpId, buttons[0]));
                buttons[0].GetComponentInChildren<TextMeshProUGUI>().text = PersonManager.personList[id].codename;
                buttons[1].onClick.AddListener(() => ChooseTarget(tmpId, buttons[1])); // 프리팹에 버튼 함수 할당
            }
        }

        CalculateByCity();
    }

    void ClickInformationButton(bool person, int id, Button self)
    {
        if(selectedInfoId < 0)
        {
            selectedInfoId = id;
            previouslySelectedInfoButton = self;
            self.GetComponentInChildren<Image>().color = Color.green;
            self.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
            prePerson = person;
            if(person)
            {
                informationScreenInstance.UpdatePersonInformationScreen(id);
            }
            else
            {
                informationScreenInstance.UpdateEquipmentInformationScreen(id);
            }
        }
        else if(prePerson != person || selectedInfoId != id)
        {
            previouslySelectedInfoButton.GetComponentInChildren<Image>().color = Color.white;
            previouslySelectedInfoButton.GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
            selectedInfoId = id;
            previouslySelectedInfoButton = self;
            self.GetComponentInChildren<Image>().color = Color.green;
            self.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
            prePerson = person;
            if(person)
            {
                informationScreenInstance.UpdatePersonInformationScreen(id);
            }
            else
            {
                informationScreenInstance.UpdateEquipmentInformationScreen(id);
            }
        }
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
        actionCost = num;
        moneyText.text = num.ToString();
        ChangeCostText();
    }

    public void MinusMoney()
    {
        int num = int.Parse(moneyText.text);
        num /= 2;
        actionCost = num;
        moneyText.text = num.ToString();
        ChangeCostText();
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
}
