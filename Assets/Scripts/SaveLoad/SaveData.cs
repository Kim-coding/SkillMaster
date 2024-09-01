

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

    //보상 ID
    public int rewardID;

    //UI 세팅
    public float bgm;
    public float sfx;

    //튜토리얼
    public int tutorialID;
    public int tutorialIndex;
    public bool isDungeonOpen;

    //스토리
    public bool isStory;

    //유저 이름
    public string userName;

    //던전 해제 팝업 확인 여부
    public bool hasShownDungeonMessage;
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