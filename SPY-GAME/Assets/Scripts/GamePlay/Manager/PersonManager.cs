using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PersonManager : MonoBehaviour
{
    private static PersonManager currentInstance;

    public static List<Person> personList {get; private set;}

    // 싱글톤 접근용 프로퍼티
    public static PersonManager instance
    {
        get
        {
            // 만약 싱글톤 변수에 아직 오브젝트가 할당되지 않았다면
            if (currentInstance == null)
            {
                // 씬에서 PersonManager 오브젝트를 찾아 할당
                currentInstance = FindObjectOfType<PersonManager>();
            }

            // 싱글톤 오브젝트를 반환
            return currentInstance;
        }
    }

    private void Awake() {
        // 씬에 싱글톤 오브젝트가 된 다른 PersonManager 오브젝트가 있다면
        if (instance != this)
        {
            // 자신을 파괴
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        int num = 0;
        for(int i = 0;i < 18;i++)
        {
            num += (int)Math.Pow(2, i);
        }
        Debug.Log(num);

        personList = new List<Person>();

        personList.Add(new Person(0, false, "Alfred", 262143, 0, "", "", 42, true, false, true, 55, 0, -3, 90, 3, 73, 6, 2, 86, 8, 29, 43, 0, 0));
        personList.Add(new Person(1, true, "Ben", 262143, 0, "", "", 46, true, true, false, 60, 0, -2, 66, 1, 26, 1, 39, 56, 19, 77, 99, 0, 0));
        personList.Add(new Person(2, true, "Carla", 262143, 0, "", "", 39, false, true, true, 51, 0, -1, 62, 7, 67, 2, 98, 49, 98, 44, 97, 0, 0));
        personList.Add(new Person(3, true, "Diana", 262143, 3, "", "", 33, false, false, true, 67, 0, 1, 79, 0, 57, 6, 86, 17, 7, 64, 75, 0, 0));
        personList.Add(new Person(4, false, "Eric", 262143, 0, "", "", 35, true, false, true, 55, 0, 0, 42, 1, 53, 8, 26, 51, 13, 84, 78, 0, 0));
        personList.Add(new Person(5, false, "Francis", 262143, 0, "", "", 32, true, false, true, 68, 0, 2, 70, 2, 44, 7, 13, 15, 18, 51, 89, 0, 0));
        personList.Add(new Person(6, true, "Glen", 262143, 0, "", "", 25, true, true, true, 48, -5, 3, 66, 0, 64, 10, 37, 81, 90, 70, 93, 0, 0));
        personList.Add(new Person(7, true, "Helena", 262143, 0, "", "", 36, false, false, true, 78, -5, -3, 92, 0, 59, 9, 53, 64, 86, 11, 86, 0, 0));
        personList.Add(new Person(8, false, "Isabel", 262143, 0, "", "", 35, false, false, true, 58, 0, -2, 87, 3, 41, 2, 87, 56, 1, 28, 8, 0, 0));
        personList.Add(new Person(9, true, "Jack", 262143, 0, "", "", 20, true, false, true, 42, 0, -1, 57, 2, 14, 4, 47, 54, 78, 22, 40, 0, 0));
        personList.Add(new Person(10, false, "Kate", 262143, 0, "", "", 29, false, true, false, 44, 0, 0, 52, 4, 8, 2, 33, 99, 38, 99, 99, 0, 0));
        personList.Add(new Person(11, true, "Lily", 262143, 0, "", "", 23, false, true, true, 77, -13, 1, 83, 5, 90, 5, 29, 100, 77, 35, 5, 0, 0));
        personList.Add(new Person(12, true, "Mario", 262143, 0, "", "", 25, true, false, true, 59, -14, 2, 97, 9, 75, 6, 77, 1, 11, 44, 47, 0, 0));
        personList.Add(new Person(13, true, "Noah", 262143, 0, "", "", 45, true, true, false, 60, 0, 3, 86, 4, 65, 4, 95, 6, 50, 77, 77, 0, 0));
        personList.Add(new Person(14, true, "Owen", 262143, 0, "", "", 23, true, false, true, 53, 0, -3, 45, 0, 61, 6, 31, 77, 35, 39, 27, 0, 0));
        personList.Add(new Person(15, false, "Paul", 262143, 0, "", "", 44, true, false, true, 47, 0, -2, 46, 5, 52, 8, 45, 83, 100, 72, 68, 0, 0));
        personList.Add(new Person(16, false, "Quinn", 262143, 0, "", "", 34, false, false, true, 70, 0, -1, 73, 6, 37, 4, 56, 54, 1, 81, 51, 0, 0));
        personList.Add(new Person(17, true, "Rachel", 262143, 0, "", "", 50, false, false, true, 71, 0, 1, 62, 5, 13, 9, 55, 66, 64, 59, 76, 0, 0));
        personList.Add(new Person(18, true, "Sadie", 262143, 0, "", "", 33, false, false, true, 79, 0, 2, 93, 6, 23, 2, 81, 97, 16, 98, 7, 0, 0));
        personList.Add(new Person(19, true, "Toby", 262143, 0, "", "", 32, true, true, false, 42, 0, 3, 43, 0, 43, 7, 1, 68, 96, 21, 17, 0, 0));
        personList.Add(new Person(20, false, "Ulysses", 0, 0, "", "", 41, true, false, true, 49, 0, 0, 71, 6, 68, 2, 14, 100, 65, 95, 8, 0, 0));
        personList.Add(new Person(21, true, "Veronica", 0, 0, "", "", 42, false, true, true, 60, 0, 1, 94, 0, 4, 8, 82, 55, 71, 43, 14, 0, 0));
        personList.Add(new Person(22, false, "Walter", 0, 0, "", "", 33, true, false, true, 48, 0, 0, 66, 3, 39, 2, 73, 39, 28, 62, 18, 0, 0));
        personList.Add(new Person(23, true, "Xavier", 0, 0, "", "", 42, true, true, false, 73, -14, -1, 77, 0, 15, 2, 47, 30, 40, 56, 41, 0, 0));
        personList.Add(new Person(24, true, "Yosef", 0, 0, "", "", 33, true, false, true, 47, 0, -2, 70, 3, 72, 6, 75, 94, 3, 12, 19, 0, 0));
        personList.Add(new Person(25, true, "Zoey", 0, 0, "", "", 30, false, false, true, 76, -13, 1, 95, 4, 10, 5, 96, 28, 42, 45, 43, 0, 0));
        
        CityManager.ResetPersonListOfCity();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static bool CheckSexualMatch(int actor, int target)
    {
        bool s1 = personList[actor].gender, s2 = personList[target].gender;

        if(s1 == s2 && personList[target].sexualHomo)
            return true;

        if(s1 != s2 && personList[target].sexualHetero)
            return true;
        
        return false;
    }

    public static bool CheckAgent(int id)
    {
        return (personList[id].isAgent == true);
    }

    public static bool CheckAvailable(int id)
    {
        return (personList[id].status == 0);
    }

    public static bool CheckFriendly(int id)
    {
        return (personList[id].belong > 0);
    }

    public static bool CheckPersuadable(int id)
    {
        return (personList[id].belong == 0 || personList[id].belong == -1);
    }

    public static bool CheckTargeted(int id)
    {
        return (personList[id].isTargeted != 0);
    }

    public static bool CheckTrait(int id, int check)
    {
        return ((personList[id].trait & (1 << check)) > 0);
    }

    public static bool CheckTraitReal(int id, int check)
    {
        return ((personList[id].traitReal & (1 << check)) > 0);
    }

    public static bool CheckPerk(int id, int check)
    {
        return ((personList[id].perk & (1 << check)) > 0);
    }

    public static bool CheckPerkReal(int id, int check)
    {
        return ((personList[id].perkReal & (1 << check)) > 0);
    }

    public static void ChangeStatus(int id, int after)
    {
        personList[id].ChangeStatus(after);
    }

    public static void MakeAvailable(int id)
    {
        if(personList[id].status > 0)
            personList[id].ChangeStatus(0);
    }

    public static void Die(int id)
    {
        personList[id].ChangeStatus(-13);
    }

    public static void Wound(int id, int term)
    {
        term *= -1;
        personList[id].ChangeStatus(term);
    }

    public static void Captured(int id)
    {
        personList[id].ChangeStatus(-14);
    }

    public static void Missed(int id)
    {
        personList[id].ChangeStatus(-15);
    }

    public static void ChangeCity(int id, int after)
    {
        personList[id].ChangeCity(after);
    }

    public static void ChangeBelong(int id, int after)
    {
        personList[id].ChangeBelong(after);
    }

    public static void ChangeIsTargeted(int id, int after)
    {
        personList[id].ChangeIsTargeted(after);
    }

    public static void AddTrait(int id, int add)
    {
        personList[id].AddTrait(add);
    }

    public static void RemoveTrait(int id, int remove)
    {
        personList[id].RemoveTrait(remove);
    }

    public static void AddAction(int id, Action newAction)
    {
        personList[id].AddAction(newAction);
    }

    public static void AddReport(int id, string reportText)
    {
        personList[id].AddReport(reportText);
    }

    public static void AddDoubleAction(int id, Action newAction)
    {
        personList[id].AddDoubleAction(newAction);
    }

    public static List<string> GetReportList(int id)
    {
        return personList[id].reportList;
    }
}
