using CsvHelper;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using UnityEngine;

public class SkillBookData
{
    public int skillbook_ID { get; set; }
    public int skill_equipment_id { get; set; }
    public int skill_level { get; set; }
    public int dia_rewardvalue { get; set; }
    public int text_id { get; set; }
    public string image { get; set; }

}
public class EquipBookData
{
    public int equipbook_id { get; set; }
    public int equipment_part { get; set; }
    public int equipment_order { get; set; }
    public int equipment1_id { get; set; }
    public int equipment2_id { get; set; }
    public int equipment3_id { get; set; }
    public int equipment4_id { get; set; }
    public int equipment5_id { get; set; }
    public int equipment_reward { get; set; }
    public int synergy_type { get; set; }
    public float synergy_value { get; set; }
    public string nametext_id { get; set; }

}


public class SkillBookTable : DataTable
{
    private Dictionary<int, SkillBookData> skillBookTable = new Dictionary<int, SkillBookData>();

    public List<SkillBookData> skillBookDatas
    {
        get
        {
            return skillBookTable.Values.ToList();
        }
    }

    public SkillBookData GetID(int id)
    {
        skillBookTable.TryGetValue(id, out var data);
        return data;
    }
    public override void Load(string path)
    {
        string fullPath = string.Format(FormatPath, path);
        TextAsset data = Resources.Load<TextAsset>(fullPath);

        using (var reader = new StringReader(data.text))
        using (var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            var records = csvReader.GetRecords<SkillBookData>();
            foreach (var record in records)
            {
                skillBookTable.Add(record.skill_equipment_id, record);
            }
        }
    }
}

public class EquipBookTable : DataTable
{
    private Dictionary<int, EquipBookData> equipBookTable = new Dictionary<int, EquipBookData>();

    public List<EquipBookData> equipBookDatas
    {
        get
        {
            return equipBookTable.Values.ToList();
        }
    }

    public EquipBookData GetID(int id)
    {
        equipBookTable.TryGetValue(id, out var data);
        return data;
    }
    public override void Load(string path)
    {
        string fullPath = string.Format(FormatPath, path);
        TextAsset data = Resources.Load<TextAsset>(fullPath);

        using (var reader = new StringReader(data.text))
        using (var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            var records = csvReader.GetRecords<EquipBookData>();
            foreach (var record in records)
            {
                equipBookTable.Add(record.equipbook_id, record);
            }
        }
    }
}
