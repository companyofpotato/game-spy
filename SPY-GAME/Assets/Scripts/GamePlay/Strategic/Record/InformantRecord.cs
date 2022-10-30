using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Linq;

public struct InformantData
{
    public int id;
    public string codename;
    public int status;
    public int passion;
    public int location;
    public int exposure;
    public Transform transform;

    public InformantData(int id, string codename, int status, int passion, int location, int exposure, Transform transform)
    {
        this.id = id;
        this.codename = codename;
        this.status = status;
        this.passion = passion;
        this.location = location;
        this.exposure = exposure;
        this.transform = transform;
    }
}

public class InformantRecord : MonoBehaviour
{
    private List<InformantData> dataList;
    
    private InformationScreen informationScreenInstance;
    private int selectedInfoId;
    private bool personDef;
    private Button previouslySelectedInfoButton;

    private int lastSort;
    private bool lastSortReverse;
    private Button lastSortButton;

    [SerializeField]
    private Transform informantScrollContent;

    [SerializeField]
    private GameObject informantButtonPrefab;

    [SerializeField]
    private GameObject informationScreen;

    // Start is called before the first frame update
    void Start()
    {
        dataList = new List<InformantData>();
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
        dataList = new List<InformantData>();
        if(lastSortButton != null)
        {
            lastSortButton.GetComponentInChildren<Image>().color = new Color32(97, 97, 97, 255);
            lastSortButton = null;
        }
    }

    public void ResetScrollContentList()
    {
        Transform[] childList = informantScrollContent.GetComponentsInChildren<Transform>();
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
        where person.isAgent == false && person.status != -14
        select person;

        foreach(var person in personQuery)
        {
            GameObject informantButton = Instantiate(informantButtonPrefab, informantScrollContent);
            InformantData informantData = new InformantData(person.id, person.codename, person.status, person.passion, person.location, person.exposure, informantButton.GetComponent<Transform>());

            if(person.CheckReveal(7) == false)
            {
                informantData.status = -1000;
            }

            if(person.CheckReveal(9) == false)
            {
                informantData.passion = -1;
            }

            if(person.CheckReveal(10) == false)
            {
                informantData.location = -1;
            }

            if(person.CheckReveal(11) == false)
            {
                informantData.exposure = -1;
            }

            TextMeshProUGUI[] texts = informantButton.GetComponentsInChildren<TextMeshProUGUI>();

            texts[0].text = informantData.codename;
            if(person.belong > 0)
                texts[0].color = Color.blue;
            else
                texts[0].color = Color.red;
            if(informantData.status == -1000)
                texts[1].text = "???";
            else 
                texts[1].text = informantData.status.ToString();//문자열로 수정하기
            if(informantData.passion < 0)
                texts[2].text = "???";
            else 
                texts[2].text = informantData.passion.ToString();
            if(informantData.location < 0)
                texts[3].text = "???";
            else 
                texts[3].text = CityManager.cityList[informantData.location].name;
            if(informantData.exposure < 0)
                texts[4].text = "???";
            else 
                texts[4].text = informantData.exposure.ToString();

            Button button = informantButton.GetComponent<Button>();
            button.onClick.AddListener(() => ClickInformationButton(informantData.id, button));

            dataList.Add(informantData);
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
                case 2 : dataList = dataList.OrderByDescending(x => x.status).ThenBy(x => x.id).ToList(); break;
                case 3 : dataList = dataList.OrderByDescending(x => x.passion).ThenBy(x => x.id).ToList(); break;
                case 4 : dataList = dataList.OrderByDescending(x => x.location).ThenBy(x => x.id).ToList(); break;
                case 5 : dataList = dataList.OrderByDescending(x => x.exposure).ThenBy(x => x.id).ToList(); break;
            }
        }
        else
        {
            switch(n)
            {
                case 0 : dataList = dataList.OrderBy(x => x.id).ToList(); break;
                case 1 : dataList = dataList.OrderBy(x => x.codename).ThenBy(x => x.id).ToList(); break;
                case 2 : dataList = dataList.OrderBy(x => x.status).ThenBy(x => x.id).ToList(); break;
                case 3 : dataList = dataList.OrderBy(x => x.passion).ThenBy(x => x.id).ToList(); break;
                case 4 : dataList = dataList.OrderBy(x => x.location).ThenBy(x => x.id).ToList(); break;
                case 5 : dataList = dataList.OrderBy(x => x.exposure).ThenBy(x => x.id).ToList(); break;
            }
        }

        int count = dataList.Count;
        for(int idx = 0;idx < count;idx++)
        {
            dataList[idx].transform.SetSiblingIndex(idx);
        }
    }
}
