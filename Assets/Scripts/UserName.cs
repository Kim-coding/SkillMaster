using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UserName : MonoBehaviour
{
    public GameObject namePanel;
    public TMP_InputField inputName;
    public Button OkButton;

    public string userName = "¹æ¶ûÀÚ";

    public GameObject panel;
    public Button yesButton;
    public Button noButton;

    public TextMeshProUGUI myName;

    private void Start()
    {
        OkButton.onClick.AddListener(Open);
        yesButton.onClick.AddListener(SaveName);
        noButton.onClick.AddListener(Back);
    }

    public void Back()
    {
        panel.SetActive(false);
    }
    public void Open()
    {
        panel.SetActive(true);
        myName.text = inputName.text;
    }

    public void OpenPanel()
    {
        namePanel.SetActive(true);
    }

    private void SaveName()
    {
        var input = inputName.text;

        if (IsValidName(input))
        {
            userName = input;
            SaveLoadSystem.CurrSaveData.savePlay.userName = userName;
            SaveLoadSystem.Save();
            GameMgr.Instance.uiMgr.userNameText.text = userName;
            namePanel.SetActive(false);
        }
    }

    private bool IsValidName(string name)
    {
        if (Regex.IsMatch(name, @"\s"))
        {
            return false;
        }
        if (inputName.text.Equals(Regex.Replace(inputName.text, @"[^\w\.@-]", "", RegexOptions.Singleline)) != true)
        {
            return false;
        }
        if (name.Length >= 1 && name.Length <= 3)
        {
            return true;
        }

        return false;
    }
}