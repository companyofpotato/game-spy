using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    private static MainMenuManager currentInstance;

    [SerializeField]
    private GameObject newGameView;

    [SerializeField]
    private GameObject loadGameView;

    [SerializeField]
    private GameObject optionView;

    [SerializeField]
    private GameObject saveGameView;

    // 싱글톤 접근용 프로퍼티
    public static MainMenuManager instance
    {
        get
        {
            // 만약 싱글톤 변수에 아직 오브젝트가 할당되지 않았다면
            if (currentInstance == null)
            {
                // 씬에서 MainMenuManager 오브젝트를 찾아 할당
                currentInstance = FindObjectOfType<MainMenuManager>();
            }

            // 싱글톤 오브젝트를 반환
            return currentInstance;
        }
    }

    private void Awake() {
        // 씬에 싱글톤 오브젝트가 된 다른 MainMenuManager 오브젝트가 있다면
        if (instance != this)
        {
            // 자신을 파괴
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ShowNewGameView()
    {
        newGameView.gameObject.SetActive(true);
    }

    public void HideNewGameView()
    {
        newGameView.gameObject.SetActive(false);
    }

    public void ShowLoadGameView()
    {
        loadGameView.gameObject.SetActive(true);
    }

    public void HideLoadGameView()
    {
        loadGameView.gameObject.SetActive(false);
    }

    public void ShowOptionView()
    {
        optionView.gameObject.SetActive(true);
    }

    public void HideOptionView()
    {
        optionView.gameObject.SetActive(false);
    }

    public void ShowSaveGameView()
    {
        saveGameView.gameObject.SetActive(true);
    }

    public void HideSaveGameView()
    {
        saveGameView.gameObject.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene(0);
    }
}
