using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

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

    void Start()
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

    public void CurrentTime()  // �ܺο��� (Ư�� ���� ����)�ֱ������� ���� �ð��� ���� �ð����� �����ϱ� ���� ��
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
            DateTime serverTime = DateTime.Parse(worldTime.datetime);
            Debug.Log("���� �ð� (���� �ð�): " + serverTime);
            SaveStartTime(serverTime);
        }
    }

    public IEnumerator GetEndTime()    // ���� �ð�
    {
        UnityWebRequest request = UnityWebRequest.Get(WorldTimeApiUrl);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("�ð� �������� ����");
        }
        else
        {
            var jsonResult = request.downloadHandler.text;
            var worldTime = JsonUtility.FromJson<WorldTimeApiResponse>(jsonResult);
            DateTime serverTime = DateTime.Parse(worldTime.datetime);
            Debug.Log("���� �ð� (���� �ð�): " + serverTime);
            SaveEndTime(serverTime);
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

    public static TimeSpan GetInactiveDiration() // TimeSpan�� �� ��¥�� �ð� ������ ������ ��Ÿ���� ����ü
    {
        return TimeSpan.Zero;
    }

    public void CalculateInactiveDuration()
    {
        TimeData timeData = LoadTimeData();
        if (timeData != null && !string.IsNullOrEmpty(timeData.endTime))
        {
            DateTime lastEndTime;
            if (DateTime.TryParse(timeData.endTime, out lastEndTime))
            {
                DateTime currentTime = DateTime.UtcNow;
                TimeSpan inactiveDuration = currentTime - lastEndTime;
                Debug.Log("��Ȱ�� �Ⱓ: " + inactiveDuration.TotalMinutes + "��");
            }
        }
    }

    [Serializable]
    private class WorldTimeApiResponse
    {
        public string datetime;
    }
}
