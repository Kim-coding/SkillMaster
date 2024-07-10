using CsvHelper;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using UnityEngine;

public class SkillData
{
    public static readonly string ForematSkillPath = "Skill/{0}";

    public int ID { get; private set; }
    public string SkillName { get; private set; }
    public int AttackDamage { get; private set; }
    public float AttackRange {  get; private set; }
    public float Speed { get; private set; }
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
                skillTable.Add(record.ID, record);
            }
        }
    }
}
