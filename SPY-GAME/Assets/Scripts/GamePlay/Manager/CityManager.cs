using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/*
- city
0 : 플레이어의 본부. 지도에 표시되지는 않는다.
7 : 적의 본부. 지도에 표시되지는 않는다.
*/

public class CityManager : MonoBehaviour
{
    private static CityManager currentInstance;

    public static List<City> cityList {get; private set;}

    static List<int> emptyPersonList = new List<int>(new int[] {});

    public static int buildingCounts {get; private set;}

    // 싱글톤 접근용 프로퍼티
    public static CityManager instance
    {
        get
        {
            // 만약 싱글톤 변수에 아직 오브젝트가 할당되지 않았다면
            if (currentInstance == null)
            {
                // 씬에서 CityManager 오브젝트를 찾아 할당
                currentInstance = FindObjectOfType<CityManager>();
            }

            // 싱글톤 오브젝트를 반환
            return currentInstance;
        }
    }

    private void Awake() {
        // 씬에 싱글톤 오브젝트가 된 다른 CityManager 오브젝트가 있다면
        if (instance != this)
        {
            // 자신을 파괴
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        cityList = new List<City>();
        cityList.Add(new City(0, "Resistance Base", -1, -1, -1, emptyPersonList));
        cityList.Add(new City(1, "Alpha", 0, 65, 0, emptyPersonList));
        cityList.Add(new City(2, "Bravo", 0, 4, 0, emptyPersonList));
        cityList.Add(new City(3, "Charlie", 0, 35, 0, emptyPersonList));
        cityList.Add(new City(4, "Delta", 0, 24 + 128 + 256, 0, emptyPersonList));
        cityList.Add(new City(5, "Echo", 0, 32 + 512, 0, emptyPersonList));
        cityList.Add(new City(6, "Foxtrot", 0, 69, 0, emptyPersonList));
        cityList.Add(new City(7, "Golf", 0, 9 + 256 + 512, 0, emptyPersonList));
        cityList.Add(new City(8, "Hotel", 0, 16 + 32 + 128, 0, emptyPersonList));
        cityList.Add(new City(9, "Empire Base", -1, -1, -1, emptyPersonList));

        buildingCounts = 10;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static City GetCityInfo(int id)
    {
        return cityList[id];
    }

    public static void ResetPersonListOfCity()
    {
        for(int i = 0;i < 8;i++)
        {
            var tmpQuery =
            from person in PersonManager.personList
            where person.location == i
            select person.id;

            List<int> tmpList = tmpQuery.ToList();
            ChangeCityPersonList(i, tmpList);

/*
            foreach(var item in tmpList)
            {
                Debug.Log(PersonManager.personList[item].codename + PersonManager.personList[item].status);
            }
*/
        }
    }

    public static void ChangeCityInfo(int id, City changedCity)
    {

    }

    public static void ChangeCityName(int id, string name)
    {
        cityList[id].ChangeName(name);
    }

    public static void ChangeCityType(int id, int type)
    {
        cityList[id].ChangeType(type);
    }

    public static void ChangeCityBuildings(int id, int buildings)
    {
        cityList[id].ChangeBuildings(buildings);
    }

    public static void ChangeCityTraits(int id, int traits)
    {
        cityList[id].ChangeTraits(traits);
    }

    public static void ChangeCityPersonList(int id, List<int> personList)
    {
        cityList[id].ChangePersonList(personList);
    }

    public static void AddAction(int id, Action newAction)
    {
        cityList[id].AddAction(newAction);
    }

    public static void AddReport(int id, string reportText)
    {
        cityList[id].AddReport(reportText);
    }

    public static string GetReportText(int id)
    {
        return cityList[id].reportText;
    }
}
