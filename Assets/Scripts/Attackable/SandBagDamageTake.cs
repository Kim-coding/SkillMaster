using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandBagDamageTake : MonoBehaviour, IAttackable
{
    public void OnAttack(GameObject attacker, GameObject defender, Attack attack)
    {
        var score  = gameObject.GetComponent<IDamageable>();
        score.Health += attack.Damage;
        
        gameObject.GetComponent<DamageDisplay>().DisplayText(attack);

        var dungdonScene = GameMgr.Instance.sceneMgr.dungeonScene;
        var currentStageData = DataTableMgr.Get<GoldDungeonTable>(DataTableIds.goldDungeon).GetID(dungdonScene.currentStage);

        GameMgr.Instance.uiMgr.ScoreSliderUpdate(score.Health, new BigInteger(currentStageData.request_damage));

        if (score.Health >= new BigInteger(currentStageData.request_damage))
        {
            dungdonScene.currentStage++;
            var nextStageData = DataTableMgr.Get<GoldDungeonTable>(DataTableIds.goldDungeon).GetID(dungdonScene.currentStage);
            
            GameMgr.Instance.uiMgr.InitializeNextStageSlider(new BigInteger(nextStageData.request_damage) - new BigInteger(currentStageData.request_damage));
        }

        gameObject.GetComponent<DamageDisplay>().DisplayText(attack);
    }
}
