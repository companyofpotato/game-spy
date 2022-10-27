using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TacticalActionManager : MonoBehaviour
{
    private static TacticalActionManager currentInstance;

    public static int selectedCityNumber {get; private set;}
    public static City baseCity {get; private set;}
    public static City selectedCity {get; private set;}

    public InvestigateRegion investigateRegionInstance {get; private set;}
    public Deploy deployInstance {get; private set;}
    public Withdrawal withdrawalInstance {get; private set;}
    public Assassination assassinationInstance {get; private set;}
    public Persuasion persuasionInstance {get; private set;}

    private TextMeshProUGUI[] cityTexts;

    [SerializeField]
    private GameObject cityInformation;

    [SerializeField]
    private GameObject InvestigateRegionScript;

    [SerializeField]
    private GameObject DeployScript;

    [SerializeField]
    private GameObject WithdrawalScript;

    [SerializeField]
    private GameObject AssassinationScript;

    [SerializeField]
    private GameObject PersuasionScript;

    // 싱글톤 접근용 프로퍼티
    public static TacticalActionManager instance
    {
        get
        {
            // 만약 싱글톤 변수에 아직 오브젝트가 할당되지 않았다면
            if (currentInstance == null)
            {
                // 씬에서 TacticalActionManager 오브젝트를 찾아 할당
                currentInstance = FindObjectOfType<TacticalActionManager>();
            }

            // 싱글톤 오브젝트를 반환
            return currentInstance;
        }
    }

    private void Awake() {
        // 씬에 싱글톤 오브젝트가 된 다른 TacticalActionManager 오브젝트가 있다면
        if (instance != this)
        {
            // 자신을 파괴
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        selectedCityNumber = -1;
        deployInstance = DeployScript.GetComponent<Deploy>();
        investigateRegionInstance = InvestigateRegionScript.GetComponent<InvestigateRegion>();
        withdrawalInstance = WithdrawalScript.GetComponent<Withdrawal>();
        assassinationInstance = AssassinationScript.GetComponent<Assassination>();
        persuasionInstance = PersuasionScript.GetComponent<Persuasion>();

        cityTexts = cityInformation.GetComponentsInChildren<TextMeshProUGUI>();
        ResetCityInfo();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectCity(int cityNumber)
    {
        selectedCityNumber = cityNumber;

        baseCity = CityManager.GetCityInfo(0);
        selectedCity = CityManager.GetCityInfo(cityNumber);

        ReflectCityInfo();

        ShowCityInfo();
    }

    public void ResetSelectedCity()
    {
        selectedCityNumber = -1;
        ResetCityInfo();
    }

    public void RefreshSelectedCity()
    {
        SelectCity(selectedCityNumber);
    }

    public void ReflectCityInfo()
    {
        deployInstance.ReflectCityInfo(baseCity, selectedCity);
        withdrawalInstance.ReflectCityInfo(selectedCity);
        assassinationInstance.ReflectCityInfo(baseCity, selectedCity);
        persuasionInstance.ReflectCityInfo(baseCity, selectedCity);
    }

    public void ShowCityInfo()
    {
        cityTexts[1].text = selectedCity.name;
        switch(selectedCity.type)
        {
            case 0 : cityTexts[3].text = "Big City"; break;
            case 1 : cityTexts[3].text = "Middle City"; break;
            case 2 : cityTexts[3].text = "Small City"; break;
        }

        int key = 1, idx = 0, max = 1 << CityManager.buildingCounts;
        for(;key < max;)
        {
            if((selectedCity.buildings & key) > 0)
            {
                switch(idx)
                {
                    case 0 : cityTexts[5].text = "Agency"; break;
                    case 1 : cityTexts[5].text = "Factory"; break;
                    case 2 : cityTexts[5].text = "Lab"; break;
                    case 3 : cityTexts[5].text = "Harbor"; break;
                    case 4 : cityTexts[5].text = "Airport"; break;
                    case 5 : cityTexts[5].text = "Supply Depot"; break;
                    case 6 : cityTexts[5].text = "Army Base"; break;
                    case 7 : cityTexts[5].text = "Air Base"; break;
                    case 8 : cityTexts[5].text = "Naval Base"; break;
                    case 9 : cityTexts[5].text = "Communication Base"; break;
                }
                break;
            }
            key = key << 1;
            idx++;
        }

        key = key << 1;
        idx++;
        for(;key < max;)
        {
            if((selectedCity.buildings & key) > 0)
            {
                cityTexts[5].text += ", ";
                switch(idx)
                {
                    case 0 : cityTexts[5].text += "Agency"; break;
                    case 1 : cityTexts[5].text += "Factory"; break;
                    case 2 : cityTexts[5].text += "Lab"; break;
                    case 3 : cityTexts[5].text += "Harbor"; break;
                    case 4 : cityTexts[5].text += "Airport"; break;
                    case 5 : cityTexts[5].text += "Supply Depot"; break;
                    case 6 : cityTexts[5].text += "Army Base"; break;
                    case 7 : cityTexts[5].text += "Air Base"; break;
                    case 8 : cityTexts[5].text += "Naval Base"; break;
                    case 9 : cityTexts[5].text += "Communication Base"; break;
                }
            }
            key = key << 1;
            idx++;
        }
        
        cityTexts[7].text = selectedCity.traits.ToString();//추후 수정
    }

    public void ResetCityInfo()
    {
        cityTexts[1].text = "X";
        cityTexts[3].text = "X";
        cityTexts[5].text = "X";
        cityTexts[7].text = "X";
    }
}
