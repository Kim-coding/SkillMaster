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
        // SerializedProperty 초기화
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
        // 설명 텍스트 표시
        EditorGUILayout.LabelField("플레이어의 기본 이동속도 / 공격속도 / 공격 범위 설정", style);


        // 설명 텍스트
       // EditorGUILayout.HelpBox("플레이어의 기본 이동속도 / 공격속도 / 공격 범위 설정", MessageType.Info);

        serializedObject.Update();


        // 각 필드에 대한 설명을 개별적으로 렌더링
        EditorGUILayout.BeginVertical();
        EditorGUILayout.Space(10);

        EditorGUILayout.Slider(baseSpeed, 0f, 10f, new GUIContent("이동 속도"));
        DrawDescriptionLabel("플레이어 캐릭터의 기본 이동 속도");

        EditorGUILayout.Slider(baseAttackSpeed, 0f, 10f, new GUIContent("공격 속도"));
        DrawDescriptionLabel("플레이어 캐릭터의 기본 공격 속도");

        EditorGUILayout.Slider(baseAttackRange, 0f, 10f, new GUIContent("공격 범위"));
        DrawDescriptionLabel("플레이어 캐릭터의 기본 공격 범위");
        EditorGUILayout.EndVertical();

        EditorGUILayout.Space(20);
        EditorGUILayout.LabelField("플레이어의 기본 Hp / Hp회복 / 공격력 / 방어력 / 치명타확률 / 치명타배율 설정", style);
        EditorGUILayout.Space(10);


        EditorGUILayout.PropertyField(basePlayerAttackPower, new GUIContent("기본 공격력"));
        DrawDescriptionLabel("플레이어 캐릭터의 기본 공격력");

        EditorGUILayout.PropertyField(basePlayerDefence, new GUIContent("기본 방어력"));
        DrawDescriptionLabel("플레이어 캐릭터의 기본 방어력");

        EditorGUILayout.PropertyField(basePlayerMaxHealth, new GUIContent("기본 최대 체력"));
        DrawDescriptionLabel("플레이어 캐릭터의 기본 최대 체력");

        EditorGUILayout.PropertyField(basePlayerHealthRecovery, new GUIContent("기본 체력 회복력"));
        DrawDescriptionLabel("플레이어 캐릭터의 기본 체력 회복력");

        EditorGUILayout.PropertyField(basePlayerCriticalPercent, new GUIContent("기본 치명타 확률"));
        DrawDescriptionLabel("플레이어 캐릭터의 기본 치명타 확률");

        EditorGUILayout.PropertyField(basePlayerCriticalMultiple, new GUIContent("기본 치명타 배율"));
        DrawDescriptionLabel("플레이어 캐릭터의 기본 치명타 배율");

        // 변경 사항 적용
        serializedObject.ApplyModifiedProperties();

        PlayerBaseStat stat = (PlayerBaseStat)target;
        EditorGUILayout.Space(20);

        if (GUILayout.Button("적용"))
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
