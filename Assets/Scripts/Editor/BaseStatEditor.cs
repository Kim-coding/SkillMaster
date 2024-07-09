using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlayerBaseStat))]
public class BaseStatEditor : Editor
{
    SerializedProperty Speed;
    SerializedProperty AttackSpeed;
    SerializedProperty AttackRange;


    void OnEnable()
    {
        // SerializedProperty �ʱ�ȭ
        Speed = serializedObject.FindProperty("speed");
        AttackSpeed = serializedObject.FindProperty("attackSpeed");
        AttackRange = serializedObject.FindProperty("attackRange");
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

        EditorGUILayout.Slider(Speed, 0f, 10f, new GUIContent("�̵� �ӵ�"));
        DrawDescriptionLabel("�÷��̾� ĳ������ �⺻ �̵� �ӵ�");

        EditorGUILayout.Slider(AttackSpeed, 0f, 10f, new GUIContent("���� �ӵ�"));
        DrawDescriptionLabel("�÷��̾� ĳ������ �⺻ ���� �ӵ�");

        EditorGUILayout.Slider(AttackRange, 0f, 10f, new GUIContent("���� ����"));
        DrawDescriptionLabel("�÷��̾� ĳ������ �⺻ ���� ����");
        EditorGUILayout.EndVertical();


        // ���� ���� ����
        serializedObject.ApplyModifiedProperties();
    }

    void DrawDescriptionLabel(string description)
    {
        EditorGUILayout.BeginHorizontal();
        GUILayout.Space(EditorGUIUtility.labelWidth);
        EditorGUILayout.LabelField(description);
        EditorGUILayout.EndHorizontal();
    }
}
