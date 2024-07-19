using CsvHelper;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using UnityEngine;

public class MonsterData
{
    public static readonly string FormatMonsterPath = "Monster/{0}";

    public int Monsterid { get; set; }
    public string Damage { get; set; }
    public string Health {  get; set; }
    public string GoldValue {  get; set; }
    public string Reward { get; set; }
    public int RewardValue { get; set; }
    public string Asset1 { get; set; }
    public string Asset2 { get; set; }
    //µî

}

public class MonsterTable : DataTable
{
    private Dictionary<int, MonsterData> monsterTable = new Dictionary<int, MonsterData>();

    public List<MonsterData> monsterDatas
    {
        get
        {
            return monsterTable.Values.ToList();
        }
    }

    public MonsterData GetID(int id)
    {
        monsterTable.TryGetValue(id, out var data);
        return data;
    }

    public override void Load(string path)
    {
        string fullPath = string.Format(FormatPath, path);
        TextAsset data = Resources.Load<TextAsset>(fullPath);

        using (var reader = new StringReader(data.text))
        using (var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            var records = csvReader.GetRecords<MonsterData>();
            foreach (var record in records)
            {
                monsterTable.Add(record.Monsterid, record);
            }
        }
    }
}
