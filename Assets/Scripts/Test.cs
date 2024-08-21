using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Test : StateMachineBehaviour
{
    BatteryStatus batteryStatus;

    private void TestBattery()
    {
        BatteryStatus batteryStatus = SystemInfo.batteryStatus;

        float batteryLevel = SystemInfo.batteryLevel;

        Debug.Log("Battery Status: " + batteryStatus.ToString());

        int batteryPercentage = Mathf.RoundToInt(batteryLevel * 100);
        Debug.Log("Battery Level: " + batteryPercentage + "%");

        if (batteryStatus == BatteryStatus.Charging)
        {
            Debug.Log("Device is charging.");
        }
        else if (batteryStatus == BatteryStatus.Discharging)
        {
            Debug.Log("Device is discharging.");
        }
        else if (batteryStatus == BatteryStatus.NotCharging)
        {
            Debug.Log("Device is not charging.");
        }
        else if (batteryStatus == BatteryStatus.Full)
        {
            Debug.Log("Battery is full.");
        }
        else
        {
            Debug.Log("Battery status unknown.");
        }
    }

    // ��: ���� ���� �� ���͸� ������ Ȯ���ϴ� ���
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        TestBattery();
    }
}
