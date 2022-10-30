using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Linq;

public struct PrisonerData
{
    public int id;
    public string codename;
    public int appearance;
    public int status;
    public int passion;
    public int location;
    public int exposure;
    public int rank;
    public int aim;
    public int stealth;
    public int handicraft;
    public int analysis;
    public int narration;
    public Transform transform;

    public PrisonerData(int id, string codename, int appearance, int status, int passion, int location, int exposure, int rank, int aim, int stealth, int handicraft, int analysis, int narration, Transform transform)
    {
        this.id = id;
        this.codename = codename;
        this.appearance = appearance;
        this.status = status;
        this.passion = passion;
        this.location = location;
        this.exposure = exposure;
        this.rank = rank;
        this.aim = aim;
        this.stealth = stealth;
        this.handicraft = handicraft;
        this.analysis = analysis;
        this.narration = narration;
        this.transform = transform;
    }
}

public class PrisonerRecord : MonoBehaviour
{
    private List<PrisonerData> dataList;
    
    private InformationScreen informationScreenInstance;
    private int selectedInfoId;
    private bool personDef;
    private Button previouslySelectedInfoButton;

    private int lastSort;
    private bool lastSortReverse;
    private Button lastSortButton;

    [SerializeField]
    private Transform prisonerScrollContent;

    [SerializeField]
    private GameObject prisonerButtonPrefab;

    [SerializeField]
    private GameObject informationScreen;

    // Start is called before the first frame update
    void Start()
    {
        dataList = new List<PrisonerData>();
        informationScreenInstance = informationScreen.GetComponent<InformationScreen>();
        lastSort = 0;
        lastSortReverse = false;
        personDef = true;
        selectedInfoId = -1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public void ResetVariables()
    {
        lastSort = 0;
        lastSortReverse = false;
        selectedInfoId = -1;
        dataList = new List<PrisonerData>();
        if(lastSortButton != null)
        {
            lastSortButton.GetComponentInChildren<Image>().color = new Color32(97, 97, 97, 255);
            lastSortButton = null;
        }
    }

    public void ResetScrollContentList()
    {
        Transform[] childList = prisonerScrollContent.GetComponentsInChildren<Transform>();
        int childCount;

        if(childList != null)
        {
            childCount = childList.Length;
            for(int i = 1;i < childCount;i++)
            {
                Destroy(childList[i].gameObject);
            }
        }
    }

    public void MakeStructList()
    {
        var personQuery = 
        from person in PersonManager.personList
        where person.status == -14
        select person;

        foreach(var person in personQuery)
        {
            GameObject prisonerButton = Instantiate(prisonerButtonPrefab, prisonerScrollContent);
            PrisonerData prisonerData = new PrisonerData(person.id, person.codename, person.appearance, person.status, person.passion, person.location, person.exposure, person.rank, person.aim, person.stealth, person.handicraft, person.analysis, person.narration, prisonerButton.GetComponent<Transform>());
            
            if(person.CheckReveal(6) == false)
            {
                prisonerData.appearance = -1;
            }

            if(person.CheckReveal(7) == false)
            {
                prisonerData.status = -1000;
            }

            if(person.CheckReveal(9) == false)
            {
                prisonerData.passion = -1;
            }

            if(person.CheckReveal(10) == false)
            {
                prisonerData.location = -1;
            }

            if(person.CheckReveal(11) == false)
            {
                prisonerData.exposure = -1;
            }

            if(person.CheckReveal(12) == false)
            {
                prisonerData.rank = -1;
            }

            if(person.CheckReveal(13) == false)
            {
                prisonerData.aim = -1;
            }

            if(person.CheckReveal(14) == false)
            {
                prisonerData.stealth = -1;
            }

            if(person.CheckReveal(15) == false)
            {
                prisonerData.handicraft = -1;
            }

            if(person.CheckReveal(16) == false)
            {
                prisonerData.analysis = -1;
            }

            if(person.CheckReveal(17) == false)
            {
                prisonerData.narration = -1;
            }

            TextMeshProUGUI[] texts = prisonerButton.GetComponentsInChildren<TextMeshProUGUI>();

            texts[0].text = prisonerData.codename;
            if(person.belong > 0)
                texts[0].color = Color.blue;
            else
                texts[0].color = Color.red;
            if(prisonerData.appearance < 0)
                texts[1].text = "???";
            else 
                texts[1].text = prisonerData.appearance.ToString();
            if(prisonerData.status == -1000)
                texts[2].text = "???";
            else 
                texts[2].text = prisonerData.status.ToString();//문자열로 수정하기
            if(prisonerData.passion < 0)
                texts[3].text = "???";
            else 
                texts[3].text = prisonerData.passion.ToString();
            if(prisonerData.location < 0)
                texts[4].text = "???";
            else 
                texts[4].text = CityManager.cityList[prisonerData.location].name;
            if(prisonerData.exposure < 0)
                texts[5].text = "???";
            else 
                texts[5].text = prisonerData.exposure.ToString();
            if(prisonerData.rank < 0)
                texts[6].text = "???";
            else 
                texts[6].text = prisonerData.rank.ToString();
            if(prisonerData.aim < 0)
                texts[7].text = "???";
            else 
                texts[7].text = prisonerData.aim.ToString();
            if(prisonerData.stealth < 0)
                texts[8].text = "???";
            else 
                texts[8].text = prisonerData.stealth.ToString();
            if(prisonerData.handicraft < 0)
                texts[9].text = "???";
            else 
                texts[9].text = prisonerData.handicraft.ToString();
            if(prisonerData.analysis < 0)
                texts[10].text = "???";
            else 
                texts[10].text = prisonerData.analysis.ToString();
            if(prisonerData.narration < 0)
                texts[11].text = "???";
            else 
                texts[11].text = prisonerData.narration.ToString();

            Button button = prisonerButton.GetComponent<Button>();
            button.onClick.AddListener(() => ClickInformationButton(prisonerData.id, button));

            dataList.Add(prisonerData);
        }

        SortDataList(0, true);
    }

    void ClickInformationButton(int id, Button self)
    {
        if(selectedInfoId < 0)
        {
            selectedInfoId = id;
            previouslySelectedInfoButton = self;
            self.GetComponentInChildren<Image>().color = Color.green;
            if(personDef)
            {
                informationScreenInstance.UpdatePersonInformationScreen(id);
            }
            else
            {
                informationScreenInstance.UpdateReportInformationScreen(id, true);
            }
        }
        else if(selectedInfoId != id)
        {
            previouslySelectedInfoButton.GetComponentInChildren<Image>().color = new Color32(97, 97, 97, 255);
            selectedInfoId = id;
            previouslySelectedInfoButton = self;
            self.GetComponentInChildren<Image>().color = Color.green;
            if(personDef)
            {
                informationScreenInstance.UpdatePersonInformationScreen(id);
            }
            else
            {
                informationScreenInstance.UpdateReportInformationScreen(id, true);
            }
        }
    }

    public void SwitchPersonReport(TextMeshProUGUI text)
    {
        if(personDef)
        {
            personDef = false;
            text.text = "Show Person Information";
            if(selectedInfoId >= 0)
            {
                informationScreenInstance.UpdateReportInformationScreen(selectedInfoId, true);
            }
        }
        else
        {
            personDef = true;
            text.text = "Show Report Information";
            if(selectedInfoId >= 0)
            {
                informationScreenInstance.UpdatePersonInformationScreen(selectedInfoId);
            }
        }
    }

    public void ChangeSortColorToggle(Button self)
    {
        if(lastSortButton != null && lastSortButton != self)
        {
            lastSortButton.GetComponentInChildren<Image>().color = new Color32(97, 97, 97, 255);
        }
        lastSortButton = self;
    }

    public void SortDataToggle(int n)
    {
        if(lastSort == n)
        {
            if(lastSortReverse)
            {
                SortDataList(n, false);
                lastSortButton.GetComponentInChildren<Image>().color = Color.green;
            }
            else
            {
                SortDataList(n, true);
                lastSortButton.GetComponentInChildren<Image>().color = Color.blue;
            }
        }
        else
        {
            SortDataList(n, false);
            lastSortButton.GetComponentInChildren<Image>().color = Color.green;
        }
    }

    void SortDataList(int n, bool reverse)
    {
        lastSort = n;
        lastSortReverse = reverse;
        if(reverse == false)
        {
            switch(n)
            {
                case 0 : dataList = dataList.OrderByDescending(x => x.id).ToList(); break;
                case 1 : dataList = dataList.OrderByDescending(x => x.codename).ThenBy(x => x.id).ToList(); break;
                case 2 : dataList = dataList.OrderByDescending(x => x.appearance).ThenBy(x => x.id).ToList(); break;
                case 3 : dataList = dataList.OrderByDescending(x => x.status).ThenBy(x => x.id).ToList(); break;
                case 4 : dataList = dataList.OrderByDescending(x => x.passion).ThenBy(x => x.id).ToList(); break;
                case 5 : dataList = dataList.OrderByDescending(x => x.location).ThenBy(x => x.id).ToList(); break;
                case 6 : dataList = dataList.OrderByDescending(x => x.exposure).ThenBy(x => x.id).ToList(); break;
                case 7 : dataList = dataList.OrderByDescending(x => x.rank).ThenBy(x => x.id).ToList(); break;
                case 8 : dataList = dataList.OrderByDescending(x => x.aim).ThenBy(x => x.id).ToList(); break;
                case 9 : dataList = dataList.OrderByDescending(x => x.stealth).ThenBy(x => x.id).ToList(); break;
                case 10 : dataList = dataList.OrderByDescending(x => x.handicraft).ThenBy(x => x.id).ToList(); break;
                case 11 : dataList = dataList.OrderByDescending(x => x.analysis).ThenBy(x => x.id).ToList(); break;
                case 12 : dataList = dataList.OrderByDescending(x => x.narration).ThenBy(x => x.id).ToList(); break;
            }
        }
        else
        {
            switch(n)
            {
                case 0 : dataList = dataList.OrderBy(x => x.id).ToList(); break;
                case 1 : dataList = dataList.OrderBy(x => x.codename).ThenBy(x => x.id).ToList(); break;
                case 2 : dataList = dataList.OrderBy(x => x.appearance).ThenBy(x => x.id).ToList(); break;
                case 3 : dataList = dataList.OrderBy(x => x.status).ThenBy(x => x.id).ToList(); break;
                case 4 : dataList = dataList.OrderBy(x => x.passion).ThenBy(x => x.id).ToList(); break;
                case 5 : dataList = dataList.OrderBy(x => x.location).ThenBy(x => x.id).ToList(); break;
                case 6 : dataList = dataList.OrderBy(x => x.exposure).ThenBy(x => x.id).ToList(); break;
                case 7 : dataList = dataList.OrderBy(x => x.rank).ThenBy(x => x.id).ToList(); break;
                case 8 : dataList = dataList.OrderBy(x => x.aim).ThenBy(x => x.id).ToList(); break;
                case 9 : dataList = dataList.OrderBy(x => x.stealth).ThenBy(x => x.id).ToList(); break;
                case 10 : dataList = dataList.OrderBy(x => x.handicraft).ThenBy(x => x.id).ToList(); break;
                case 11 : dataList = dataList.OrderBy(x => x.analysis).ThenBy(x => x.id).ToList(); break;
                case 12 : dataList = dataList.OrderBy(x => x.narration).ThenBy(x => x.id).ToList(); break;
            }
        }

        int count = dataList.Count;
        for(int idx = 0;idx < count;idx++)
        {
            dataList[idx].transform.SetSiblingIndex(idx);
        }
    }
}
