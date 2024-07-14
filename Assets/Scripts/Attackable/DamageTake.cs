using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MonsterDamageTake : MonoBehaviour, IAttackable
{

    private MonsterAI monster;

    private void Awake()
    {
        monster = GetComponent<MonsterAI>();
    }

    public void OnAttack(GameObject attacker, GameObject defender, Attack attack)
    {
        monster.monsterStat.health -= attack.Damage; // 데미지 감소 곱해서 빼야함
        //Debug.Log(monster.health.ToString());
        if (monster.monsterStat.health.factor == 1 && monster.monsterStat.health.numberList[0] <= 0 && !monster.onDeath)
        {
            monster.monsterStat.health.Clear();
            monster.onDeath = true;
            var destructibles = GetComponents<IDestructible>();
            foreach (var destructible in destructibles)
            {
                destructible.OnDestruction(attacker);
            }
        }
        Vector3 deathPosition = transform.position; // 몬스터의 죽음 위치
        float moveDuration = 1.0f; // 이동 지속 시간

        // 골드 생성 및 이동 호출
        CreateAndMoveGold(deathPosition, moveDuration);
    }

    void CreateAndMoveGold(Vector3 position, float duration)
    {
        // 골드 프리팹 인스턴스 생성
        GameObject goldObject = Instantiate(GameMgr.Instance.goldManager.goldPrefab, position, Quaternion.identity);
        GoldMovement goldMovement = goldObject.GetComponent<GoldMovement>();

        // GoldMovement 스크립트 초기화
        goldMovement.Initialize(GameMgr.Instance.goldManager.mainCamera, GameMgr.Instance.goldManager.uiTarget, duration);

        // 골드 매니저에 등록
        GameMgr.Instance.goldManager.StartMovingGold(goldMovement, duration);
    }
}
