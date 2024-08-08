using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GuideWindow : MonoBehaviour
{
    public NetworkConnect network;
    public GameObject networkPanel;

    public GameObject battleWindow;
    public GameObject skillWindow;
    public GameObject upgradeWindow;
    public GameObject gachaWindow;
    public GameObject equipmentWindow;
    public GameObject questWindow;

    public Button skipButton;
    public Button backButton;
    public Button nextButton;
    public Button closeButton;

    private Dictionary<Guide_Windows, GameObject> windows;
    private List<Guide_Windows> windowOrder;
    private int currentWindowIndex = 0;
    
    //private string mainSceneAddress = "Main";
    //public AssetLabelReference skillIconLable;

    private void Awake()
    {
        windows = new Dictionary<Guide_Windows, GameObject>()
        {
            {Guide_Windows.Battle, battleWindow},
            {Guide_Windows.Skill, skillWindow },
            {Guide_Windows.Upgrade, upgradeWindow },
            {Guide_Windows.Gacha, gachaWindow },
            {Guide_Windows.Equipment, equipmentWindow },
            {Guide_Windows.Quest, questWindow },
        };

        windowOrder = new List<Guide_Windows>(windows.Keys);

        foreach (var win in windows)
        {
            win.Value.SetActive(false);
        }

        skipButton.onClick.AddListener(OnSkip);
        backButton.onClick.AddListener(OnBack);
        nextButton.onClick.AddListener(OnNext);
        closeButton.onClick.AddListener(CloseGuide);

        currentWindowIndex = 0;
        Open(windowOrder[currentWindowIndex]);
    }

    public void OpenGuide()
    {
        if (!network.CheckConnectInternet())
        {
            networkPanel.gameObject.SetActive(true);
            return;
        }
        else
        {
            gameObject.SetActive(true);
            Debug.Log("인터넷 연결 확인");
        }
    }

    public void CloseGuide()
    {
        gameObject.SetActive(false);
    }

    private void OnSkip()
    {
        SceneManager.LoadScene("Loading");
    }

    private void OnBack()
    {
        if (currentWindowIndex > 0)
        {
            currentWindowIndex--;
            Open(windowOrder[currentWindowIndex]);
        }
    }

    private void OnNext()
    {
        if (currentWindowIndex < windowOrder.Count - 1)
        {
            currentWindowIndex++;
            Open(windowOrder[currentWindowIndex]);
        }
        else if (windowOrder[currentWindowIndex] == Guide_Windows.Quest)
        { 
            SceneManager.LoadScene("Loading");
            //LoadScene(mainSceneAddress);
        }
    }

    public void Open(Guide_Windows window)
    {
        foreach (var win in windows)
        {
            win.Value.SetActive(false);
        }

        if (windows.ContainsKey(window))
        {
            windows[window].SetActive(true);
        }
    }

    public void CheckNetworkConnet()
    {
        if(network.CheckConnectInternet())
        {
            networkPanel.SetActive(false);
        }
        else
        {
            #if UNITY_EDITOR
                        UnityEditor.EditorApplication.isPlaying = false;
            #else
                    Application.Quit();
            #endif
        }
    }
}
