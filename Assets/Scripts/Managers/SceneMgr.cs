
using UnityEngine;

public class SceneMgr : MonoBehaviour
{
    public MainScene mainScene;
    public DungeonScene dungeonScene;
    public DamageTextMgr damageTextMgr;
    private Tutorial tutorial;

    public void Init()
    {
        tutorial = new Tutorial();
        tutorial.Init();

        if (mainScene != null)
        {
            mainScene.Init();
            GameMgr.Instance.rewardMgr.Init();
        }
        if(dungeonScene != null)
        {
            dungeonScene.Init();
        }
    }
}
