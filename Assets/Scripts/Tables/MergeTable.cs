using CsvHelper;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using UnityEngine;

public class MergeData
{
    public static readonly string ForematNextSkillPath = "Skill/{0}";

    public int ID { get; private set; }

}

public class MergeTable : DataTable
{
    private Dictionary<int, MergeData> dataTable = new Dictionary<int, MergeData>();

    public List<MergeData> skillDatas
    {
        get
        {
            return dataTable.Values.ToList();
        }
    }

    public MergeData GetID(int id)
    {
        dataTable.TryGetValue(id, out var data);
        return data;
    }

    public override void Load(string path)
    {
        string fullPath = string.Format(FormatPath, path);
        TextAsset data = Resources.Load<TextAsset>(fullPath);

        using (var reader = new StringReader(data.text))
        using (var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            var records = csvReader.GetRecords<MergeData>();
            foreach (var record in records)
            {
                dataTable.Add(record.ID, record);
            }
        }
    }
}
