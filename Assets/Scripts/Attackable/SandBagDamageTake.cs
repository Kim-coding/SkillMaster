using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SandBagDamageTake : MonoBehaviour, IAttackable
{
    private BigInteger maxDamage;
    private GoldDungeonData currentStageData;
    private DungeonScene dungeonScene;
    private string currentRequestDamage;
    private string nextRequestDamage;

    private bool nextLv = false;

    private void Start()
    {
        dungeonScene = GameMgr.Instance.sceneMgr.dungeonScene;
        currentStageData = DataTableMgr.Get<GoldDungeonTable>(DataTableIds.goldDungeon).GetID(dungeonScene.currentStage);
        currentRequestDamage = currentStageData.request_damage;
        maxDamage = new BigInteger(currentRequestDamage);
    }

    public void OnAttack(GameObject attacker, GameObject defender, Attack attack)
    {
        var score = gameObject.GetComponent<IDamageable>();

        if (nextRequestDamage != null && nextLv)
        {
            maxDamage = new BigInteger(int.Parse(nextRequestDamage) - int.Parse(currentRequestDamage));//인트로 변환하는 건 임시 방편이다.
            currentRequestDamage = nextRequestDamage;
            nextLv = false;
            Debug.Log("maxDamage : "+maxDamage.ToString());
        }

        score.Health += attack.Damage;
        Debug.Log(score.Health);

        dungeonScene.currentScore += attack.Damage;
        Debug.Log(dungeonScene.currentScore);

        gameObject.GetComponent<DamageDisplay>().DisplayText(attack);

        GameMgr.Instance.uiMgr.ScoreSliderUpdate(score.Health, maxDamage);

        if (score.Health >= maxDamage)
        {
            dungeonScene.currentStage++;
            var nextStageData = DataTableMgr.Get<GoldDungeonTable>(DataTableIds.goldDungeon).GetID(dungeonScene.currentStage);

            nextRequestDamage = nextStageData.request_damage;

            GameMgr.Instance.uiMgr.InitializeNextStageSlider(dungeonScene.currentStage - 1);
            score.Health -= maxDamage;
            nextLv = true;
        }

        gameObject.GetComponent<DamageDisplay>().DisplayText(attack);
    }
}
