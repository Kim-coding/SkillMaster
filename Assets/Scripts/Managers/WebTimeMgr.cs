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

    public void CurrentTime()  // �ܺο��� (Ư�� ���� ����)�ֱ������� ���� �ð��� ���� �ð����� �����ϱ� ���� ��
    {
        if (network.CheckConnectInternet())
        {
            StartCoroutine(GetEndTime());
        }
    }

    private IEnumerator GetStartTime()  //���� �ð�
    {
        yield return null;
        //SaveStartTime();
    }

    private IEnumerator GetEndTime()    // ���� �ð�
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

    public static TimeSpan GetInactiveDiration() // TimeSpan�� �� ��¥�� �ð� ������ ������ ��Ÿ���� ����ü
    {
        return TimeSpan.Zero;
    }
}
