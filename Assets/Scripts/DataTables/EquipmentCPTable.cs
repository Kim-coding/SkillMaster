using CsvHelper;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using UnityEngine;


public class CPData
{
    public int equipment_option { get; set; }
    public float equipment_option_value { get; set; }
    public int Stat_ID { get; set; }

    public float GetMaxStat
    {
        get
        {
            return DataTableMgr.Get<OptionNumberTable>(DataTableIds.optionNumber).GetID(Stat_ID).option_max;
        }
    }

}

public class EquipmentCPTable : DataTable
{
    private Dictionary<int, CPData> CPTable = new Dictionary<int, CPData>();

    public List<CPData> CPDatas
    {
        get
        {
            return CPTable.Values.ToList();
        }
    }

    public CPData GetID(int id)
    {
        CPTable.TryGetValue(id, out var data);
        return data;
    }
    public override void Load(string path)
    {
        string fullPath = string.Format(FormatPath, path);
        TextAsset data = Resources.Load<TextAsset>(fullPath);

        using (var reader = new StringReader(data.text))
        using (var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            var records = csvReader.GetRecords<CPData>();
            foreach (var record in records)
            {
                CPTable.Add(record.equipment_option, record);
            }
        }
    }
}
