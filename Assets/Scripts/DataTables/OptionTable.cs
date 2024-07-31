using CsvHelper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using UnityEngine;


public class OptionData
{
    //public static readonly string FormatMonsterPath = "Monster/{0}";

    public int optionID { get; set; }
    public int option1_valueid { get; set; }
    public float option1_persent { get; set; }
    public OptionNumberData GetOption1_value
    {
        get
        {
            return DataTableMgr.Get<OptionNumberTable>(DataTableIds.optionNumber).GetID(option1_valueid);
        }
    }

    public int option2_valueid { get; set; }
    public float option2_persent { get; set; }
    public OptionNumberData GetOption2_value
    {
        get
        {
            return DataTableMgr.Get<OptionNumberTable>(DataTableIds.optionNumber).GetID(option2_valueid);
        }
    }

    public int option3_valueid { get; set; }
    public float option3_persent { get; set; }
    public OptionNumberData GetOption3_value
    {
        get
        {
            return DataTableMgr.Get<OptionNumberTable>(DataTableIds.optionNumber).GetID(option3_valueid);
        }
    }

    public int option4_valueid { get; set; }
    public float option4_persent { get; set; }
    public OptionNumberData GetOption4_value
    {
        get
        {
            return DataTableMgr.Get<OptionNumberTable>(DataTableIds.optionNumber).GetID(option4_valueid);
        }
    }

}
public class OptionTable : DataTable
{

    private Dictionary<int, OptionData> optionTable = new Dictionary<int, OptionData>();

    public List<OptionData> optionDatas
    {
        get
        {
            return optionTable.Values.ToList();
        }
    }

    public OptionData GetID(int id)
    {
        optionTable.TryGetValue(id, out var data);
        return data;
    }
    public override void Load(string path)
    {
        string fullPath = string.Format(FormatPath, path);
        TextAsset data = Resources.Load<TextAsset>(fullPath);

        using (var reader = new StringReader(data.text))
        using (var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            var records = csvReader.GetRecords<OptionData>();
            foreach (var record in records)
            {
                optionTable.Add(record.optionID, record);
            }
        }
    }

}
