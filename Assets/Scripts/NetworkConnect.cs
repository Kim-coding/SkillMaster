using UnityEngine;

public class NetworkConnect : MonoBehaviour
{
    private bool isConnect = false;

    public bool CheckConnectInternet()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            isConnect = false;  //���ͳ� X
        }
        else
        {
            isConnect = true;   //���ͳ� ��
        }

        return isConnect;
    }
}
