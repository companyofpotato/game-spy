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

    static List<int> emptyPersonList = new List<int>(new int[] {-1});

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
        cityList.Add(new City(0, "base", -1, -1, -1, emptyPersonList));
        cityList.Add(new City(1, "alpha", 0, 0, 0, emptyPersonList));
        cityList.Add(new City(2, "bravo", 0, 0, 0, emptyPersonList));
        cityList.Add(new City(3, "charlie", 0, 0, 0, emptyPersonList));
        cityList.Add(new City(4, "delta", 0, 0, 0, emptyPersonList));
        cityList.Add(new City(5, "echo", 0, 0, 0, emptyPersonList));
        cityList.Add(new City(6, "foxtrot", 0, 0, 0, emptyPersonList));
        cityList.Add(new City(7, "golf", 0, 0, 0, emptyPersonList));

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static City GetCityInfo(int cityNumber)
    {
        return cityList[cityNumber];
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

    public static void ChangeCityInfo(int cityNumber, City changedCity)
    {

    }

    public static void ChangeCityId(int cityNumber, int id)
    {
        cityList[cityNumber].ChangeId(id);
    }

    public static void ChangeCityName(int cityNumber, string name)
    {
        cityList[cityNumber].ChangeName(name);
    }

    public static void ChangeCityType(int cityNumber, int type)
    {
        cityList[cityNumber].ChangeType(type);
    }

    public static void ChangeCityBuildings(int cityNumber, int buildings)
    {
        cityList[cityNumber].ChangeBuildings(buildings);
    }

    public static void ChangeCityTraits(int cityNumber, int traits)
    {
        cityList[cityNumber].ChangeTraits(traits);
    }

    public static void ChangeCityPersonList(int cityNumber, List<int> personList)
    {
        cityList[cityNumber].ChangePersonList(personList);
    }
}
