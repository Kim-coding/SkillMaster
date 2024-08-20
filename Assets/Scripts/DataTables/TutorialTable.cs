using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TutorialData
{
    
}

public class TutorialTable : DataTable
{
    private Dictionary<int, TutorialData> tutorialTable = new Dictionary<int, TutorialData>();

    public List<TutorialData> tutorialDatas
    {
        get
        {
            return tutorialTable.Values.ToList();
        }
    }

    public override void Load(string path)
    {
        throw new System.NotImplementedException();
    }

}
