using CsvHelper;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class StringData
{
    public int StringID { get; set; }
    public int ApplyTable { get; set; }
    public string Text { get; set; }

}

public class StringTable : DataTable
{
    private Dictionary<int, StringData> stringTable = new Dictionary<int, StringData>();

    public List<StringData> stringDatas
    {
        get
        {
            return stringTable.Values.ToList();
        }
    }

    public StringData GetID(int id)
    {
        stringTable.TryGetValue(id, out var data);
        return data;
    }
    public override void Load(string path)
    {
        string fullPath = string.Format(FormatPath, path);
        TextAsset data = Resources.Load<TextAsset>(fullPath);

        using (var reader = new StringReader(data.text))
        using (var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            var records = csvReader.GetRecords<StringData>();
            foreach (var record in records)
            {
                stringTable.Add(record.StringID, record);
            }
        }
    }
}
