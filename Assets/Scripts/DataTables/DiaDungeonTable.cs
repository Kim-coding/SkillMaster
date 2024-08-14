using CsvHelper;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using UnityEngine;

public class DiaDungeonData
{
    public float timelimt { get; set; }
    public int boss1_id { get; set; }
    public int boss2_id { get; set; }
    public int boss3_id { get; set; }
    public string reward_value { get; set; }
    public int stage_lv { get; set; }
}

public class DiaDungeonTable : DataTable
{
    private Dictionary<int, DiaDungeonData> diaDungeonTable = new Dictionary<int, DiaDungeonData>();

    public List<DiaDungeonData> diaDungeonDatas
    {
        get
        {
            return diaDungeonTable.Values.ToList();
        }
    }

    public DiaDungeonData GetID(int id)
    {
        diaDungeonTable.TryGetValue(id, out var data);
        return data;
    }

    public override void Load(string path)
    {
        string fullPath = string.Format(FormatPath, path);
        TextAsset data = Resources.Load<TextAsset>(fullPath);

        using (var reader = new StringReader(data.text))
        using (var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            var records = csvReader.GetRecords<DiaDungeonData>();
            foreach (var record in records)
            {
                diaDungeonTable.Add(record.stage_lv, record);
            }
        }
    }
}
