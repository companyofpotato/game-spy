using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ActionManager : MonoBehaviour
{
    private static ActionManager currentInstance;

    private static List<Action> actionList;
    private static List<List<Action>> actionListList;
    private static List<string> reportList;
    private static List<List<string>> reportListList;

    private static int currentTurn;

    [SerializeField]
    private GameObject reportView;

    [SerializeField]
    private Transform reportContent;

    [SerializeField]
    private GameObject reportPrefab;

    private static int escapeWound = 25;
    private static int escapeDeath = 5;
    private static int escapeMin = 1;
    private static int escapeMax = 4;

    private static int captured = 65;
    //private static int missing = 35;

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
    public static ActionManager instance
    {
        get
        {
            // 만약 싱글톤 변수에 아직 오브젝트가 할당되지 않았다면
            if (currentInstance == null)
            {
                // 씬에서 ActionManager 오브젝트를 찾아 할당
                currentInstance = FindObjectOfType<ActionManager>();
            }

            // 싱글톤 오브젝트를 반환
            return currentInstance;
        }
    }

    private void Awake() {
        // 씬에 싱글톤 오브젝트가 된 다른 ActionManager 오브젝트가 있다면
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
        actionListList = new List<List<Action>>();
        reportListList = new List<List<string>>();
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
            int success = -1, escape = -1;
            bool bef = false, aft = false;
            if(MakeRandomNumber() < action.befOdd)
            {
                //시작 전에 발각된다면
                bef = true;
                //Debug.Log($"Before");
                if(MakeRandomNumber() < action.escapeOdd)
                {
                    //시작 전에 탈출에 성공한다면
                    escape = EscapeSuccess(action.who);
                }
                else
                {
                    //시작 전에 탈출에 실패한다면
                    escape = EscapeFail(action.who);
                }
            }
            else
            {
                //발각되지 않는다면
                if(MakeRandomNumber() < action.successOdd)
                {
                    //임무가 성공한다면
                    switch(action.what)
                    {
                        case 0 : success = ActionInvestigateRegion(action); break;
                        case 1 : success = ActionDeploySuccess(action); break;
                        case 2 : success = ActionWithdrawalSuccess(action); break;
                        case 3 : success = ActionAssassinationSuccess(action); break;
                        case 4 : success = ActionPersuasion(action); break;
                        default : Debug.Log("ERROR:There is no type of this action:" + action.what); break;
                    }
                }
                else
                {
                    //임무가 실패한다면
                    ActionFailed(action);
                    success = 0;
                }

                if(MakeRandomNumber() < action.aftOdd)
                {
                    //임무 후에 발각된다면
                    aft = true;
                    //Debug.Log("After");
                    if(MakeRandomNumber() < action.escapeOdd)
                    {
                        //임무 후에 탈출에 성공한다면
                        escape = EscapeSuccess(action.who);
                    }
                    else
                    {
                        //임무 후에 탈출에 실패한다면
                        escape = EscapeFail(action.who);
                    }
                }
            }
            PersonManager.MakeAvailable(action.who);

            action.Executed(currentTurn, bef, success, aft, escape);
            Debug.Log($"{bef}, {success}, {aft}, {escape}");

            PersonManager.AddAction(action.who, action);
            PersonManager.AddReport(action.who, action.MakeText(true, false));
            CityManager.AddAction(action.where, action);
            CityManager.AddReport(action.where, action.MakeText(true, true));

            reportList.Add(action.MakeText(false, true));
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
        //Debug.Log("Mission Failed");
    }

    public static int ActionInvestigateRegion(Action action)
    {
        return 1;
    }

    public static int ActionDeploySuccess(Action action)
    {
        //Debug.Log("Deploy Success");
        PersonManager.ChangeCity(action.who, action.whom);
        action.ChangeAftOdd(0);
        return 1;
    }

    public static int ActionWithdrawalSuccess(Action action)
    {
        //Debug.Log("Withdrawal Success");
        PersonManager.ChangeCity(action.who, action.whom);
        action.ChangeAftOdd(0);
        return 1;
    }

    public static int ActionAssassinationSuccess(Action action)
    {
        //Debug.Log("Assassination Success");
        
        int wound = -1;
        switch(action.how)
        {
            case 1 : if(MakeRandomNumber() < pistolWound) wound = MakeRandomNumber(pistolMin, pistolMax); break;
            case 2 : if(MakeRandomNumber() < sniperRifleWound) wound = MakeRandomNumber(sniperRifleMin, sniperRifleMax); break;
            case 3 : if(MakeRandomNumber() < smallBombWound) wound = MakeRandomNumber(smallBombMin, smallBombMax); break;
            case 4 : if(MakeRandomNumber() < bigBombWound) wound = MakeRandomNumber(bigBombMin, bigBombMax); break;
            default : Debug.Log("ERROR:ActionManager.ActionAssassinationSuccess(), No Matched method number!"); break;
        }
        
        if(wound > 0)
        {
            PersonManager.Wound(action.whom, wound);
            return wound;
        }
        else
        {
            PersonManager.Die(action.whom);
            return 13;
        } 
    }

    public static int ActionPersuasion(Action action)
    {
        //Debug.Log("Persuasion Success");

        if(PersonManager.CheckTrait(action.whom, 0))
        {
            PersonManager.ChangeBelong(action.whom, 2);
        }
        else if(PersonManager.personList[action.whom].belong == 0)
        {
            PersonManager.ChangeBelong(action.whom, 1);
        }
        else if(PersonManager.personList[action.whom].belong == -1)
        {
            PersonManager.ChangeBelong(action.whom, -2);
        }
        return 1;
    }

    public static int EscapeSuccess(int id)
    {
        //Debug.Log("Escape Success");
        int number = MakeRandomNumber();
        if(number < escapeDeath)
        {
            PersonManager.Die(id);
            return 13;
        }
        else if(number < escapeWound)
        {
            int wound = MakeRandomNumber(escapeMin, escapeMax);
            PersonManager.Wound(id, wound);
            return wound;
        }
        else
        {
            return 0;
        }
    }

    public static int EscapeFail(int id)
    {
        //Debug.Log("Escape Fail");
        if(MakeRandomNumber() < captured)
        {
            PersonManager.Captured(id);
            return 14;
        }
        else
        {
            PersonManager.Missed(id);
            return 15;
        }
    }

    public static void EndTurn()
    {
        RecoverWoundedPerson();
        ExecuteActions();
        AddEnemyAction();
        ExecuteEnemyAction();
        StartTurn();
        //MakeReport()
        //ShowReportView()
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
        TextMeshProUGUI tmpText = reportContent.GetComponentInChildren<TextMeshProUGUI>();
        tmpText.text = $"Report of Turn {currentTurn - 1}.";

        foreach(string item in reportList)
        {
            GameObject tmp = Instantiate(reportPrefab, reportContent);
            tmpText = tmp.GetComponentInChildren<TextMeshProUGUI>();
            tmpText.text = item;
        }

        reportListList.Add(reportList);
    }
}
