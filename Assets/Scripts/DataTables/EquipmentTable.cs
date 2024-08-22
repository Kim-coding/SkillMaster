using CsvHelper;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Globalization;
using System.IO;
using System.Linq;
using UnityEngine;


public class EquipData
{
    public int equipmentID { get; set; }
    public int equipmenttype { get; set; }
    public string equipment_rating { get; set; }
    public string equipment_esset { get; set; }
    public Sprite[] GetTexture
    {
        get
        {
            return Resources.LoadAll<Sprite>("Equipment/" + equipment_esset);
        }
    }

    public Sprite Geticon
    {
        get
        {
            var Image = GetTexture[0].texture;
            return Sprite.Create(Image, new Rect(0, 0, Image.width, Image.height), new Vector2(0.5f, 0.5f));
        }
    }
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


    public int GetSetId
    {
        get
        {
            var data = DataTableMgr.Get<EquipBookTable>(DataTableIds.equipBook).equipBookDatas;
            foreach(var setOption in data)
            {
                if(setOption.equipment1_id == equipmentID || setOption.equipment2_id == equipmentID ||
                    setOption.equipment3_id == equipmentID || setOption.equipment4_id == equipmentID
                    || setOption.equipment5_id == equipmentID)
                {
                    return setOption.equipbook_id;
                }
            }

            return -1;
        }
    }

    public int explain_id { get; set; }

}


public class EquipmentTable : DataTable
{

    private Dictionary<int, EquipData> equipTable = new Dictionary<int, EquipData>();

    public List<EquipData> equipDatas
    {
        get
        {
            return equipTable.Values.ToList();
        }
    }

    public EquipData GetID(int id)
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
                equipTable.Add(record.equipmentID, record);
            }
        }
    }

}
