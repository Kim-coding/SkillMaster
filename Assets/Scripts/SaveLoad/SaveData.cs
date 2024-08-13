

using Newtonsoft.Json;
using System.Collections.Generic;

public class SavePlayData
{
    public int instanceId;

    // ���� �� - �������� ����
    public int stageId; // stageCount�� �����Ϳ��� ������

    // �÷��̾� ����

    public PlayerCurrency saveCurrency;
    public PlayerStat saveStat;
    public PlayerEnhance savePlayerEnhance;
    public PlayerInfomation savePlayerInfomation;
    [JsonIgnore]
    public PlayerInventory savePlayerInventory;
    [JsonIgnore]
    public List<SkillBallController> saveSkillBallControllers;


    //�����Ұ͵�
}

public abstract class SaveData
{
    public int Version { get; protected set; }

    public abstract SaveData VersionUp();

}

public class SaveDataV1 : SaveData
{

    public SavePlayData savePlay;

    public SaveDataV1()
    {
        Version = 1;
    }

    // ���� ����

    public override SaveData VersionUp()
    {
        return null;
    }
}