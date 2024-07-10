using CsvHelper;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class BossData
{
    public int ID { get; private set; }
    public string BossName { get; private set; }
    public int Health { get; private set; }
    public int Damage { get; private set; }
    public int Gold { get; private set; }
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
                bossTable.Add(record.ID, record);
            }
        }
    }
}
