using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EventManager : MonoBehaviour
{
    private static EventManager currentInstance;

    private static List<Action> actionList;

    private static List<List<Action>> actionListList = new List<List<Action>>();
    private static List<string> reportListList = new List<string>();

    private static List<string> reportList;

    private static int currentTurn;

    [SerializeField]
    private GameObject reportView;

    [SerializeField]
    private TextMeshProUGUI reportText;

    private static int escapeWound = 25;
    private static int escapeDeath = 5;
    private static int escapeMin = 1;
    private static int escapeMax = 4;

    private static int pistolWound = 60;
    //private static int pistolDeath = 40;
    private static int pistolMin = 4;
    private static int pistolMax = 6;
    private static int sniperRifleWound = 30;
    //private static int sniperRifleDeath = 70;
    private static int sniperRifleMin = 6;
    private static int sniperRifleMax = 10;
    private static int smallBombWound = 70;
    //private static int smallBombDeath = 30;
    private static int smallBombMin = 2;
    private static int smallBombMax = 6;
    private static int bigBombWound = 10;
    //private static int bigBombDeath = 90;
    private static int bigBombMin = 8;
    private static int bigBombMax = 12;

    // 싱글톤 접근용 프로퍼티
    public static EventManager instance
    {
        get
        {
            // 만약 싱글톤 변수에 아직 오브젝트가 할당되지 않았다면
            if (currentInstance == null)
            {
                // 씬에서 EventManager 오브젝트를 찾아 할당
                currentInstance = FindObjectOfType<EventManager>();
            }

            // 싱글톤 오브젝트를 반환
            return currentInstance;
        }
    }

    private void Awake() {
        // 씬에 싱글톤 오브젝트가 된 다른 EventManager 오브젝트가 있다면
        if (instance != this)
        {
            // 자신을 파괴
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        actionList = new List<Action>();
        currentTurn = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void AddAction(Action newAction)
    {
        actionList.Add(newAction);
    }

    public static void ExecuteActions()
    {
        reportList = new List<string>();

        foreach(var action in actionList)
        {
            if(MakeRandomNumber() < action.bef)
            {
                //시작 전에 발각된다면
                reportList.Add($"Codename {PersonManager.personList[action.who].codename} has been compromised before action.");

                if(MakeRandomNumber() < action.escape)
                {
                    //시작 전에 탈출에 성공한다면
                    EscapeSuccess(action.who);
                }
                else
                {
                    //시작 전에 탈출에 실패한다면
                    EscapeFail(action.who);
                }
            }
            else
            {
                //발각되지 않는다면
                if(MakeRandomNumber() < action.success)
                {
                    //임무가 성공한다면
                    switch(action.type)
                    {
                        case 0 : ActionInvestigateRegion(action); break;
                        case 1 : ActionDeploySuccess(action); break;
                        case 2 : ActionWithdrawalSuccess(action); break;
                        case 3 : ActionAssassinationSuccess(action); break;
                        case 4 : ActionPersuasion(action); break;
                        default : Debug.Log("ERROR:There is no type of this action:" + action.type); break;
                    }
                }
                else
                {
                    //임무가 실패한다면
                    ActionFailed(action);
                }

                if(MakeRandomNumber() < action.aft)
                {
                    //임무 후에 발각된다면
                    reportList.Add($"Codename {PersonManager.personList[action.who].codename} has been compromised after action.");

                    if(MakeRandomNumber() < action.escape)
                    {
                        //임무 후에 탈출에 성공한다면
                        EscapeSuccess(action.who);
                    }
                    else
                    {
                        //임무 후에 탈출에 실패한다면
                        EscapeFail(action.who);
                    }
                }
            }
            PersonManager.MakeAvailable(action.who);
            reportList.Add("\n");
        }

        actionListList.Add(actionList);
        actionList = new List<Action>();
    }

    public static int MakeRandomNumber()
    {
        int random_number = Random.Range(0, 100);
        //Debug.Log(random_number);
        return random_number;
    }

    public static int MakeRandomNumber(int min, int max)
    {
        int random_number = Random.Range(min, max + 1);
        return random_number;
    }

    public static void ActionFailed(Action action)
    {
        Debug.Log("Mission Failed");
        reportList.Add($"Codename {PersonManager.personList[action.who].codename} has failed action.");
    }

    public static void ActionInvestigateRegion(Action action)
    {
        
    }

    public static void ActionDeploySuccess(Action action)
    {
        Debug.Log("Deploy Success");
        PersonManager.ChangeCity(action.who, action.target);
        reportList.Add($"Codename {PersonManager.personList[action.who].codename} is deployed in {CityManager.cityList[action.target].name}");
    }

    public static void ActionWithdrawalSuccess(Action action)
    {
        Debug.Log("Withdrawal Success");
        PersonManager.ChangeCity(action.who, action.target);
        reportList.Add($"Codename {PersonManager.personList[action.who].codename} is withdrawn from {CityManager.cityList[action.where].name}");
    }

    public static void ActionAssassinationSuccess(Action action)
    {
        Debug.Log("Assassination Success");
        
        int wound = -1;
        switch(action.how)
        {
            case 1 : if(MakeRandomNumber() < pistolWound) wound = MakeRandomNumber(pistolMin, pistolMax); break;
            case 2 : if(MakeRandomNumber() < sniperRifleWound) wound = MakeRandomNumber(sniperRifleMin, sniperRifleMax); break;
            case 3 : if(MakeRandomNumber() < smallBombWound) wound = MakeRandomNumber(smallBombMin, smallBombMax); break;
            case 4 : if(MakeRandomNumber() < bigBombWound) wound = MakeRandomNumber(bigBombMin, bigBombMax); break;
            default : Debug.Log("ERROR:EventManager.ActionAssassinationSuccess(), No Matched method number!"); break;
        }
        
        if(wound > 0)
        {
            PersonManager.Wound(action.target, wound);
            reportList.Add($"Target {PersonManager.personList[action.target].codename} is wounded for {wound} turn by codename {PersonManager.personList[action.who].codename}");
        }
        else
        {
            PersonManager.Die(action.target);
            reportList.Add($"Codename {PersonManager.personList[action.who].codename} has assassinated codename {PersonManager.personList[action.target].codename}");
        } 
    }

    public static void ActionPersuasion(Action action)
    {
        Debug.Log("Persuasion Success");

        if(PersonManager.CheckTrait(action.target, 0))
        {
            PersonManager.ChangeBelong(action.target, 2);
        }
        else if(PersonManager.personList[action.target].belong == 0)
        {
            PersonManager.ChangeBelong(action.target, 1);
        }
        else if(PersonManager.personList[action.target].belong == -1)
        {
            PersonManager.ChangeBelong(action.target, -2);
        }
        reportList.Add($"Codename {PersonManager.personList[action.who].codename} has persuaded codename {PersonManager.personList[action.target].codename}");
    }

    public static void EscapeSuccess(int id)
    {
        int number = MakeRandomNumber();
        if(number < escapeDeath)
        {
            PersonManager.Die(id);
            reportList.Add($"Codename {PersonManager.personList[id].codename} is died during escape.");
        }
        else if(number < escapeWound)
        {
            int wound = MakeRandomNumber(escapeMin, escapeMax);
            PersonManager.Wound(id, wound);
            reportList.Add($"Codename {PersonManager.personList[id].codename} is wounded for {wound} turn during escape.");
        }
        else
        {
            reportList.Add($"Codename {PersonManager.personList[id].codename} is escaped.");
        }
    }

    public static void EscapeFail(int id)
    {
        PersonManager.Captured(id);
        reportList.Add($"Codename {PersonManager.personList[id].codename} is failed to escape.");
    }

    public static void EndTurn()
    {
        RecoverWoundedPerson();
        ExecuteActions();
        AddEnemyAction();
        ExecuteEnemyAction();
        StartTurn();
    }

    public static void RecoverWoundedPerson()
    {

    }

    public static void AddEnemyAction()
    {
        
    }

    public static void ExecuteEnemyAction()
    {
        
    }

    public static void StartTurn()
    {
        CityManager.ResetPersonListOfCity();
        currentTurn++;
    }

    public void MakeReport()
    {
        reportText.text = "Current Turn is " + currentTurn + "\n";
        foreach(var text in reportList)
        {
            reportText.text += text;
            reportText.text += "\n";
        }
        reportListList.Add(reportText.text);
    }

    public void ShowReportView()
    {
        reportView.gameObject.SetActive(true);
    }

    public void HideReportView()
    {
        reportView.gameObject.SetActive(false);
    }
}
