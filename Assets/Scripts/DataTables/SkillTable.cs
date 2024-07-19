using CsvHelper;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using UnityEngine;

public class SkillData
{
   // public static readonly string ForematSkillPath = "Skill/{0}";

    public int Skill_ID { get; set; }
    public int Type { get; set; }
    public float AttackSpeed { get; set; }
    public float DamageColdown {  get; set; }
    public float SkillArange { get; set; }
    public float AtkArangeX { get; set; }
    public float AtkArangeY { get; set; }
    public int SkillLv { get; set; }
    public int SkillPropertyID { get; set; }
}

public class SkillTable : DataTable
{
    private Dictionary<int, SkillData> skillTable = new Dictionary<int, SkillData>();

    public List<SkillData> skillDatas
    {
        get
        {
            return skillTable.Values.ToList();
        }
    }

    public SkillData GetID(int id)
    {
        skillTable.TryGetValue(id, out var data);
        return data;
    }

    public override void Load(string path)
    {
        string fullPath = string.Format(FormatPath, path);
        TextAsset data = Resources.Load<TextAsset>(fullPath);

        using (var reader = new StringReader(data.text))
        using (var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            var records = csvReader.GetRecords<SkillData>();
            foreach (var record in records)
            {
                skillTable.Add(record.Skill_ID, record);
            }
        }
    }
}
