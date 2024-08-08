using CsvHelper;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using UnityEngine;

public class OfflineRewardData
{
    public int StageID { get; set; }
    public int GoldValue { get; set; }
    public int DiaValue {  get; set; }
    public float DiaProbability { get; set; }
}

public class OfflineRewardTable : DataTable
{
    private Dictionary<int, OfflineRewardData> offlineRewardTable = new Dictionary<int, OfflineRewardData>();

    public List<OfflineRewardData> offlineRewardDatas
    {
        get
        {
            return offlineRewardTable.Values.ToList();
        }
    }
    public OfflineRewardData GetID(int id)
    {
        offlineRewardTable.TryGetValue(id, out var data);
        return data;
    }
    public override void Load(string path)
    {
        string fullPath = string.Format(FormatPath, path);
        TextAsset data = Resources.Load<TextAsset>(fullPath);

        using (var reader = new StringReader(data.text))
        using (var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            var records = csvReader.GetRecords<OfflineRewardData>();
            foreach (var record in records)
            {
                offlineRewardTable.Add(record.StageID, record);
            }
        }
    }
}
