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

    public Deploy deployInstance {get; private set;}

    [SerializeField]
    private GameObject warningMessage1;

    [SerializeField]
    private TextMeshProUGUI cityInfo;

    [SerializeField]
    private GameObject DeployScript;

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

    public void ReflectCityInfo()
    {
        deployInstance.ReflectCityInfo(baseCity, selectedCity);
    }

    public void ShowCityInfo()
    {
        cityInfo.text = "City Number : " + selectedCityNumber;
    }

    public void ShowWarningMessage1()
    {
        if(selectedCityNumber == -1)
            warningMessage1.gameObject.SetActive(true);
    }

    public void HideWarningMessage1()
    {
        warningMessage1.gameObject.SetActive(false);
    }
}
