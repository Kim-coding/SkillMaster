using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;

public class ApiSpeedTest : MonoBehaviour
{
    //���� ����ð� �׽�Ʈ

    private const string SeoulTimeApiUrl = "https://timeapi.io/api/Time/current/zone?timeZone=Asia/Seoul";
    private const string UtcTimeApiUrl = "https://worldtimeapi.org/api/timezone/Etc/UTC";
    private bool isTesting = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && !isTesting)
        {
            isTesting = true;
            StartApiSpeedTest();
        }
    }

    async void StartApiSpeedTest()
    {
        TimeSpan seoulTime = await GetResponseTime(SeoulTimeApiUrl);
        UnityEngine.Debug.Log($"Seoul Time API ���� �ð�: {seoulTime.TotalMilliseconds} ms");

        TimeSpan utcTime = await GetResponseTime(UtcTimeApiUrl);
        UnityEngine.Debug.Log($"UTC Time API ���� �ð�: {utcTime.TotalMilliseconds} ms");

        if (seoulTime < utcTime)
        {
            UnityEngine.Debug.Log("Seoul Time API�� �� ����");
        }
        else
        {
            UnityEngine.Debug.Log("UTC Time API�� �� ����");
        }
        isTesting = false;
    }

    private async Task<TimeSpan> GetResponseTime(string url)
    {
        using (HttpClient client = new HttpClient())
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            stopwatch.Stop();
            return stopwatch.Elapsed;
        }
    }
}
