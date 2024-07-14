using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[CustomEditor(typeof(PlayerBaseStat))]
public class BaseStatEditor : Editor
{
    SerializedProperty baseSpeed;
    SerializedProperty baseAttackSpeed;
    SerializedProperty baseAttackRange;

    SerializedProperty basePlayerAttackPower;
    SerializedProperty basePlayerDefence;
    SerializedProperty basePlayerMaxHealth;
    SerializedProperty basePlayerHealthRecovery;
    SerializedProperty basePlayerCriticalPercent;
    SerializedProperty basePlayerCriticalMultiple;


    void OnEnable()
    {
        // SerializedProperty �ʱ�ȭ
        baseSpeed = serializedObject.FindProperty("baseSpeed");
        baseAttackSpeed = serializedObject.FindProperty("baseAttackSpeed");
        baseAttackRange = serializedObject.FindProperty("baseAttackRange");

        basePlayerAttackPower = serializedObject.FindProperty("basePlayerAttackPower");
        basePlayerDefence = serializedObject.FindProperty("basePlayerDefence");
        basePlayerMaxHealth = serializedObject.FindProperty("basePlayerMaxHealth");
        basePlayerHealthRecovery = serializedObject.FindProperty("basePlayerHealthRecovery");
        basePlayerCriticalPercent = serializedObject.FindProperty("basePlayerCriticalPercent");
        basePlayerCriticalMultiple = serializedObject.FindProperty("basePlayerCriticalMultiple");
    }
    public override void OnInspectorGUI()
    {
        GUIStyle style = new GUIStyle(EditorStyles.helpBox);
        style.fontSize = 14;
        style.padding = new RectOffset(10, 10, 10, 10);
        style.wordWrap = true;
        EditorGUILayout.Space();
        // ���� �ؽ�Ʈ ǥ��
        EditorGUILayout.LabelField("�÷��̾��� �⺻ �̵��ӵ� / ���ݼӵ� / ���� ���� ����", style);


        // ���� �ؽ�Ʈ
       // EditorGUILayout.HelpBox("�÷��̾��� �⺻ �̵��ӵ� / ���ݼӵ� / ���� ���� ����", MessageType.Info);

        serializedObject.Update();


        // �� �ʵ忡 ���� ������ ���������� ������
        EditorGUILayout.BeginVertical();
        EditorGUILayout.Space(10);

        EditorGUILayout.Slider(baseSpeed, 0f, 10f, new GUIContent("�̵� �ӵ�"));
        DrawDescriptionLabel("�÷��̾� ĳ������ �⺻ �̵� �ӵ�");

        EditorGUILayout.Slider(baseAttackSpeed, 0f, 10f, new GUIContent("���� �ӵ�"));
        DrawDescriptionLabel("�÷��̾� ĳ������ �⺻ ���� �ӵ�");

        EditorGUILayout.Slider(baseAttackRange, 0f, 10f, new GUIContent("���� ����"));
        DrawDescriptionLabel("�÷��̾� ĳ������ �⺻ ���� ����");
        EditorGUILayout.EndVertical();

        EditorGUILayout.Space(20);
        EditorGUILayout.LabelField("�÷��̾��� �⺻ Hp / Hpȸ�� / ���ݷ� / ���� / ġ��ŸȮ�� / ġ��Ÿ���� ����", style);
        EditorGUILayout.Space(10);


        EditorGUILayout.PropertyField(basePlayerAttackPower, new GUIContent("�⺻ ���ݷ�"));
        DrawDescriptionLabel("�÷��̾� ĳ������ �⺻ ���ݷ�");

        EditorGUILayout.PropertyField(basePlayerDefence, new GUIContent("�⺻ ����"));
        DrawDescriptionLabel("�÷��̾� ĳ������ �⺻ ����");

        EditorGUILayout.PropertyField(basePlayerMaxHealth, new GUIContent("�⺻ �ִ� ü��"));
        DrawDescriptionLabel("�÷��̾� ĳ������ �⺻ �ִ� ü��");

        EditorGUILayout.PropertyField(basePlayerHealthRecovery, new GUIContent("�⺻ ü�� ȸ����"));
        DrawDescriptionLabel("�÷��̾� ĳ������ �⺻ ü�� ȸ����");

        EditorGUILayout.PropertyField(basePlayerCriticalPercent, new GUIContent("�⺻ ġ��Ÿ Ȯ��"));
        DrawDescriptionLabel("�÷��̾� ĳ������ �⺻ ġ��Ÿ Ȯ��");

        EditorGUILayout.PropertyField(basePlayerCriticalMultiple, new GUIContent("�⺻ ġ��Ÿ ����"));
        DrawDescriptionLabel("�÷��̾� ĳ������ �⺻ ġ��Ÿ ����");

        // ���� ���� ����
        serializedObject.ApplyModifiedProperties();

        PlayerBaseStat stat = (PlayerBaseStat)target;
        EditorGUILayout.Space(20);

        if (GUILayout.Button("����"))
        {
            stat.NotifySettingsChange();
            EditorUtility.SetDirty(stat);
        }

    }

    void DrawDescriptionLabel(string description)
    {
        EditorGUILayout.BeginHorizontal();
        GUILayout.Space(EditorGUIUtility.labelWidth);
        EditorGUILayout.LabelField(description);
        EditorGUILayout.EndHorizontal();
    }
}
