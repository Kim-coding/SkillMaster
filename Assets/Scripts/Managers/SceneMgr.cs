
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
            mainScene.Init();

        }
        if(dungeonScene != null)
        {
            dungeonScene.Init();
        }
    }
}
