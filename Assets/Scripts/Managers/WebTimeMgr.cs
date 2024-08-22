using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

[Serializable]
public class TimeData
{
    public string startTime;
    public string endTime;
}

public class WebTimeMgr : MonoBehaviour
{
    private const string SeoulTimeApiUrl = "https://timeapi.io/api/Time/current/zone?timeZone=Asia/Seoul";
    private const string UtcTimeApiUrl = "https://worldtimeapi.org/api/timezone/Etc/UTC";

    private NetworkConnect network;
    private string dataPath;
    private DateTime startTime;

    private static WebTimeMgr instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            GameMgr.Instance.FindWebTime();
            return;
        }
    }

    public void Start()
    {
        if (network == null)
        {
            network = GameMgr.Instance.networkConnect;
            if (network.CheckConnectInternet())
            {
                dataPath = Path.Combine(Application.persistentDataPath, "timeData.json");
                StartCoroutine(GetStartTime());
            }
            else
            {
                Debug.Log("���ͳ��� ���� �� �ּ���.");
            }
        }
    }

    public void SaveTime()  // �ܺο��� (Ư�� ���� ����)�ֱ������� ���� �ð��� ���� �ð����� �����ϱ� ���� ��
    {
        if (network.CheckConnectInternet())
        {
            StartCoroutine(GetEndTime());
        }
        else
        {
            Debug.Log("���ͳ��� ���� �� �ּ���.");
        }
    }

    public IEnumerator GetStartTime()  //���� �ð�
    {
        UnityWebRequest request = UnityWebRequest.Get(SeoulTimeApiUrl);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("�ð� �������� ����: " + request.error);
        }
        else
        {
            var jsonResult = request.downloadHandler.text;
            var worldTime = JsonUtility.FromJson<WorldTimeApiResponse>(jsonResult);

            if (worldTime != null && !string.IsNullOrEmpty(worldTime.dateTime))
            {
                startTime = DateTime.Parse(worldTime.dateTime);
                Debug.Log("���� �ð� (���� �ð�): " + startTime);
                SaveStartTime(startTime);
                OfflineDuration();
            }
            else
            {
                Debug.LogError("���� ���信�� dateTime �ʵ尡 �������.");
            }
        }
    }

    public IEnumerator GetEndTime()  // ���� �ð� �񵿱������� ��������
    {
        UnityWebRequest request = UnityWebRequest.Get(SeoulTimeApiUrl);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("�ð� �������� ����: " + request.error);
        }
        else
        {
            var jsonResult = request.downloadHandler.text;
            var worldTime = JsonUtility.FromJson<WorldTimeApiResponse>(jsonResult);

            if (worldTime != null && !string.IsNullOrEmpty(worldTime.dateTime))
            {
                DateTime endTime = DateTime.Parse(worldTime.dateTime);
                Debug.Log("���� �ð� (������ �ð�): " + endTime);
                SaveEndTime(endTime);
            }
            else
            {
                Debug.LogError("���� ���信�� dateTime �ʵ尡 �������.");
            }
        }
    }

    public IEnumerator GetExitTime()  // ���� �ð� �񵿱������� ��������
    {
        UnityWebRequest request = UnityWebRequest.Get(SeoulTimeApiUrl);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("�ð� �������� ����: " + request.error);
        }
        else
        {
            var jsonResult = request.downloadHandler.text;
            var worldTime = JsonUtility.FromJson<WorldTimeApiResponse>(jsonResult);

            if (worldTime != null && !string.IsNullOrEmpty(worldTime.dateTime))
            {
                DateTime endTime = DateTime.Parse(worldTime.dateTime);
                Debug.Log("���� �ð� (������ �ð�): " + endTime);
                SaveEndTime(endTime);
            }
            else
            {
                Debug.LogError("���� ���信�� dateTime �ʵ尡 �������.");
            }
        }
        Exit();
    }

    private void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void SaveStartTime(DateTime startTime)
    {
        TimeData timeData = LoadTimeData();
        if (timeData == null)
        {
            timeData = new TimeData();
        }
        timeData.startTime = startTime.ToString();
        SaveTimeData(timeData);
    }

    private void SaveEndTime(DateTime endTime)
    {
        TimeData timeData = LoadTimeData();
        if (timeData == null)
        {
            timeData = new TimeData();
        }
        timeData.endTime = endTime.ToString();
        SaveTimeData(timeData);
    }

    private TimeData LoadTimeData()
    {
        if(File.Exists(dataPath))
        {
            string jsonData = File.ReadAllText(dataPath);
            return JsonUtility.FromJson<TimeData>(jsonData);
        }
        return null;
    }

    private void SaveTimeData(TimeData timeData)
    {
        string jsonData = JsonUtility.ToJson(timeData, true);
        File.WriteAllText(dataPath, jsonData);
    }

    public void OfflineDuration()
    {
        TimeData timeData = LoadTimeData();
        if (timeData != null && !string.IsNullOrEmpty(timeData.endTime))  // �������� ����
        {
            DateTime lastEndTime;
            if (DateTime.TryParse(timeData.endTime, out lastEndTime))
            {
                Debug.Log("������ ���� �ð�" + timeData.endTime);
                TimeSpan inactiveDuration = startTime - lastEndTime;
                if(inactiveDuration.Minutes > 0)
                {
                    GameMgr.Instance.rewardMgr.OfflineRewardPopUp(inactiveDuration);
                }
            }
        }
        if (timeData != null && !string.IsNullOrEmpty(timeData.startTime) && !string.IsNullOrEmpty(timeData.endTime))  // ���� ����
        {
            DateTime lastEndTime;
            DateTime lastStartTime;
            if (DateTime.TryParse(timeData.endTime, out lastEndTime) && DateTime.TryParse(timeData.startTime, out lastStartTime))
            {
                if (lastStartTime.Date != lastEndTime.Date)
                {
                    MidnightReward();
                }
            }
        }
    }

    private void MidnightReward()
    {
        List<NormalItem> goldKeyItemList = new List<NormalItem> { };
        List<NormalItem> diaKeyItemList = new List<NormalItem> { };

        foreach (var item in GameMgr.Instance.playerMgr.playerinventory.playerNormalItemList)
        {
            if(item.itemName == "220003")
            {
                goldKeyItemList.Add(item);
            }
            if(item.itemName == "220004")
            {
                diaKeyItemList.Add(item);
            }
        }

        if(goldKeyItemList.Count == 0)
        {
            GameMgr.Instance.playerMgr.playerinventory.CreateItem(220003, 2, ItemType.misc);
        }
        else if (goldKeyItemList.Count == 1)
        {
            GameMgr.Instance.playerMgr.playerinventory.CreateItem(220003, 1, ItemType.misc);
        }

        if(diaKeyItemList.Count == 0)
        {
            GameMgr.Instance.playerMgr.playerinventory.CreateItem(220004, 2, ItemType.misc);
        }
        else if (diaKeyItemList.Count == 1)
        {
            GameMgr.Instance.playerMgr.playerinventory.CreateItem(220004, 1, ItemType.misc);
        }
    }

    [System.Serializable]
    private class WorldTimeApiResponse
    {
        public string dateTime;
    }
}
