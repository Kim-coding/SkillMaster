using CsvHelper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using UnityEngine;


public class EquipData
{
    //public static readonly string FormatMonsterPath = "Monster/{0}";

    public int equipmentID { get; set; }
    public int equipmenttype { get; set; }
    public string equipment_rating { get; set; }
    public string equipment_esset { get; set; }  //¿Ã∞‘ key
    public int reinforcement_value { get; set; }
    public int item_name_id { get; set; }
    public string GetItemName
    {
        get
        {
            return DataTableMgr.Get<StringTable>(DataTableIds.String).GetID(item_name_id);
        }
    }
    public int option_value { get; set; }
    public int option_id { get; set; }

    public OptionData GetOption
    {
        get
        {
            return DataTableMgr.Get<OptionTable>(DataTableIds.option).GetID(option_id);
        }
    }

    public string item_icon { get; set; }

}


public class EquipmentTable : DataTable
{

    private Dictionary<string, EquipData> equipTable = new Dictionary<string, EquipData>();

    public List<EquipData> equipDatas
    {
        get
        {
            return equipTable.Values.ToList();
        }
    }

    public EquipData GetID(string id)
    {
        equipTable.TryGetValue(id, out var data);
        return data;
    }
    public override void Load(string path)
    {
        string fullPath = string.Format(FormatPath, path);
        TextAsset data = Resources.Load<TextAsset>(fullPath);

        using (var reader = new StringReader(data.text))
        using (var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            var records = csvReader.GetRecords<EquipData>();
            foreach (var record in records)
            {
                equipTable.Add(record.equipment_esset, record);
            }
        }
    }

}
