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
        // SerializedProperty 초기화
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
        // 설명 텍스트 표시
        EditorGUILayout.LabelField("플레이어의 기본 이동속도 / 공격속도 / 공격 범위 설정", style);


        // 설명 텍스트
       // EditorGUILayout.HelpBox("플레이어의 기본 이동속도 / 공격속도 / 공격 범위 설정", MessageType.Info);

        serializedObject.Update();


        // 각 필드에 대한 설명을 개별적으로 렌더링
        EditorGUILayout.BeginVertical();
        EditorGUILayout.Space(10);

        EditorGUILayout.Slider(Speed, 0f, 10f, new GUIContent("이동 속도"));
        DrawDescriptionLabel("플레이어 캐릭터의 기본 이동 속도");

        EditorGUILayout.Slider(AttackSpeed, 0f, 10f, new GUIContent("공격 속도"));
        DrawDescriptionLabel("플레이어 캐릭터의 기본 공격 속도");

        EditorGUILayout.Slider(AttackRange, 0f, 10f, new GUIContent("공격 범위"));
        DrawDescriptionLabel("플레이어 캐릭터의 기본 공격 범위");
        EditorGUILayout.EndVertical();


        // 변경 사항 적용
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
