
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
            tutorial = new Tutorial();
            tutorial.Init();
        }
        if(dungeonScene != null)
        {
            dungeonScene.Init();
        }
    }

    public void Start()
    {
        if (mainScene != null && tutorial.tutorialID <= 120238)
        {
            SaveLoadSystem.Save();
            tutorial.OnTutorial();
        }
    }
}
