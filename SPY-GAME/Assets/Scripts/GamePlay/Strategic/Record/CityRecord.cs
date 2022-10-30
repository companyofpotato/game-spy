using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Linq;

public struct CityData
{
    public int id;
    public string name;
    public int type;
    public Transform transform;

    public CityData(int id, string name, int type, Transform transform)
    {
        this.id = id;
        this.name = name;
        this.type = type;
        this.transform = transform;
    }
}

public class CityRecord : MonoBehaviour
{
    private List<CityData> dataList;
    
    private InformationScreen informationScreenInstance;
    private int selectedInfoId;
    private Button previouslySelectedInfoButton;

    private int lastSort;
    private bool lastSortReverse;
    private Button lastSortButton;

    [SerializeField]
    private Transform cityScrollContent;

    [SerializeField]
    private GameObject cityButtonPrefab;

    [SerializeField]
    private GameObject informationScreen;

    // Start is called before the first frame update
    void Start()
    {
        dataList = new List<CityData>();
        informationScreenInstance = informationScreen.GetComponent<InformationScreen>();
        lastSort = 0;
        lastSortReverse = false;
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
        dataList = new List<CityData>();
        if(lastSortButton != null)
        {
            lastSortButton.GetComponentInChildren<Image>().color = new Color32(97, 97, 97, 255);
            lastSortButton = null;
        }
    }

    public void ResetScrollContentList()
    {
        Transform[] childList = cityScrollContent.GetComponentsInChildren<Transform>();
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
        foreach(var city in CityManager.cityList)
        {
            GameObject cityButton = Instantiate(cityButtonPrefab, cityScrollContent);
            CityData cityData = new CityData(city.id, city.name, city.type, cityButton.GetComponent<Transform>());
            TextMeshProUGUI[] texts = cityButton.GetComponentsInChildren<TextMeshProUGUI>();

            texts[0].text = cityData.name;
            switch(cityData.type)
            {
                case -1 : texts[1].text = "Base"; break;
                case 0 : texts[1].text = "Big City"; break;
                case 1 : texts[1].text = "Middle City"; break;
                case 2 : texts[1].text = "Small City"; break;
            }

            Button button = cityButton.GetComponent<Button>();
            button.onClick.AddListener(() => ClickInformationButton(cityData.id, button));
    
            dataList.Add(cityData);
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
            informationScreenInstance.UpdateReportInformationScreen(id, false);
        }
        else if(selectedInfoId != id)
        {
            previouslySelectedInfoButton.GetComponentInChildren<Image>().color = new Color32(97, 97, 97, 255);
            selectedInfoId = id;
            previouslySelectedInfoButton = self;
            self.GetComponentInChildren<Image>().color = Color.green;
            informationScreenInstance.UpdateReportInformationScreen(id, false);
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
                case 1 : dataList = dataList.OrderByDescending(x => x.name).ThenBy(x => x.id).ToList(); break;
                case 2 : dataList = dataList.OrderByDescending(x => x.type).ThenBy(x => x.id).ToList(); break;
            }
        }
        else
        {
            switch(n)
            {
                case 0 : dataList = dataList.OrderBy(x => x.id).ToList(); break;
                case 1 : dataList = dataList.OrderBy(x => x.name).ThenBy(x => x.id).ToList(); break;
                case 2 : dataList = dataList.OrderBy(x => x.type).ThenBy(x => x.id).ToList(); break;
            }
        }

        int count = dataList.Count;
        for(int idx = 0;idx < count;idx++)
        {
            dataList[idx].transform.SetSiblingIndex(idx);
        }
    }
}
