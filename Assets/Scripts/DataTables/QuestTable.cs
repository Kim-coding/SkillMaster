using CsvHelper;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using UnityEngine;

public class QuestData
{
    public int QuestID {  get; set; }
    public int Division {  get; set; } // Ÿ��
    public int Targetvalue {  get; set; }  // ����Ʈ �Ϸ�����
    public string reward { get; set; } // ����
    public int rewardvalue { get;  set; } // ���� ��ġ 
    public string rewardicon { get; set; } // ������
    public int Next_Quest { get; set; } // ���� ����Ʈ ���̵�
    public int StringId { get; set; } // ����
    public int Level { get; set; } //����

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
                questTable.Add(record.QuestID, record);
            }
        }
    }
}
