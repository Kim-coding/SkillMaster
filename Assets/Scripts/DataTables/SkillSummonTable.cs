using CsvHelper;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using UnityEngine;

public class SkillSummonData
{
    public int summonID { get; set; }
    public int CbnUpgradeLv { get; set; }
    public int skill1Lv { get; set; }
    public int skill1per { get; set; }
    public int skill2Lv { get; set; }
    public int skill2per { get; set; }
    public int skill3Lv { get; set; }
    public int skill3per { get; set; }
    public int skill4Lv { get; set; }
    public int skill4per { get; set; }

}

public class SkillSummonTable : DataTable
{
    private Dictionary<int, SkillSummonData> skillSummonTable = new Dictionary<int, SkillSummonData>();

    public List<SkillSummonData> skillSummonDatas
    {
        get
        {
            return skillSummonTable.Values.ToList();
        }
    }

    public SkillSummonData GetID(int id)
    {
        skillSummonTable.TryGetValue(id, out var data);
        return data;
    }
    public override void Load(string path)
    {
        string fullPath = string.Format(FormatPath, path);
        TextAsset data = Resources.Load<TextAsset>(fullPath);

        using (var reader = new StringReader(data.text))
        using (var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            var records = csvReader.GetRecords<SkillSummonData>();
            foreach (var record in records)
            {
                skillSummonTable.Add(record.CbnUpgradeLv, record);
            }
        }
    }

}
