using CsvHelper;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using UnityEngine;


public class GachaData
{
    //public static readonly string FormatMonsterPath = "Monster/{0}";

    public int gachaLv { get; set; }
    public int gachaReward { get; set; }
    public int gachaRewardValue { get; set; }
    public float C_Probability { get; set; }
    public float B_Probability { get; set; }
    public float A_Probability { get; set; }
    public float S_Probability { get; set; }
    public float SS_Probability { get; set; }
    public float SSS_Probability { get; set; }

}

    public class GachaTable : DataTable
{

    private Dictionary<int, GachaData> gachaTable = new Dictionary<int, GachaData>();

    public List<GachaData> gachaDatas
    {
        get
        {
            return gachaTable.Values.ToList();
        }
    }

    public GachaData GetID(int id)
    {
        gachaTable.TryGetValue(id, out var data);
        return data;
    }
    public override void Load(string path)
    {
        string fullPath = string.Format(FormatPath, path);
        TextAsset data = Resources.Load<TextAsset>(fullPath);

        using (var reader = new StringReader(data.text))
        using (var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            var records = csvReader.GetRecords<GachaData>();
            foreach (var record in records)
            {
                gachaTable.Add(record.gachaLv, record);
            }
        }
    }

}
