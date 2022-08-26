using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityManager : MonoBehaviour
{
    private static CityManager currentInstance;

    public static City[] cityList {get; private set;}

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
        cityList = new City[8];
        for(int i = 0;i < 8;i++)
        {
            cityList[i] = new City();
        }

        int[] tmp = {0, 1, 2, 3, 4, 5, 6, 7};

        cityList[0].SetNewCity(0, "base", -1, -1, -1, null, tmp);
        cityList[1].SetNewCity(1, "alpha", 0, 0, 0, null, null);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static City GetCityInfo(int cityNumber)
    {
        return cityList[cityNumber];
    }

    public void ChangeValue(int cityNumber, City changedCity)
    {

    }
}
