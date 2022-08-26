using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonManager : MonoBehaviour
{
    private static PersonManager currentInstance;

    public static int isAgent {get; private set;} //해당 id의 인물이 요원인지 표시하는 비트맵 인덱스

    public static Person[] personList {get; private set;}

    // 싱글톤 접근용 프로퍼티
    public static PersonManager instance
    {
        get
        {
            // 만약 싱글톤 변수에 아직 오브젝트가 할당되지 않았다면
            if (currentInstance == null)
            {
                // 씬에서 PersonManager 오브젝트를 찾아 할당
                currentInstance = FindObjectOfType<PersonManager>();
            }

            // 싱글톤 오브젝트를 반환
            return currentInstance;
        }
    }

    private void Awake() {
        // 씬에 싱글톤 오브젝트가 된 다른 PersonManager 오브젝트가 있다면
        if (instance != this)
        {
            // 자신을 파괴
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        personList = new Person[8];
        for(int i = 0;i < 8;i++)
        {
            personList[i] = new Person();
        }

        personList[0].SetNewPerson(0, "Dutch");
        personList[1].SetNewPerson(1, "Arthur");
        personList[2].SetNewPerson(2, "John");
        personList[3].SetNewPerson(3, "Hosea");
        personList[4].SetNewPerson(4, "Micah");
        personList[5].SetNewPerson(5, "Charles");
        personList[6].SetNewPerson(6, "Bill");
        personList[7].SetNewPerson(7, "Javier");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static bool checkAgent(int id)
    {
        return (isAgent & (1 << id)) > 0;
    }
}
