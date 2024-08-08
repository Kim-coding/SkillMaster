using UnityEngine;

public class NetworkConnect : MonoBehaviour
{
    private bool isConnect = false;

    public bool CheckConnectInternet()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            isConnect = false;  //인터넷 X
        }
        else
        {
            isConnect = true;   //인터넷 ㅇ
        }

        return isConnect;
    }
}
