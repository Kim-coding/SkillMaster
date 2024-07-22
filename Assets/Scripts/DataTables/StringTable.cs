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
    private Dictionary<int, string> stringTable = new Dictionary<int, string>();

    public string GetID(int id)
    {
        if (!stringTable.ContainsKey(id))
            return "Null ID";
        return stringTable[id];
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
                stringTable.Add(record.StringID, record.Text);
            }
        }
    }
}
