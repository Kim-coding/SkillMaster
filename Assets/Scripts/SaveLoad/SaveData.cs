

using Newtonsoft.Json;
using System.Collections.Generic;

public class SavePlayData
{
    public int instanceId;

    // 메인 씬 - 스테이지 관련
    public int stageId; // stageCount는 데이터에서 가져옴

    // 플레이어 관련

    public PlayerCurrency saveCurrency;
    //public PlayerStat saveStat;
    public PlayerEnhance savePlayerEnhance;
    public PlayerInfomation savePlayerInfomation;
    public PlayerInventory savePlayerInventory;
    public List<SkillBallController> saveSkillBallControllers = new List<SkillBallController>();


    //퀘스트
    public int questID;
    public int questValue;


    //UI 세팅
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

    // 현재 버전

    public override SaveData VersionUp()
    {
        return null;
    }
}