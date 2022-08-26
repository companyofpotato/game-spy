using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    private static EventManager currentInstance;

    [SerializeField]
    private GameObject reportView;

    // 싱글톤 접근용 프로퍼티
    public static EventManager instance
    {
        get
        {
            // 만약 싱글톤 변수에 아직 오브젝트가 할당되지 않았다면
            if (currentInstance == null)
            {
                // 씬에서 EventManager 오브젝트를 찾아 할당
                currentInstance = FindObjectOfType<EventManager>();
            }

            // 싱글톤 오브젝트를 반환
            return currentInstance;
        }
    }

    private void Awake() {
        // 씬에 싱글톤 오브젝트가 된 다른 EventManager 오브젝트가 있다면
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

    public void AddAction()
    {

    }

    public void EndTurn()
    {

    }

    public void showReportView()
    {
        reportView.gameObject.SetActive(true);
    }

    public void hideReportView()
    {
        reportView.gameObject.SetActive(false);
    }
}
