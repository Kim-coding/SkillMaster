using CsvHelper;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using UnityEngine;

public class BossData
{
    public int Monsterid { get; set; }
    public string Damage { get; set; }
    public string Health { get; set; }
    public string GoldValue { get; set; }
    public string DiaValue { get; set; }
    public int Reward1 { get; set; }
    public int Reward1Value { get; set; }
    public string Asset { get; set; }
}

public class BossTable : DataTable
{
    private Dictionary<int, BossData> bossTable = new Dictionary<int, BossData>();

    public List<BossData> bossDatas
    {
        get
        {
            return bossTable.Values.ToList();
        }
    }

    public BossData GetID(int id)
    {
        bossTable.TryGetValue(id, out var data);
        return data;
    }
    public override void Load(string path)
    {
        string fullPath = string.Format(FormatPath, path);
        TextAsset data = Resources.Load<TextAsset>(fullPath);

        using (var reader = new StringReader(data.text))
        using (var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            var records = csvReader.GetRecords<BossData>();
            foreach (var record in records)
            {
                bossTable.Add(record.Monsterid, record);
            }
        }
    }
}
