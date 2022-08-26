using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Deploy : MonoBehaviour
{
    public static City baseCity {get; private set;}
    public static City selectedCity {get; private set;}

    [SerializeField]
    private Transform scrollList;

    [SerializeField]
    private GameObject agentPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //선택된 도시의 정보를 DeployView에 반영한다.
    public void ReflectCityInfo(City city1, City city2)
    {
        Transform[] childList = scrollList.GetComponentsInChildren<Transform>();

        if(childList != null)
        {
            int childCount = childList.Length;
            for(int i = 1;i < childCount;i++)
            {
                Destroy(childList[i].gameObject);
            }
        }

        baseCity = city1;
        selectedCity = city2;
        Debug.Log(baseCity.name);
        Debug.Log(selectedCity.name);
        int count = baseCity.personList.Length;
        
        for(int i = 0;i < count;i++)
        {
            //만약 해당 인물이 아군 요원이라면,
            //if
            GameObject tmp = Instantiate(agentPrefab, scrollList); // 프리팹 생성
            tmp.GetComponent<Button>().onClick.AddListener(() => ChooseAgent(baseCity.personList[i])); // 프리팹에 버튼 함수 할당
            tmp.GetComponentInChildren<TextMeshProUGUI>().text = PersonManager.personList[baseCity.personList[i]].name;
        }
    }

    //선택된 요원의 정보를 오른쪽 뷰에 띄워준다.
    public static void ChooseAgent(int id)
    {
        
    }

    public void Execute()
    {
        
    }
}
