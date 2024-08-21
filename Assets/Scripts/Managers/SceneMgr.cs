
using UnityEngine;

public class SceneMgr : MonoBehaviour
{
    public MainScene mainScene;
    public DungeonScene dungeonScene;
    public DamageTextMgr damageTextMgr;
    public Tutorial tutorial;

    public void Init()
    {
        if (mainScene != null)
        {
            mainScene.Init();
            GameMgr.Instance.rewardMgr.Init();
        }
        if(dungeonScene != null)
        {
            dungeonScene.Init();
        }

        tutorial = new Tutorial();
        tutorial.Init();
    }

    public void Start()
    {
        if (tutorial.tutorialID <= 120238)
        {
            tutorial.OnTutorial();
        }
    }
}
