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
            ShowUnselectedCityView();
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

    public void ShowUnselectedCityView()
    {
        unselectedCityView.gameObject.SetActive(true);
    }

    public void ShowLowMoneyView()
    {
        lowMoneyView.gameObject.SetActive(true);
    }

    public void ShowUnselectedAgentView()
    {
        unselectedAgentView.gameObject.SetActive(true);
    }

    public void ShowUnselectedTargetView()
    {
        unselectedTargetView.gameObject.SetActive(true);
    }

    public void ShowUnselectedMethodView()
    {
        unselectedMethodView.gameObject.SetActive(true);
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
}
