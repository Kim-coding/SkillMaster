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

    public Slider attackSpeedSlider;
    public float attackSpeedValue;
    public TextMeshProUGUI attackSpeedText;

    public Slider attackRangeSlider;
    public float attackRangeValue;
    public TextMeshProUGUI attackRangeText;

    private void Awake()
    {
        speedSlider.value = playerStat.baseSpeed;
        attackSpeedSlider.value = playerStat.baseAttackSpeed;
        attackRangeSlider.value = playerStat.baseAttackRange;
    }

    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        fpsText.text = string.Format("{0:0.} FPS", fps);

        speedValue = Mathf.Round(speedSlider.value * 100f) / 100f;
        attackSpeedValue = Mathf.Round(attackSpeedSlider.value * 100f) / 100f;
        attackRangeValue = Mathf.Round(attackRangeSlider.value * 100f) / 100f;

        speedText.text = speedValue.ToString();
        attackSpeedText.text = attackSpeedValue.ToString();
        attackRangeText.text = attackRangeValue.ToString();

        GameMgr.Instance.playerMgr.playerStat.DebugStatSetting(speedValue, attackSpeedValue, attackRangeValue);
    }
}
