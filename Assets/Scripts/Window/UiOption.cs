using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiOption : MonoBehaviour
{
    public GameObject optionWindow;
    public GameObject configurationWindow;
    public GameObject exitWindow;

    //public GameObject LoadingPanel;

    public Button[] closeButtons;

    private Dictionary<Option_Windows, GameObject> windows;

    private void Awake()
    {
        windows = new Dictionary<Option_Windows, GameObject>()
        {
            {Option_Windows.Option, optionWindow},
            {Option_Windows.Configuration, configurationWindow },
            {Option_Windows.Exit, exitWindow },
        };
        foreach (var win in windows)
        {
            win.Value.SetActive(false);
        }
        optionWindow.SetActive(true);

        foreach (var button in closeButtons)
        {
            button.onClick.AddListener(CloseButton);
        }
    }

    private void OnEnable()
    {
        foreach (var win in windows)
        {
            win.Value.SetActive(false);
        }
        optionWindow.SetActive(true);
    }

    public void OnConfiguration()
    {
        GameMgr.Instance.soundMgr.PlaySFX("Button");
        foreach (var win in windows)
        {
            win.Value.SetActive(false);
        }
        configurationWindow.SetActive(true);
    }
    public void OnExit()
    {
        foreach (var win in windows)
        {
            win.Value.SetActive(false);
        }
        exitWindow.SetActive(true);
    }

    public void Exit()
    {
        //StartCoroutine(GameMgr.Instance.SaveAndQuit());
        GameMgr.Instance.SaveAndQuit();
    }

    public void CloseButton()
    {
        GameMgr.Instance.soundMgr.PlaySFX("Button");
        gameObject.SetActive(false);
    }

    public void QuitDungeon()
    {
        Addressables.LoadSceneAsync("MainScene", LoadSceneMode.Single);
    }
}
