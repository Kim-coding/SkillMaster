using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiEnhance : MonoBehaviour
{
    private PlayerEnhance p_E;
    public Enhance attackPowerUpgrade;
    public Enhance defenceUpgrade;
    public Enhance maxHealthUpgrade;
    public Enhance recoveryUpgrade;
    public Enhance criticalPercentUpgrade;
    public Enhance criticalMultipleUpgrade;

    public Enhance attackSpeedUpgrade;
    public Enhance speedUpgrade;
    public Enhance attackRangeUpgrade;

    private void Start()
    {
        attackPowerUpgrade.GetComponent<Button>().onClick.AddListener(GameMgr.Instance.playerMgr.playerEnhance.AddAttackPower);
        attackPowerUpgrade.Init("���ݷ� ��ȭ");
        defenceUpgrade.GetComponent<Button>().onClick.AddListener(GameMgr.Instance.playerMgr.playerEnhance.AddDefence);
        defenceUpgrade.Init("���� ��ȭ");
        maxHealthUpgrade.GetComponent<Button>().onClick.AddListener(GameMgr.Instance.playerMgr.playerEnhance.AddMaxHealth);
        maxHealthUpgrade.Init("�ִ� ü�� ��ȭ");
        recoveryUpgrade.GetComponent<Button>().onClick.AddListener(GameMgr.Instance.playerMgr.playerEnhance.AddRecovery);
        recoveryUpgrade.Init("ȸ���� ��ȭ");
        criticalPercentUpgrade.GetComponent<Button>().onClick.AddListener(GameMgr.Instance.playerMgr.playerEnhance.AddCriticalPercent);
        criticalPercentUpgrade.Init("ġ��Ÿ Ȯ�� ��ȭ");
        criticalMultipleUpgrade.GetComponent<Button>().onClick.AddListener(GameMgr.Instance.playerMgr.playerEnhance.AddCriticalMultiple);
        criticalMultipleUpgrade.Init("ġ��Ÿ ��� ��ȭ");

        attackSpeedUpgrade.GetComponent<Button>().onClick.AddListener(GameMgr.Instance.playerMgr.playerEnhance.AddAttackSpeed);
        attackSpeedUpgrade.Init("���� �ӵ� ��ȭ");
        speedUpgrade.GetComponent<Button>().onClick.AddListener(GameMgr.Instance.playerMgr.playerEnhance.AddSpeed);
        speedUpgrade.Init("�̵� �ӵ� ��ȭ");
        attackRangeUpgrade.GetComponent<Button>().onClick.AddListener(GameMgr.Instance.playerMgr.playerEnhance.AddAttackRange);
        attackRangeUpgrade.Init("��Ÿ� ��ȭ");

    }


    public void TextUpdate() //TO-DO enum ���� �Ű������� �޾Ƽ� �װŸ� ��������
    {
        p_E = GameMgr.Instance.playerMgr.playerEnhance; //TO-DO �Ź� ������Ʈ�� �ƴ϶� �ٸ����� �������

        attackPowerUpgrade.TextUpdate(
            p_E.attackPowerLevel,
            p_E.attackPowerLevel * p_E.attackPowerValue,
           (p_E.attackPowerLevel + 1) * p_E.attackPowerValue,
            p_E.attackPowerCost);

        defenceUpgrade.TextUpdate(
            p_E.defenceLevel,
            p_E.defenceLevel * p_E.defenceValue,
           (p_E.defenceLevel + 1) * p_E.defenceValue,
            p_E.defenceCost);

        maxHealthUpgrade.TextUpdate(
            p_E.maxHealthLevel,
            p_E.maxHealthLevel * p_E.maxHealthValue,
           (p_E.maxHealthLevel + 1) * p_E.maxHealthValue,
            p_E.maxHealthCost);

        recoveryUpgrade.TextUpdate(
            p_E.recoveryLevel,
            p_E.recoveryLevel * p_E.recoveryValue,
           (p_E.recoveryLevel + 1) * p_E.recoveryValue,
            p_E.recoveryCost);

        criticalPercentUpgrade.TextUpdate(
           p_E.criticalPercentLevel,
           p_E.criticalPercentLevel * p_E.criticalPercentValue,
          (p_E.criticalPercentLevel + 1) * p_E.criticalPercentValue,
           p_E.criticalPercentCost);

        criticalMultipleUpgrade.TextUpdate(
           p_E.criticalMultipleLevel,
           p_E.criticalMultipleLevel * p_E.criticalMultipleValue,
          (p_E.criticalMultipleLevel + 1) * p_E.criticalMultipleValue,
           p_E.criticalMultipleCost);


        attackSpeedUpgrade.TextUpdate(
           p_E.attackSpeedLevel,
           p_E.attackSpeedLevel * p_E.attackSpeedValue,
          (p_E.attackSpeedLevel + 1) * p_E.attackSpeedValue,
           p_E.attackSpeedCost);

        speedUpgrade.TextUpdate(
           p_E.speedLevel,
           p_E.speedLevel * p_E.speedValue,
          (p_E.speedLevel + 1) * p_E.speedValue,
           p_E.speedCost);

        attackRangeUpgrade.TextUpdate(
           p_E.attackRangeLevel,
           p_E.attackRangeLevel * p_E.attackRangeValue,
          (p_E.attackRangeLevel + 1) * p_E.attackRangeValue,
           p_E.attackRangeCost);

    }
}
