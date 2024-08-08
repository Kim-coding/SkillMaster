using CsvHelper;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using UnityEngine;

public class StuffData
{
    public int stuffid { get; set; }
    public int stuff_name { get; set; }
    public string GetName
    {
        get
        {
            return DataTableMgr.Get<StringTable>(DataTableIds.String).GetID(stuff_name);
        }
    }
    public int stuff_type { get; set; }
    public int Itembundle { get; set; }
    public int Itemmaxvalue { get; set; }
    public string Itemicon { get; set; }
    public Sprite Geticon
    {
        get
        {
            Sprite[] texture = Resources.LoadAll<Sprite>(Itemicon);
            var Image = texture[0].texture;
            return Sprite.Create(Image, new Rect(0, 0, Image.width, Image.height), new Vector2(0.5f, 0.5f));
        }
    }
    public int stuff_explainid { get; set; }

    public string GetExplain
    {
        get
        {
            return DataTableMgr.Get<StringTable>(DataTableIds.String).GetID(stuff_explainid);
        }
    }


}

public class StuffTable : DataTable
{

    private Dictionary<int, StuffData> stuffTable = new Dictionary<int, StuffData>();

    public List<StuffData> stuffDatas
    {
        get
        {
            return stuffTable.Values.ToList();
        }
    }

    public StuffData GetID(int id)
    {
        stuffTable.TryGetValue(id, out var data);
        return data;
    }
    public override void Load(string path)
    {
        string fullPath = string.Format(FormatPath, path);
        TextAsset data = Resources.Load<TextAsset>(fullPath);

        using (var reader = new StringReader(data.text))
        using (var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            var records = csvReader.GetRecords<StuffData>();
            foreach (var record in records)
            {
                stuffTable.Add(record.stuffid, record);
            }
        }
    }


}
