using UnityEngine;

[CreateAssetMenu(fileName = "PlayerBaseStat", menuName = "PlayerBaseStat")]
public class PlayerBaseStat : ScriptableObject
{
    public float baseSpeed;
    public float baseAttackSpeed;
    public float baseAttackRange;



    public delegate void OnSettingChange();
    public event OnSettingChange onSettingChange;

    public void NotifySettingsChange()
    {
        if (onSettingChange != null)
            onSettingChange();
    }

}
