using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[CustomEditor(typeof(PlayerBaseStat))]
public class BaseStatEditor : Editor
{
    SerializedProperty baseSpeed;
    SerializedProperty baseAttackSpeed;
    SerializedProperty baseAttackRange;


    void OnEnable()
    {
        // SerializedProperty �ʱ�ȭ
        baseSpeed = serializedObject.FindProperty("baseSpeed");
        baseAttackSpeed = serializedObject.FindProperty("baseAttackSpeed");
        baseAttackRange = serializedObject.FindProperty("baseAttackRange");
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


        // ���� ���� ����
        serializedObject.ApplyModifiedProperties();

        PlayerBaseStat stat = (PlayerBaseStat)target;

        if (GUILayout.Button("����"))
        {
            stat.NotifySettingsChange();
            EditorUtility.SetDirty(stat);
        }


        EditorGUILayout.Space(20);

        EditorGUILayout.LabelField("�÷��̾��� �⺻ Hp / Hpȸ�� / ���ݷ� / ���� / ġ��ŸȮ�� / ġ��Ÿ���� ����", style);

    }

    void DrawDescriptionLabel(string description)
    {
        EditorGUILayout.BeginHorizontal();
        GUILayout.Space(EditorGUIUtility.labelWidth);
        EditorGUILayout.LabelField(description);
        EditorGUILayout.EndHorizontal();
    }
}
