using CsvHelper;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using UnityEngine;

public class GoldDungeonData
{
    public int gold_dungeon_lv { get; set; }
    public string request_damage {  get; set; }
    public string reward_value { get; set; }
}

public class GoldDungeonTable : DataTable
{
    private Dictionary<int, GoldDungeonData> goldDungeonTable = new Dictionary<int, GoldDungeonData>();

    public List<GoldDungeonData> goldDungeonDatas
    {
        get
        {
            return goldDungeonTable.Values.ToList();
        }
    }

    public GoldDungeonData GetID(int id)
    {
        goldDungeonTable.TryGetValue(id, out var data);
        return data;
    }

    public override void Load(string path)
    {
        string fullPath = string.Format(FormatPath, path);
        TextAsset data = Resources.Load<TextAsset>(fullPath);

        using (var reader = new StringReader(data.text))
        using (var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            var records = csvReader.GetRecords<GoldDungeonData>();
            foreach (var record in records)
            {
                goldDungeonTable.Add(record.gold_dungeon_lv, record);
            }
        }
    }
}
