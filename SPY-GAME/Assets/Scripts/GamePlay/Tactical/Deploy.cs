using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Deploy : MonoBehaviour
{
    public City baseCity {get; private set;}
    public City selectedCity {get; private set;}

    public int selectedAgentId {get; private set;}
    public Person selectedAgent {get; private set;}
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

    private int equipmentBef;
    private int equipmentSuccess;
    private int equipmentAft;
    private int equipmentEscape;

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

    private Button previouslySelectedAgentButton;

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
    private Transform equipmentScrollContent;

    [SerializeField]
    private GameObject personPrefab;

    [SerializeField]
    private GameObject equipmentPrefab;

    [SerializeField]
    private GameObject informationScreen;

    [SerializeField]
    private GameObject oddInformation;

    [SerializeField]
    private TextMeshProUGUI costText;

    [SerializeField]
    private GameObject confirmationView;

    [SerializeField]
    private GameObject lowMoneyView;

    [SerializeField]
    private GameObject unselectedAgentView;

    // Start is called before the first frame update
    void Start()
    {
        selectedAgentId = -1;
        usableEquipment = new List<int>();
        usableEquipment.Add(0);
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

        childList = equipmentScrollContent.GetComponentsInChildren<Transform>();

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
        selectedInfoId = -1;
        prePerson = true;
        ResetOddScreen();
        equipmentBef = 0;
        equipmentSuccess = 0;
        equipmentAft = 0;
        equipmentEscape = 0;
        ChangeCostText();
    }

    public void ChangeCostText()
    {
        costText.text = $"$ {actionCost}";
    }

    //선택된 도시의 정보를 DeployView에 반영한다.
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

        equipmentList = new List<bool>();
        for(int i = 0;i < EquipmentManager.count;i++)
        {
            equipmentList.Add(false);
            
            if(usableEquipment.Contains(i) == false)
            {
                continue;
            }

            id = i;
            GameObject tmp = Instantiate(equipmentPrefab, equipmentScrollContent); // 프리팹 생성
            Button[] buttons = tmp.GetComponentsInChildren<Button>();
            int tmpId = id;
            buttons[0].onClick.AddListener(() => ClickInformationButton(false, tmpId, buttons[0]));
            buttons[0].GetComponentInChildren<TextMeshProUGUI>().text = EquipmentManager.equipmentList[id].name;
            buttons[1].onClick.AddListener(() => ChooseEquipment(tmpId, buttons[1])); // 프리팹에 버튼 함수 할당
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
        }
        if(selectedAgentId != -1)
        {
            CalculateByAgent();
            UpdateOddScreen();
        }
    }

    //장비 버튼이 눌릴 경우 호출되는 함수
    public void ChooseEquipment(int id, Button self)
    {
        if(equipmentList[id])
        {
            equipmentList[id] = false;
            self.GetComponentInChildren<Image>().color = Color.white;
            equipmentBef -= EquipmentManager.equipmentList[id].bef;
            equipmentSuccess -= EquipmentManager.equipmentList[id].success;
            equipmentAft -= EquipmentManager.equipmentList[id].aft;
            equipmentEscape -= EquipmentManager.equipmentList[id].escape;
            actionCost -= EquipmentManager.equipmentList[id].cost;
            ChangeCostText();
        }
        else
        {
            equipmentList[id] = true;
            self.GetComponentInChildren<Image>().color = Color.green;
            equipmentBef += EquipmentManager.equipmentList[id].bef;
            equipmentSuccess += EquipmentManager.equipmentList[id].success;
            equipmentAft += EquipmentManager.equipmentList[id].aft;
            equipmentEscape += EquipmentManager.equipmentList[id].escape;
            actionCost += EquipmentManager.equipmentList[id].cost;
            ChangeCostText();
        }
        if(selectedAgentId != -1)
        {
            UpdateOddScreen();
        }
    }

    public void CalculateByAgent()
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

    public void CalculateByCity()
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

    public void ResetOddScreen()
    {
        oddTexts[1].text = "X";
        oddTexts[3].text = "X";
        oddTexts[5].text = "X";
        oddTexts[7].text = "X";
    }

    public void UpdateOddScreen()
    {
        bef = agentBef + equipmentBef + cityBef;
        success = agentSuccess + equipmentSuccess + citySuccess;
        aft = agentAft + equipmentAft + cityAft;
        escape = agentEscape + equipmentEscape + cityEscape;
        
        oddTexts[1].text = bef.ToString() + "%";
        oddTexts[3].text = success.ToString() + "%";
        oddTexts[5].text = aft.ToString() + "%";
        oddTexts[7].text = escape.ToString() + "%";

        befReal = agentBefReal + equipmentBef + cityBef;
        successReal = agentSuccessReal + equipmentSuccess + citySuccess;
        aftReal = agentAftReal + equipmentAft + cityAft;
        escapeReal = agentEscapeReal + equipmentEscape + cityEscape;
    }

    public void Execute()
    {
        if(selectedAgentId < 0)
        {
            unselectedAgentView.gameObject.SetActive(true);
        }
        else if(ResourceManager.money < actionCost)
        {
            lowMoneyView.gameObject.SetActive(true);
        }
        else
        {
            Action newAction = new Action(1, selectedAgentId, baseCity.id, selectedCity.id, 0, equipmentList, befReal, successReal, aftReal, escapeReal);//1은 Deploy의 type이다.
            ActionManager.AddAction(newAction);
            PersonManager.ChangeStatus(selectedAgentId, 1);
            ResourceManager.ChangeMoney(ResourceManager.money - actionCost);

            confirmationView.gameObject.SetActive(true);
        }
    }
    
    public void HideConfirmationView()
    {
        confirmationView.gameObject.SetActive(false);
    }
}
