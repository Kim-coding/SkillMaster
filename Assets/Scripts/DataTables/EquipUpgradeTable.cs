using CsvHelper;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using UnityEngine;

public class EquipmentUpgradeData
{
    public int Equipmentupgradeid { get; set; }
    public int upgrade_lv { get; set; }
    public int reinforce_increase { get; set; }
    public int Success_persent { get; set; }
    public int Success_persentdown { get; set; }
    public int gold_usevalue { get; set; }
    public float option_raise { get; set; }

}


public class EquipUpgradeTable : DataTable
{
    private Dictionary<int, EquipmentUpgradeData> equipmentUpgradeTable = new Dictionary<int, EquipmentUpgradeData>();

    public List<EquipmentUpgradeData> equipmentUpgradeDatas
    {
        get
        {
            return equipmentUpgradeTable.Values.ToList();
        }
    }

    public EquipmentUpgradeData GetID(int id)
    {
        equipmentUpgradeTable.TryGetValue(id, out var data);
        return data;
    }
    public override void Load(string path)
    {
        string fullPath = string.Format(FormatPath, path);
        TextAsset data = Resources.Load<TextAsset>(fullPath);

        using (var reader = new StringReader(data.text))
        using (var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            var records = csvReader.GetRecords<EquipmentUpgradeData>();
            foreach (var record in records)
            {
                equipmentUpgradeTable.Add(record.upgrade_lv, record);
            }
        }
    }
}
