using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DebugMode : MonoBehaviour
{
    public TextMeshProUGUI fpsText; // 화면에 FPS를 표시할 UI 텍스트

    private float deltaTime = 0.0f;
    public PlayerAI player;
    public PlayerBaseStat playerStat;

    public Slider speedSlider;
    public float speedValue;
    public TextMeshProUGUI speedText;

    public Slider cooldownSlider;
    public float cooldownValue;
    public TextMeshProUGUI cooldownText;

    public Slider attackSpeedSlider;
    public float attackSpeedValue;
    public TextMeshProUGUI attackSpeedText;

    public Slider attackRangeSlider;
    public float attackRangeValue;
    public TextMeshProUGUI attackRangeText;

    public Slider recoveryDurationSlider;
    public float recoveryDurationValue;
    public TextMeshProUGUI recoveryDurationText;

    private void Awake()
    {
        speedSlider.value = playerStat.baseSpeed;
        cooldownSlider.value = playerStat.baseCooldown;
        attackSpeedSlider.value = playerStat.baseAttackSpeed;
        attackRangeSlider.value = playerStat.baseAttackRange;
        recoveryDurationSlider.value = playerStat.baseRecoveryDuration;
    }

    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        fpsText.text = string.Format("{0:0.} FPS", fps);

        speedValue = Mathf.Round(speedSlider.value * 100f) / 100f;
        cooldownValue = Mathf.Round(cooldownSlider.value * 100f) / 100f;
        attackSpeedValue = Mathf.Round(attackSpeedSlider.value * 100f) / 100f;
        attackRangeValue = Mathf.Round(attackRangeSlider.value * 100f) / 100f;
        recoveryDurationValue = Mathf.Round(recoveryDurationSlider.value * 100f) / 100f;

        speedText.text = speedValue.ToString();
        cooldownText.text = cooldownValue.ToString();
        attackSpeedText.text = attackSpeedValue.ToString();
        attackRangeText.text = attackRangeValue.ToString();
        recoveryDurationText.text = recoveryDurationValue.ToString();


        if(Input.GetKeyDown(KeyCode.S))
        {
            GameMgr.Instance.playerMgr.currency.AddGold(new BigInteger(1000000000));
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
           var player = GameMgr.Instance.playerMgr.characters[0].GetComponent<CharacterStat>();

            Debug.Log("ATT : " + player.attackPower.ToString());
            Debug.Log("DEF : " + player.Defence.ToString());
            Debug.Log("MHE : " + player.maxHealth.ToString());
            Debug.Log("REC : " + player.playerHealthRecovery.ToString());
            Debug.Log("CRP : " + player.playerCriticalPercent.ToString());
            Debug.Log("CRM : " + player.playerCriticalMultiple.ToString());
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            Debug.Log(GameMgr.Instance.playerMgr.playerinventory.itemAttackPower);
        }

        GameMgr.Instance.playerMgr.playerStat.DebugStatSetting(speedValue, cooldownValue, attackSpeedValue ,attackRangeValue, recoveryDurationValue);
    }
}
