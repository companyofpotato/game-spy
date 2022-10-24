using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceManager : MonoBehaviour
{
    private static ResourceManager currentInstance;

    [SerializeField]
    private int initialMoney;

    [SerializeField]
    private int initialInfo;

    [SerializeField]
    private GameObject resourceInformation;

    private TextMeshProUGUI[] resourceTexts;

    public static int money {get; private set;}

    public static int info {get; private set;}

    // 싱글톤 접근용 프로퍼티
    public static ResourceManager instance
    {
        get
        {
            // 만약 싱글톤 변수에 아직 오브젝트가 할당되지 않았다면
            if (currentInstance == null)
            {
                // 씬에서 ResourceManager 오브젝트를 찾아 할당
                currentInstance = FindObjectOfType<ResourceManager>();
            }

            // 싱글톤 오브젝트를 반환
            return currentInstance;
        }
    }

    private void Awake()
    {
        // 씬에 싱글톤 오브젝트가 된 다른 ResourceManager 오브젝트가 있다면
        if (instance != this)
        {
            // 자신을 파괴
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        money = initialMoney;
        info = initialInfo;
        resourceTexts = resourceInformation.GetComponentsInChildren<TextMeshProUGUI>();
        ResetResourceUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetResourceUI()
    {
        resourceTexts[1].text = money.ToString();
        resourceTexts[3].text = info.ToString();
    }

    public static void ChangeMoney(int newMoney)
    {
        money = newMoney;
    }

    public static void ChangeInfo(int newInfo)
    {
        info = newInfo;
    }
}
