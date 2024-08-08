using UnityEngine;

public class LinearRangeAttackSkill : MonoBehaviour, ISkillComponent, ISkill
{
    public string skillID = "LinearRangeAttackSkill"; 
    public GameObject skillObject;
    public GameObject attacker;
    public Attack attack;
    public DamageType damageType;
    private GameObject skillEffectObject;
    private string skillEffect;

    float duration = 0.5f;
    float timer = 0f;
    float angle;
    float skillEffectAngle;
    public void Initialize()
    {
        timer = 0f;
        //skillID = 0;
        skillObject = null;
        //�ʱ�ȭ �� �ʿ��� ������ �۾�
    }

    public void ApplyShape(GameObject skillObject, Vector3 launchPoint, GameObject target, float speed, float y, float x, int skillPropertyID, string skillEffect) //��ų�� ���¿� ��ġ�� ����
    {
        this.skillObject = skillObject;
        this.skillEffect = skillEffect;

        skillObject.transform.localScale = new Vector3(x, y, 1);

        skillObject.AddComponent<BoxCollider2D>();
        skillObject.GetComponent<BoxCollider2D>().isTrigger = true;
        
        var rot = (target.transform.position - launchPoint).normalized;

        angle = Mathf.Atan2(rot.y, rot.x) * Mathf.Rad2Deg;
        skillObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        skillObject.transform.position = launchPoint + rot * (x);

        skillEffectAngle = (-angle + 90);
    }

    public void ApplyDamageType(GameObject attacker, Attack attack, DamageType damageType, SkillShapeType shapeType) //��ų�� ������ �Ӽ��� ����
    {
        this.attacker = attacker;
        this.attack = attack;
        this.damageType = damageType;

        GameObject skillEffectPrefab = Resources.Load<GameObject>($"SkillEffects/{skillEffect}");
        if (skillEffectPrefab != null)
        {
            skillEffectObject = Instantiate(skillEffectPrefab, skillObject.transform.position, Quaternion.identity);
            skillEffectObject.transform.SetParent(skillObject.transform);
            if (skillEffectPrefab.GetComponent<Animator>() != null)
            {
                angle -= 90;
                skillEffectObject.transform.rotation = Quaternion.Euler(0, 0, angle);
                skillEffectObject.transform.localScale = new Vector3(1, 1.2f, 1);
            }
            else
            {
                var mainModule = skillEffectObject.GetComponent<ParticleSystem>().main;
                mainModule.startRotation = skillEffectAngle * Mathf.Deg2Rad;
            }
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            var monsterComponents = collision.GetComponents<IAttackable>();
            foreach (var monsterComponent in monsterComponents)
            {
                monsterComponent.OnAttack(attacker.gameObject, collision.gameObject, attack);
            }
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if(timer >= duration)
        {
            timer = 0;
            Destroy(skillEffectObject);
            Destroy(gameObject);
        }
    }
}
