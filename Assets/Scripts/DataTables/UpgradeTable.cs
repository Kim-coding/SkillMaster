using CsvHelper;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using UnityEngine;
public class UpgradeData
{
    public int UpgradeID { get; set; }
    public int UpgradeDivision { get; set; }
    public float Increase { get; set; }
    public int DivValue { get; set; } 
    public int Gold { get; set; }
    public int GoldRange { get; set; }
    public int GoldincreaseValue { get; set; }
    public int MaxLv { get; set; }
    public string UpgradeIcon { get; set; }
    public int StringID { get; set; }
    public string GetStringID
    {
        get
        {
            return DataTableMgr.Get<StringTable>(DataTableIds.String).GetID(StringID);
        }
    }


}

public class UpgradeTable : DataTable
{
    private Dictionary<int, UpgradeData> upgradeTable = new Dictionary<int, UpgradeData>();

    public List<UpgradeData> upgradeDatas
    {
        get
        {
            return upgradeTable.Values.ToList();
        }
    }

    public UpgradeData GetID(int id)
    {
        upgradeTable.TryGetValue(id, out var data);
        return data;
    }
    public override void Load(string path)
    {
        string fullPath = string.Format(FormatPath, path);
        TextAsset data = Resources.Load<TextAsset>(fullPath);

        using (var reader = new StringReader(data.text))
        using (var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            var records = csvReader.GetRecords<UpgradeData>();
            foreach (var record in records)
            {
                upgradeTable.Add(record.UpgradeID, record);
            }
        }
    }


}
