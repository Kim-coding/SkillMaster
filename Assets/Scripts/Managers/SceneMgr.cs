
using UnityEngine;

public class SceneMgr : MonoBehaviour
{
    public MainScene mainScene;
    public DungeonScene dungeonScene;
    public DamageTextMgr damageTextMgr;

    public void Init()
    {
        if(mainScene != null)
        {
            GameMgr.Instance.rewardMgr.Init();
            GameMgr.Instance.webTimeMgr.init();
            mainScene.Init();
        }
        if(dungeonScene != null)
        {
            dungeonScene.Init();
        }
    }
}
