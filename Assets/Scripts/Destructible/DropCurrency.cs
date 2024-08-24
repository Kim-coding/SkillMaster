using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropCurrency : MonoBehaviour, IDestructible
{
    Vector3 deathPosition;
    private float moveDuration = 1.0f;
    private string goldValue;
    private string diaValue;
    public void OnDestruction(GameObject attacker)
    {
        deathPosition = gameObject.transform.position;
        Vector3 screenPosition = Vector3.zero;
        if (Camera.main != null)
        {
            screenPosition = Camera.main.WorldToScreenPoint(deathPosition);
        }
        else
        {
            screenPosition = new Vector2(Screen.width / 2f, Screen.height / 2f);
        }

        RectTransformUtility.ScreenPointToLocalPointInRectangle(GameMgr.Instance.dropItemMgr.parent as RectTransform, screenPosition, null, out Vector2 uiPosition);

        if (gameObject.GetComponent<BossAI>() != null)
        {
            diaValue = gameObject.GetComponent<BossAI>().bossStat.dropDia;
            goldValue = gameObject.GetComponent<BossAI>().bossStat.dropGold;
            GameMgr.Instance.dropItemMgr.CreateAndMoveGold(uiPosition, moveDuration, goldValue);
            if(diaValue != "-1")
            { 
                GameMgr.Instance.dropItemMgr.CreateAndMoveDia(uiPosition, moveDuration, diaValue); 
            }
        } //TO-DO 보스 보상
        if (gameObject.GetComponent<MonsterAI>() != null)
        {
            goldValue = gameObject.GetComponent<MonsterAI>().monsterStat.dropGold;
            GameMgr.Instance.dropItemMgr.CreateAndMoveGold(uiPosition, moveDuration, goldValue);
        }
    }
}
