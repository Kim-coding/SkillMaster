using System;
using System.Collections;
using UnityEngine;

public class WebTimeMgr : MonoBehaviour
{
    private const string WorldTimeApiUrl = "http://worldtimeapi.org/api/timezone/Etc/UTC";
    private NetworkConnect network;

    void Start()
    {
        network = GameMgr.Instance.networkConnect;
        if (network.CheckConnectInternet())
        {
            StartCoroutine(GetStartTime());
        }
    }

    private void OnApplicationQuit()
    {
        CurrentTime();
    }

    public void CurrentTime()  // 외부에서 (특정 시점 마다)주기적으로 현재 시간을 종료 시간으로 저장하기 위한 것
    {
        if (network.CheckConnectInternet())
        {
            StartCoroutine(GetEndTime());
        }
    }

    private IEnumerator GetStartTime()  //시작 시간
    {
        yield return null;
        //SaveStartTime();
    }

    private IEnumerator GetEndTime()    // 종료 시간
    {
        yield return null;
        //SaveEndTime();
    }

    private void SaveStartTime(DateTime startTime)
    {

    }
    private void SaveEndTime(DateTime endTime)
    {

    }

    public static TimeSpan GetInactiveDiration() // TimeSpan은 두 날짜나 시간 사이의 간격을 나타내는 구조체
    {
        return TimeSpan.Zero;
    }
}
