

using Newtonsoft.Json;
using System.Collections.Generic;

public class SavePlayData
{
    public int instanceId;

    // ���� �� - �������� ����
    public int stageId; // stageCount�� �����Ϳ��� ������

    // �÷��̾� ����

    public PlayerCurrency saveCurrency;
    //public PlayerStat saveStat;
    public PlayerEnhance savePlayerEnhance;
    public PlayerInfomation savePlayerInfomation;
    public PlayerInventory savePlayerInventory;
    public List<SkillBallController> saveSkillBallControllers = new List<SkillBallController>();


    //����Ʈ
    public int questID;
    public int questValue;


    //UI ����
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