using CsvHelper;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class StageData
{
    public int ID {  get; private set; }
    public int Stage {  get; private set; }
    public int Condition { get; private set; } //보스 등장 조건 : 처치 수
    public int BossID {  get; private set; }
    public int Monster1 { get; private set; }
    public int Monster2 { get; private set; }
    public float TimeLimit { get; private set; }
    public int MonsterLavel { get; private set; }

}

public class StageTable : DataTable
{
    private Dictionary<int, StageData> stageTable = new Dictionary<int, StageData>();

    public List<StageData> stageDatas
    {
        get
        {
            return stageTable.Values.ToList();
        }
    }

    public StageData GetID(int id)
    {
        stageTable.TryGetValue(id,out  var data);
        return data;
    }
    public override void Load(string path)
    {
        string fullPath = string.Format(FormatPath, path);
        TextAsset data = Resources.Load<TextAsset>(fullPath);

        using (var reader = new StringReader(data.text))
        using (var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            var records = csvReader.GetRecords<StageData>();
            foreach (var record in records)
            {
                stageTable.Add(record.ID, record);
            }
        }
    }
}
