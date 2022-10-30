using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GamePlayUIManager : MonoBehaviour
{
    private static GamePlayUIManager currentInstance;

    [SerializeField]
    private int strategicViewsCount;

    [SerializeField]
    private int tacticalViewsCount;

    [SerializeField]
    private GameObject[] strategicViews;

    [SerializeField]
    private GameObject[] tacticalViews;

    [SerializeField]
    private GameObject unselectedCityView;

    [SerializeField]
    private GameObject lowMoneyView;

    [SerializeField]
    private GameObject unselectedAgentView;

    [SerializeField]
    private GameObject unselectedTargetView;

    [SerializeField]
    private GameObject unselectedMethodView;

    // 싱글톤 접근용 프로퍼티
    public static GamePlayUIManager instance
    {
        get
        {
            // 만약 싱글톤 변수에 아직 오브젝트가 할당되지 않았다면
            if (currentInstance == null)
            {
                // 씬에서 GamePlayUIManager 오브젝트를 찾아 할당
                currentInstance = FindObjectOfType<GamePlayUIManager>();
            }

            // 싱글톤 오브젝트를 반환
            return currentInstance;
        }
    }

    private void Awake() {
        // 씬에 싱글톤 오브젝트가 된 다른 GamePlayUIManager 오브젝트가 있다면
        if (instance != this)
        {
            // 자신을 파괴
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowStrategicView(int n)
    {
        HideAllViews();
        strategicViews[n].gameObject.SetActive(true);
    }

    public void HideStrategicView(int n)
    {
        strategicViews[n].gameObject.SetActive(false);
    }

    public void ShowTacticalView(int n)
    {
        if(TacticalActionManager.selectedCityNumber == -1)
        {
            unselectedCityView.gameObject.SetActive(true);
            return;
        }
        
        HideAllViews();
        tacticalViews[n].gameObject.SetActive(true);
    }

    public void HideTacticalView(int n)
    {
        tacticalViews[n].gameObject.SetActive(false);
    }

    public void HideAllViews()
    {
        for(int i = 0;i < strategicViewsCount;i++)
        {
            strategicViews[i].gameObject.SetActive(false);
        }
        for(int i = 0;i < tacticalViewsCount;i++)
        {
            tacticalViews[i].gameObject.SetActive(false);
        }
    }

    public void HideUnselectedCityView()
    {
        unselectedCityView.gameObject.SetActive(false);
    }

    public void HideLowMoneyView()
    {
        lowMoneyView.gameObject.SetActive(false);
    }

    public void HideUnselectedAgentView()
    {
        unselectedAgentView.gameObject.SetActive(false);
    }

    public void HideUnselectedTargetView()
    {
        unselectedTargetView.gameObject.SetActive(false);
    }

    public void HideUnselectedMethodView()
    {
        unselectedMethodView.gameObject.SetActive(false);
    }

    public static void UpdatePersonInformationScreen(Person shownPerson, TextMeshProUGUI[] personTexts)
    {
        personTexts[1].text = shownPerson.isAgent ? "Agent" : "Informant";
        personTexts[3].text = shownPerson.firstName;
        personTexts[5].text = shownPerson.familyName;
        personTexts[7].text = shownPerson.codename;
        personTexts[9].text = shownPerson.age.ToString();
        if(shownPerson.gender)
            personTexts[11].text = "Male";
        else if(shownPerson.gender)
            personTexts[11].text = "Female";
        if(shownPerson.sexualHetero && shownPerson.sexualHomo)
            personTexts[13].text = "bisexual";
        else if(shownPerson.sexualHetero)
            personTexts[13].text = "Heterosexual";
        else if(shownPerson.sexualHomo)
            personTexts[13].text = "Homosexaul";
        personTexts[15].text = shownPerson.appearance.ToString();
        personTexts[17].text = shownPerson.status.ToString();//문자열로 수정하기
        if(shownPerson.belong > 0)
            personTexts[19].text = "Friendly";
        else if(shownPerson.belong < 0)
            personTexts[19].text = "Enemy";
        else
            personTexts[19].text = "Neutrality";
        if(shownPerson.belong == 0)
            personTexts[21].text = "X";
        else
            personTexts[21].text = shownPerson.passion.ToString();
        personTexts[23].text = CityManager.cityList[shownPerson.location].name;
        personTexts[25].text = shownPerson.exposure.ToString();
        personTexts[27].text = shownPerson.rank.ToString();
        personTexts[29].text = shownPerson.aim.ToString();
        personTexts[31].text = shownPerson.stealth.ToString();
        personTexts[33].text = shownPerson.handicraft.ToString();
        personTexts[35].text = shownPerson.analysis.ToString();
        personTexts[37].text = shownPerson.narration.ToString();

        personTexts[39].text = shownPerson.trait.ToString();//추후에 수정 필요
        personTexts[41].text = shownPerson.perk.ToString();//추후에 수정 필요

        for(int i = 0, idx = 3;i < 19;i++)
        {
            if(shownPerson.CheckReveal(i) == false)
            {
                personTexts[idx].text = "???";
            }
            if(i == 5)
            {
                i++;
                if(shownPerson.CheckReveal(i) == false)
                {
                    personTexts[idx].text = "???";
                }
            }
            idx += 2;
        }

        for(int i = 0;i < 7;i++)
        {
            if(shownPerson.CheckReal(i))
            {
                switch(i)
                {
                    case 0 : personTexts[20].color = Color.blue; personTexts[21].color = Color.blue; break;
                    case 1 : personTexts[24].color = Color.blue; personTexts[25].color = Color.blue; break;
                    case 2 : personTexts[28].color = Color.blue; personTexts[29].color = Color.blue; break;
                    case 3 : personTexts[30].color = Color.blue; personTexts[31].color = Color.blue; break;
                    case 4 : personTexts[32].color = Color.blue; personTexts[33].color = Color.blue; break;
                    case 5 : personTexts[34].color = Color.blue; personTexts[35].color = Color.blue; break;
                    case 6 : personTexts[36].color = Color.blue; personTexts[37].color = Color.blue; break;
                }
            }
        }
    }

    public static void UpdateEquipmentInformationScreen(Equipment shownEquipment, TextMeshProUGUI[] equipmentTexts)
    {
        equipmentTexts[1].text = shownEquipment.name;
        equipmentTexts[3].text = shownEquipment.cost.ToString();
        equipmentTexts[5].text = shownEquipment.description;
        if(shownEquipment.bef < 0)
            equipmentTexts[7].text = shownEquipment.bef.ToString() + "%";
        else
            equipmentTexts[7].text = "+" + shownEquipment.bef.ToString() + "%";
        if(shownEquipment.success < 0)
            equipmentTexts[9].text = shownEquipment.success.ToString() + "%";
        else
            equipmentTexts[9].text = "+" + shownEquipment.success.ToString() + "%";
        if(shownEquipment.aft < 0)
            equipmentTexts[11].text = shownEquipment.aft.ToString() + "%";
        else
            equipmentTexts[11].text = "+" + shownEquipment.aft.ToString() + "%";
        if(shownEquipment.escape < 0)
            equipmentTexts[13].text = shownEquipment.escape.ToString() + "%";
        else
            equipmentTexts[13].text = "+" + shownEquipment.escape.ToString() + "%";
    }
}
