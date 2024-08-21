using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBook : MonoBehaviour
{
    public SkillBook skillBookPrefab;
    public GameObject skillBookPanel;

    public Dictionary<int, SkillBook> skillBookDic = new Dictionary<int, SkillBook>();


    public void Init()
    {
        var data = GameMgr.Instance.playerMgr.playerInfo.skillBookDatas;

        foreach (var item in data)
        {
            var newBook = Instantiate(skillBookPrefab, skillBookPanel.transform);
            newBook.Init(item.Value);
            skillBookDic.Add(item.Key, newBook);
            GameMgr.Instance.playerMgr.playerInfo.AcquiredUpdate(item.Key, item.Value.state);
        }
    }
}
