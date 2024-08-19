using System;
using System.Collections;
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
    private const string WorldTimeApiUrl = "https://worldtimeapi.org/api/timezone/Etc/UTC";
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
            //StartCoroutine(GetEndTime());
            GetEndTime();
        }
        else
        {
            Debug.Log("���ͳ��� ���� �� �ּ���.");
        }
    }

    private IEnumerator GetStartTime()  //���� �ð�
    {
        UnityWebRequest request = UnityWebRequest.Get(WorldTimeApiUrl);
        yield return request.SendWebRequest();
        
        if(request.result  != UnityWebRequest.Result.Success)
        {
            Debug.Log("�ð� �������� ����");
        }
        else
        {
            var jsonResult = request.downloadHandler.text;
            var worldTime = JsonUtility.FromJson<WorldTimeApiResponse>(jsonResult);
            startTime = DateTime.Parse(worldTime.datetime);
            Debug.Log("���� �ð� (���� �ð�): " + startTime);
            SaveStartTime(startTime);
            OfflineDuration();
        }
    }

    public void GetEndTime()    // ���� �ð�
    {
        //UnityWebRequest request = UnityWebRequest.Get(WorldTimeApiUrl);
        //yield return request.SendWebRequest();

        //if (request.result != UnityWebRequest.Result.Success)
        //{
        //    Debug.Log("�ð� �������� ����");
        //}
        //else
        //{
        //    var jsonResult = request.downloadHandler.text;
        //    var worldTime = JsonUtility.FromJson<WorldTimeApiResponse>(jsonResult);
        //    DateTime serverTime = DateTime.Parse(worldTime.datetime);
        //    Debug.Log("���� �ð� (������ �ð�): " + serverTime);
        //    SaveEndTime(serverTime);
        //}
        DateTime endTime = FetchEndTime();
        Debug.Log("���� �ð� (������ �ð�): " + endTime);
        SaveEndTime(endTime);
    }
    private DateTime FetchEndTime()
    {
        using (HttpClient client = new HttpClient())
        {
            HttpResponseMessage response = client.GetAsync(WorldTimeApiUrl).Result; // ���������� ��û�� ����

            if (response.IsSuccessStatusCode)
            {
                string jsonResult = response.Content.ReadAsStringAsync().Result;
                var worldTime = JsonUtility.FromJson<WorldTimeApiResponse>(jsonResult);
                return DateTime.Parse(worldTime.datetime);
            }
            else
            {
                throw new Exception("��Ʈ��ũ ��û ����: " + response.ReasonPhrase);
            }
        }
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
        if (timeData != null && !string.IsNullOrEmpty(timeData.endTime))
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
    }

    [Serializable]
    private class WorldTimeApiResponse
    {
        public string datetime;
    }
}
