using CsvHelper;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class QuestData
{
    public int ID {  get; private set; }
    public int Condition { get; private set; } // ���� ����Ʈ ���� ����
    public int BossID {  get; private set; }  // ����Ʈ �Ϸ�����
    public float TimeLimit { get; private set; } // ����
    public int MonsterLavel { get; private set; } // ���� ��ġ 

}

public class QuestTable : DataTable
{
    private Dictionary<int, QuestData> questTable = new Dictionary<int, QuestData>();

    public List<QuestData> questDatas
    {
        get
        {
            return questTable.Values.ToList();
        }
    }

    public QuestData GetID(int id)
    {
        questTable.TryGetValue(id,out  var data);
        return data;
    }
    public override void Load(string path)
    {
        string fullPath = string.Format(FormatPath, path);
        TextAsset data = Resources.Load<TextAsset>(fullPath);

        using (var reader = new StringReader(data.text))
        using (var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            var records = csvReader.GetRecords<QuestData>();
            foreach (var record in records)
            {
                questTable.Add(record.ID, record);
            }
        }
    }
}
