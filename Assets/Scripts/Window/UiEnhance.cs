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
        attackPowerUpgrade.Init("공격력 강화");
        defenceUpgrade.GetComponent<Button>().onClick.AddListener(GameMgr.Instance.playerMgr.playerEnhance.AddDefence);
        defenceUpgrade.Init("방어력 강화");
        maxHealthUpgrade.GetComponent<Button>().onClick.AddListener(GameMgr.Instance.playerMgr.playerEnhance.AddMaxHealth);
        maxHealthUpgrade.Init("최대 체력 강화");
        recoveryUpgrade.GetComponent<Button>().onClick.AddListener(GameMgr.Instance.playerMgr.playerEnhance.AddRecovery);
        recoveryUpgrade.Init("회복량 강화");
        criticalPercentUpgrade.GetComponent<Button>().onClick.AddListener(GameMgr.Instance.playerMgr.playerEnhance.AddCriticalPercent);
        criticalPercentUpgrade.Init("치명타 확률 강화");
        criticalMultipleUpgrade.GetComponent<Button>().onClick.AddListener(GameMgr.Instance.playerMgr.playerEnhance.AddCriticalMultiple);
        criticalMultipleUpgrade.Init("치명타 배률 강화");

        attackSpeedUpgrade.GetComponent<Button>().onClick.AddListener(GameMgr.Instance.playerMgr.playerEnhance.AddAttackSpeed);
        attackSpeedUpgrade.Init("공격 속도 강화");
        speedUpgrade.GetComponent<Button>().onClick.AddListener(GameMgr.Instance.playerMgr.playerEnhance.AddSpeed);
        speedUpgrade.Init("이동 속도 강화");
        attackRangeUpgrade.GetComponent<Button>().onClick.AddListener(GameMgr.Instance.playerMgr.playerEnhance.AddAttackRange);
        attackRangeUpgrade.Init("사거리 강화");

    }


    public void TextUpdate() //TO-DO enum 만들어서 매개변수로 받아서 그거만 업뎃하자
    {
        p_E = GameMgr.Instance.playerMgr.playerEnhance; //TO-DO 매번 업데이트가 아니라 다른데서 해줘야함

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
