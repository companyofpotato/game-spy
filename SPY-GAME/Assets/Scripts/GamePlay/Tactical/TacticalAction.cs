using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public abstract class TacticalAction : MonoBehaviour
{
    public City baseCity {get; protected set;}
    public City selectedCity {get; protected set;}

    public int selectedAgentId {get; protected set;}
    public Person selectedAgent {get; protected set;}
    public int selectedTargetId {get; protected set;}
    public Person selectedTarget {get; protected set;}
    protected Person shownPerson;

    public Equipment selectedEquipment {get; protected set;}//지금 당장은 안 쓰지만 두자.
    public int selectedMethodId {get; protected set;}
    public Equipment selectedMethod {get; protected set;}
    protected Equipment shownEquipment;

    public List<bool> equipmentList {get; protected set;}
    protected List<int> usableMethod;
    protected List<int> usableEquipment;

    protected int bef = 0;
    protected int success = 0;
    protected int aft = 0;
    protected int escape = 0;

    protected int agentBef = 0;
    protected int agentSuccess = 0;
    protected int agentAft = 0;
    protected int agentEscape = 0;

    protected int targetBef = 0;
    protected int targetSuccess = 0;
    protected int targetAft = 0;
    protected int targetEscape = 0;

    protected int methodBef = 0;
    protected int methodSuccess = 0;
    protected int methodAft = 0;
    protected int methodEscape = 0;

    protected int equipmentBef = 0;
    protected int equipmentSuccess = 0;
    protected int equipmentAft = 0;
    protected int equipmentEscape = 0;

    protected int cityBef = 0;
    protected int citySuccess = 0;
    protected int cityAft = 0;
    protected int cityEscape = 0;

    protected int befReal = 0;
    protected int successReal = 0;
    protected int aftReal = 0;
    protected int escapeReal = 0;

    protected int agentBefReal = 0;
    protected int agentSuccessReal = 0;
    protected int agentAftReal = 0;
    protected int agentEscapeReal = 0;

    protected int targetBefReal = 0;
    protected int targetSuccessReal = 0;
    protected int targetAftReal = 0;
    protected int targetEscapeReal = 0;

    protected int methodBefReal = 0;
    protected int methodSuccessReal = 0;
    protected int methodAftReal = 0;
    protected int methodEscapeReal = 0;

    protected int moneySuccess = 0;
    protected int moneySuccessReal = 0;

    private Button previouslySelectedAgentButton;
    private Button previouslySelectedTargetButton;
    private Button previouslySelectedMethodButton;

    protected TextMeshProUGUI[] oddTexts;
    protected TextMeshProUGUI[] personTexts;
    protected TextMeshProUGUI[] equipmentTexts;

    private InformationScreen informationScreenInstance;
    private int selectedInfoId;
    private bool prePerson;
    private Button previouslySelectedInfoButton;

    protected int actionCost;

    protected GamePlayUIManager gamePlayUIManager;

    [SerializeField]
    protected GameObject GamePlayUIManagerObject;

    [SerializeField]
    protected int originalCost = 0;

    [SerializeField]
    protected Transform agentScroll;

    [SerializeField]
    protected Transform agentScrollContent;

    [SerializeField]
    protected Transform targetScroll;

    [SerializeField]
    protected Transform targetScrollContent;

    [SerializeField]
    protected Transform methodScroll;

    [SerializeField]
    protected Transform methodScrollContent;

    [SerializeField]
    protected Transform equipmentScroll;

    [SerializeField]
    protected Transform equipmentScrollContent;

    [SerializeField]
    protected GameObject agentPrefab;

    [SerializeField]
    protected GameObject targetPrefab;

    [SerializeField]
    protected GameObject methodPrefab;

    [SerializeField]
    protected GameObject equipmentPrefab;

    [SerializeField]
    private GameObject informationScreen;

    [SerializeField]
    protected GameObject oddInformation;

    [SerializeField]
    protected TextMeshProUGUI costText;

    [SerializeField]
    protected TMP_InputField moneyText;

    [SerializeField]
    protected GameObject confirmationView;

    public int type;
    // Start is called before the first frame update
    protected void Start()
    {
        oddTexts = oddInformation.GetComponentsInChildren<TextMeshProUGUI>();
        informationScreenInstance = informationScreen.GetComponent<InformationScreen>();
        gamePlayUIManager = GamePlayUIManagerObject.GetComponent<GamePlayUIManager>();
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

        if(targetScrollContent != null)
        {
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

        if(methodScrollContent != null)
        {
            childList = methodScrollContent.GetComponentsInChildren<Transform>();

            if(childList != null)
            {
                childCount = childList.Length;
                for(int i = 1;i < childCount;i++)
                {
                    Destroy(childList[i].gameObject);
                }
            }
        }

        if(equipmentScrollContent != null)
        {
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
    }

    public virtual void ResetVariables()
    {
        selectedAgentId = -1;
        selectedTargetId = -1;
        selectedMethodId = -1;
        selectedInfoId = -1;
        prePerson = true;
        ResetOddScreen();
        equipmentBef = 0;
        equipmentSuccess = 0;
        equipmentAft = 0;
        equipmentEscape = 0;
        actionCost = originalCost;
        ChangeCostText();
    }

    public void ChangeCostText()
    {
        costText.text = $"$ {actionCost}";
    }

    public void ClickInformationButton(bool person, int id, Button self)
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

    public virtual void ChooseAgent(int id, Button self)
    {
        if(selectedAgentId < 0)
        {
            selectedAgentId = id;
            previouslySelectedAgentButton = self;
            selectedAgent = PersonManager.personList[id];
            self.GetComponentInChildren<Image>().color = Color.green;
            CalculateByAgent();
        }
        else if(selectedAgentId != id)
        {
            previouslySelectedAgentButton.GetComponentInChildren<Image>().color = Color.white;
            selectedAgentId = id;
            previouslySelectedAgentButton = self;
            selectedAgent = PersonManager.personList[id];
            self.GetComponentInChildren<Image>().color = Color.green;
            CalculateByAgent();
        }
    }

    public virtual void ChooseTarget(int id, Button self)
    {
        if(selectedTargetId < 0)
        {
            selectedTargetId = id;
            previouslySelectedTargetButton = self;
            selectedTarget = PersonManager.personList[id];
            self.GetComponentInChildren<Image>().color = Color.green;
            CalculateByTarget();
        }
        else if(selectedTargetId != id)
        {
            previouslySelectedTargetButton.GetComponentInChildren<Image>().color = Color.white;
            selectedTargetId = id;
            previouslySelectedTargetButton = self;
            selectedTarget = PersonManager.personList[id];
            self.GetComponentInChildren<Image>().color = Color.green;
            CalculateByTarget();
        }
    }

    public virtual void ChooseMethod(int id, Button self)
    {
        if(selectedMethodId < 0)
        {
            selectedMethodId = id;
            equipmentList[id] = true;
            previouslySelectedMethodButton = self;
            selectedMethod = EquipmentManager.equipmentList[id];
            self.GetComponentInChildren<Image>().color = Color.green;
            actionCost += EquipmentManager.equipmentList[id].cost;
            ChangeCostText();
        }
        else if(selectedMethodId != id)
        {
            previouslySelectedMethodButton.GetComponentInChildren<Image>().color = Color.white;
            equipmentList[selectedMethodId] = false;
            actionCost -= EquipmentManager.equipmentList[selectedMethodId].cost;

            selectedMethod = EquipmentManager.equipmentList[id];
            selectedMethodId = id;
            previouslySelectedMethodButton = self;
            equipmentList[id] = true;
            self.GetComponentInChildren<Image>().color = Color.green;
            actionCost += EquipmentManager.equipmentList[id].cost;
            ChangeCostText();
        }
    }

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

    public void ResetOddScreen()
    {
        oddTexts[1].text = "X";
        oddTexts[3].text = "X";
        oddTexts[5].text = "X";
        oddTexts[7].text = "X";
    }

    public void UpdateOddScreen()
    {
        bef = agentBef + equipmentBef + cityBef + methodBef + targetBef;
        success = agentSuccess + equipmentSuccess + citySuccess + methodSuccess + targetSuccess + moneySuccess;
        aft = agentAft + equipmentAft + cityAft + methodAft + targetAft;
        escape = agentEscape + equipmentEscape + cityEscape + methodEscape + targetEscape;
        
        oddTexts[1].text = bef.ToString() + "%";
        oddTexts[3].text = success.ToString() + "%";
        oddTexts[5].text = aft.ToString() + "%";
        oddTexts[7].text = escape.ToString() + "%";

        befReal = agentBefReal + equipmentBef + cityBef + methodBefReal + targetBefReal;
        successReal = agentSuccessReal + equipmentSuccess + citySuccess + methodSuccessReal + targetSuccessReal + moneySuccessReal;
        aftReal = agentAftReal + equipmentAft + cityAft + methodAftReal + targetAftReal;
        escapeReal = agentEscapeReal + equipmentEscape + cityEscape + methodEscapeReal + targetEscapeReal;
    }

    public void Execute()
    {
        if(selectedAgentId < 0)
        {
            gamePlayUIManager.ShowUnselectedAgentView();
        }
        else if(ResourceManager.money < actionCost)
        {
            gamePlayUIManager.ShowLowMoneyView();
        }
        else if(selectedMethodId < 0)
        {
            gamePlayUIManager.ShowUnselectedMethodView();
        }
        else if(selectedTargetId < 0)
        {
            gamePlayUIManager.ShowUnselectedTargetView();
        }
        else
        {
            Action newAction = MakeAction();
            ActionManager.AddAction(newAction);
            PersonManager.ChangeStatus(selectedAgentId, type);
            ResourceManager.ChangeMoney(ResourceManager.money - actionCost);

            confirmationView.gameObject.SetActive(true);
        }
    }
    
    public void HideConfirmationView()
    {
        confirmationView.gameObject.SetActive(false);
    }

    public abstract void ReflectCityInfo(City city1, City city2);

    public virtual void CalculateByAgent()
    {

    }

    public virtual void CalculateByTarget()
    {

    }

    public virtual void CalculateByMethod()
    {

    }

    public virtual void CalculateByCity()
    {

    }

    public abstract Action MakeAction();
}
