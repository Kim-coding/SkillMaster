using CsvHelper;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using UnityEngine;
public class SkillDownData
{
    public int PropertyID { get; set; }
    public float ProjectileSizeX { get; set; }
    public float ProjectileSizeY { get; set; }
    public int ProjectileValue { get; set; }
    public float ProjectileSpeed { get; set; }
    public int Attacknumber { get; set; }
    public float attack_coldown { get; set; }
    public int Projectileangle { get; set; }
    public int HitMonsterValue { get; set; }
    public float AtminRadius { get; set; }
    public float AtmaxRadius { get; set; }
}

public class SkillDownTable : DataTable
{

    private Dictionary<int, SkillDownData> skillDownTable = new Dictionary<int, SkillDownData>();

    public List<SkillDownData> skillDownDatas
    {
        get
        {
            return skillDownTable.Values.ToList();
        }
    }

    public SkillDownData GetID(int id)
    {
        skillDownTable.TryGetValue(id, out var data);
        return data;
    }
    public override void Load(string path)
    {
        string fullPath = string.Format(FormatPath, path);
        TextAsset data = Resources.Load<TextAsset>(fullPath);

        using (var reader = new StringReader(data.text))
        using (var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            var records = csvReader.GetRecords<SkillDownData>();
            foreach (var record in records)
            {
                skillDownTable.Add(record.PropertyID, record);
            }
        }
    }

}
