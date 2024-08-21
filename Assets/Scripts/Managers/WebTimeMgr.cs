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
                Debug.Log("인터넷을 연결 해 주세요.");
            }
        }
    }

    public void SaveTime()  // 외부에서 (특정 시점 마다)주기적으로 현재 시간을 종료 시간으로 저장하기 위한 것
    {
        if (network.CheckConnectInternet())
        {
            StartCoroutine(GetEndTime());
        }
        else
        {
            Debug.Log("인터넷을 연결 해 주세요.");
        }
    }

    public IEnumerator GetStartTime()  //시작 시간
    {
        UnityWebRequest request = UnityWebRequest.Get(SeoulTimeApiUrl);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("시간 가져오기 실패: " + request.error);
        }
        else
        {
            var jsonResult = request.downloadHandler.text;
            var worldTime = JsonUtility.FromJson<WorldTimeApiResponse>(jsonResult);

            if (worldTime != null && !string.IsNullOrEmpty(worldTime.dateTime))
            {
                startTime = DateTime.Parse(worldTime.dateTime);
                Debug.Log("서버 시간 (시작 시간): " + startTime);
                SaveStartTime(startTime);
                OfflineDuration();
            }
            else
            {
                Debug.LogError("서버 응답에서 dateTime 필드가 비어있음.");
            }
        }
    }

    public IEnumerator GetEndTime()  // 종료 시간 비동기적으로 가져오기
    {
        UnityWebRequest request = UnityWebRequest.Get(SeoulTimeApiUrl);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("시간 가져오기 실패: " + request.error);
        }
        else
        {
            var jsonResult = request.downloadHandler.text;
            var worldTime = JsonUtility.FromJson<WorldTimeApiResponse>(jsonResult);

            if (worldTime != null && !string.IsNullOrEmpty(worldTime.dateTime))
            {
                DateTime endTime = DateTime.Parse(worldTime.dateTime);
                Debug.Log("서버 시간 (마지막 시간): " + endTime);
                SaveEndTime(endTime);
            }
            else
            {
                Debug.LogError("서버 응답에서 dateTime 필드가 비어있음.");
            }
        }
    }
    //private DateTime FetchEndTime()
    //{
    //    using (HttpClient client = new HttpClient())
    //    {
    //        HttpResponseMessage response = client.GetAsync(SeoulTimeApiUrl).Result;

    //        if (response.IsSuccessStatusCode)
    //        {
    //            string jsonResult = response.Content.ReadAsStringAsync().Result;
    //            var worldTime = JsonUtility.FromJson<WorldTimeApiResponse>(jsonResult);
    //            return DateTime.Parse(worldTime.dateTime);
    //        }
    //        else
    //        {
    //            throw new Exception("네트워크 요청 실패: " + response.ReasonPhrase);
    //        }
    //    }
    //}

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
                Debug.Log("마지막 종료 시간" + timeData.endTime);
                TimeSpan inactiveDuration = startTime - lastEndTime;
                if(inactiveDuration.Minutes > 0)
                {
                    GameMgr.Instance.rewardMgr.OfflineRewardPopUp(inactiveDuration);
                }
            }
        }
    }

    [System.Serializable]
    private class WorldTimeApiResponse
    {
        public string dateTime;
    }
}
