using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class SaveFile
{
    public string savedTime {get; private set;}
    public string filename {get; private set;}
    public string codeName {get; private set;}
    public int difficulty {get; private set;}

    public SaveFile()
    {
        savedTime = System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
        filename = "Save_File_";
        codeName = "not typed";
        difficulty = -1;
    }

    public void SetCodeName(string str)
    {
        codeName = str;
    }

    public void SetDifficulty(int i)
    {
        difficulty = i;
    }
}

public class SaveFileManager : MonoBehaviour
{
    private static SaveFileManager currentInstance;

    private string path;
    private string fileName;
    SaveFile currentFile = new SaveFile(); //새로 시작했을 때 데이터를 저장할 인스턴스
    SaveFile loadedFile; //로컬 파일에서 불러온 데이터를 저장할 인스턴스

    [SerializeField]
    private TMP_InputField codeName;

    [SerializeField]
    private TMP_Dropdown difficulty;

    // 싱글톤 접근용 프로퍼티
    public static SaveFileManager instance
    {
        get
        {
            // 만약 싱글톤 변수에 아직 오브젝트가 할당되지 않았다면
            if (currentInstance == null)
            {
                // 씬에서 SaveFileManager 오브젝트를 찾아 할당
                currentInstance = FindObjectOfType<SaveFileManager>();
            }

            // 싱글톤 오브젝트를 반환
            return currentInstance;
        }
    }

    private void Awake() {
        // 씬에 싱글톤 오브젝트가 된 다른 SaveFileManager 오브젝트가 있다면
        if (instance != this)
        {
            // 자신을 파괴
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this.gameObject);

        path = Application.persistentDataPath + "/";
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(path);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MakeNewFile()
    {
        currentFile = new SaveFile();
        currentFile.SetCodeName(codeName.text);
        currentFile.SetDifficulty(difficulty.value);

        Debug.Log(currentFile.codeName);
        Debug.Log(currentFile.difficulty);
    }

    public void SelectSlot(string num)
    {
        Debug.Log(num);
        fileName = "Save_File_" + num;
    }

    public void SaveData()
    {
        string fileData = JsonUtility.ToJson(currentFile);
        File.WriteAllText(path + fileName, fileData);
    }

    public void LoadData()
    {
        string fileData = File.ReadAllText(path + fileName);
        loadedFile = JsonUtility.FromJson<SaveFile>(fileData);
    }
}
