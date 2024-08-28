using CsvHelper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using UnityEngine;

public class CombinationUpgradeData
{
    public int CbnID { get; set; }
    public int UpgradeDivision { get; set; }
    public float Increase { get; set; }
    public int Pay { get; set; }
    public int PayDefault { get; set; }
    public int PayIncrease { get; set; }
    public int PayRange { get; set; }
    public int MaxLv { get; set; }
    public string UpgradeIcon { get; set; }
    public int CbnName { get; set; }
    public int CbnInfo { get; set; }

    public Sprite GetIcon
    {
        get
        {
            return Resources.Load<Sprite>(UpgradeIcon);
        }
    }

    public string GetCbnName
    {
        get
        {
            return DataTableMgr.Get<StringTable>(DataTableIds.String).GetID(CbnName);
        }
    }

    public string GetCbnInfo
    {
        get
        {
            return DataTableMgr.Get<StringTable>(DataTableIds.String).GetID(CbnInfo);
        }
    }


}

public class CombinationUpgradeTable : DataTable
{
    private Dictionary<int, CombinationUpgradeData> combinationUpgradeTable = new Dictionary<int, CombinationUpgradeData>();

    public List<CombinationUpgradeData> combinationUpgradeDatas
    {
        get
        {
            return combinationUpgradeTable.Values.ToList();
        }
    }

    public CombinationUpgradeData GetID(int id)
    {
        combinationUpgradeTable.TryGetValue(id, out var data);
        return data;
    }
    public override void Load(string path)
    {
        string fullPath = string.Format(FormatPath, path);
        TextAsset data = Resources.Load<TextAsset>(fullPath);

        using (var reader = new StringReader(data.text))
        using (var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            var records = csvReader.GetRecords<CombinationUpgradeData>();
            foreach (var record in records)
            {
                combinationUpgradeTable.Add(record.CbnID, record);
            }
        }
    }
}
