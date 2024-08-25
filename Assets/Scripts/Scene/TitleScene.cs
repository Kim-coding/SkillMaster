using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
{
    public NetworkConnect network;
    public GameObject networkPanel;

    public void NetworkCheckAndNextScene()
    {
        if (!network.CheckConnectInternet())
        {
            networkPanel.gameObject.SetActive(true);
            return;
        }
        else
        {
            SceneManager.LoadScene("Loading");
        }
    }

    public void ClosePanel()
    {
        if (!network.CheckConnectInternet())
        {
            networkPanel.SetActive(false);
            return;
        }
        else
        {
            SceneManager.LoadScene("Loading");
        }
    }

    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

    public void DeleteData()
    {
        SaveLoadSystem.DeleteSaveData();
    }

}
