using CsvHelper;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using UnityEngine;

public class QuestData
{
    public int QuestID {  get; set; }
    public int Division {  get; set; } // 타입
    public int Targetvalue {  get; set; }  // 퀘스트 완료조건
    public string reward { get; set; } // 보상
    public int rewardvalue { get;  set; } // 보상 수치 
    public string rewardicon { get; set; } // 아이콘
    public int Next_Quest { get; set; } // 다음 퀘스트 아이디
    public int StringId { get; set; } // 설명
    public int Level { get; set; } //레벨

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
