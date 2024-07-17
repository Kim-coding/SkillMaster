using CsvHelper;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class QuestData
{
    public int QuestID {  get; set; }
    public int Division {  get; set; }
    public string Textid { get; set; } // 대충 퀘스트 조건 종류
    public int Targetvalue {  get; set; }  // 퀘스트 완료조건
    public string reward { get; set; } // 보상
    public int rewardvalue { get;  set; } // 보상 수치 
    public string rewardicon { get; set; } // ?
    public int nextQuest { get; set; } // ?

}

public class QuestTable : DataTable
{
    private Dictionary<int, QuestData> questTable = new Dictionary<int, QuestData>();

    public List<QuestData> questDatas
    {
        get
        {
            return questTable.Values.ToList();
        }
    }

    public QuestData GetID(int id)
    {
        questTable.TryGetValue(id,out  var data);
        return data;
    }
    public override void Load(string path)
    {
        string fullPath = string.Format(FormatPath, path);
        TextAsset data = Resources.Load<TextAsset>(fullPath);

        using (var reader = new StringReader(data.text))
        using (var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            var records = csvReader.GetRecords<QuestData>();
            foreach (var record in records)
            {
                questTable.Add(record.QuestID, record);
            }
        }
    }
}
