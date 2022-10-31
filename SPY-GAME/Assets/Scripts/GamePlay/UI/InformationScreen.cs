using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InformationScreen : MonoBehaviour
{
    private TextMeshProUGUI[] personTexts;
    private TextMeshProUGUI[] equipmentTexts;

    [SerializeField]
    private GameObject personInformation;

    [SerializeField]
    private GameObject equipmentInformation;

    [SerializeField]
    private GameObject reportInformation;

    [SerializeField]
    private Transform reportContent;

    [SerializeField]
    private GameObject reportPrefab;

    // Start is called before the first frame update
    void Start()
    {
        personTexts = personInformation.GetComponentsInChildren<TextMeshProUGUI>();
        equipmentTexts = equipmentInformation.GetComponentsInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowPersonInformation()
    {
        personInformation.gameObject.SetActive(true);
    }

    public void HidePersonInformation()
    {
        personInformation.gameObject.SetActive(false);
    }

    public void ShowEquipmentInformation()
    {
        equipmentInformation.gameObject.SetActive(true);
    }

    public void HideEquipmentInformation()
    {
        equipmentInformation.gameObject.SetActive(false);
    }

    public void ShowReportInformation()
    {
        reportInformation.gameObject.SetActive(true);
    }

    public void HideReportInformation()
    {
        reportInformation.gameObject.SetActive(false);
    }

    public void HideAllInformationScreen()
    {
        HidePersonInformation();
        HideEquipmentInformation();
        HideReportInformation();
    }

    public void UpdatePersonInformationScreen(int id)
    {
        HideAllInformationScreen();

        personTexts[20].color = Color.white; personTexts[21].color = Color.white;
        personTexts[24].color = Color.white; personTexts[25].color = Color.white;
        personTexts[28].color = Color.white; personTexts[29].color = Color.white;
        personTexts[30].color = Color.white; personTexts[31].color = Color.white;
        personTexts[32].color = Color.white; personTexts[33].color = Color.white;
        personTexts[34].color = Color.white; personTexts[35].color = Color.white;
        personTexts[36].color = Color.white; personTexts[37].color = Color.white;

        Person shownPerson = PersonManager.personList[id];
        personTexts[1].text = shownPerson.isAgent ? "Agent" : "Informant";
        personTexts[3].text = shownPerson.codename;
        personTexts[5].text = shownPerson.firstName;
        personTexts[7].text = shownPerson.familyName;
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

        for(int i = 0, idx = 5;i < 18;i++)
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

        ShowPersonInformation();
    }

    public void UpdateEquipmentInformationScreen(int id)
    {
        HideAllInformationScreen();

        Equipment shownEquipment = EquipmentManager.equipmentList[id];
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
    
        ShowEquipmentInformation();
    }

    public void UpdateReportInformationScreen(int id, bool personDef)
    {
        HideAllInformationScreen();

        List<string> reportList = new List<string>();
        if(personDef)
            reportList = PersonManager.GetReportList(id);
        else
            reportList = CityManager.GetReportList(id);

        Transform[] childList = reportContent.GetComponentsInChildren<Transform>();
        int childCount;

        if(childList != null)
        {
            childCount = childList.Length;
            for(int i = 4;i < childCount;i++)
            {
                Destroy(childList[i].gameObject);
            }
        }

        foreach(string item in reportList)
        {
            GameObject tmp = Instantiate(reportPrefab, reportContent);
            TextMeshProUGUI tmpText = tmp.GetComponentInChildren<TextMeshProUGUI>();
            tmpText.text = item;
        }

        ShowReportInformation();
    }
}
