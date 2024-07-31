using CsvHelper;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using UnityEngine;


public class OptionNumberData
{
    //public static readonly string FormatMonsterPath = "Monster/{0}";

    public int optionvalueID { get; set; }
    public int option_state { get; set; }
    public int option_min { get; set; }
    public int option_max { get; set; }

}
public class OptionNumberTable : DataTable
{

    private Dictionary<int, OptionNumberData> optionNumberTable = new Dictionary<int, OptionNumberData>();

    public List<OptionNumberData> optionNumberDatas
    {
        get
        {
            return optionNumberTable.Values.ToList();
        }
    }

    public OptionNumberData GetID(int id)
    {
        optionNumberTable.TryGetValue(id, out var data);
        return data;
    }
    public override void Load(string path)
    {
        string fullPath = string.Format(FormatPath, path);
        TextAsset data = Resources.Load<TextAsset>(fullPath);

        using (var reader = new StringReader(data.text))
        using (var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            var records = csvReader.GetRecords<OptionNumberData>();
            foreach (var record in records)
            {
                optionNumberTable.Add(record.optionvalueID, record);
            }
        }
    }

}
