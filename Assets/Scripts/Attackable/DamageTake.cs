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
        monster.monsterStat.health -= attack.Damage; // ������ ���� ���ؼ� ������
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
        Vector3 deathPosition = transform.position; // ������ ���� ��ġ
        float moveDuration = 1.0f; // �̵� ���� �ð�

        // ��� ���� �� �̵� ȣ��
        CreateAndMoveGold(deathPosition, moveDuration);
    }

    void CreateAndMoveGold(Vector3 position, float duration)
    {
        // ��� ������ �ν��Ͻ� ����
        GameObject goldObject = Instantiate(GameMgr.Instance.goldManager.goldPrefab, position, Quaternion.identity);
        GoldMovement goldMovement = goldObject.GetComponent<GoldMovement>();

        // GoldMovement ��ũ��Ʈ �ʱ�ȭ
        goldMovement.Initialize(GameMgr.Instance.goldManager.mainCamera, GameMgr.Instance.goldManager.uiTarget, duration);

        // ��� �Ŵ����� ���
        GameMgr.Instance.goldManager.StartMovingGold(goldMovement, duration);
    }
}
