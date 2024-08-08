using CsvHelper;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using UnityEngine;


public class GachaData
{

    public int EQ_gachaLv { get; set; }
    public int gachaRequestValue { get; set; }
    public int gachaReward { get; set; }

    public StuffData getReward
    {
        get
        {
            return DataTableMgr.Get<StuffTable>(DataTableIds.stuff).GetID(gachaReward);
        }
    }
    public int gachaRewardValue { get; set; }
    public int gachaID { get; set; }

    public GachaGradeData getGachaGrade
    {
        get
        {
            return DataTableMgr.Get<GachaGradeTable>(DataTableIds.gachaGrade).GetID(gachaID);
        }
    }
}


public class GachaGradeData
{

    public int GachaID { get; set; }
    public string List { get; set; }
    public int Gacha1_ID { get; set; }
    public float Gacha1_Odds { get; set; }
    public int Gacha2_ID { get; set; }
    public float Gacha2_Odds { get; set; }
    public int Gacha3_ID { get; set; }
    public float Gacha3_Odds { get; set; }
    public int Gacha4_ID { get; set; }
    public float Gacha4_Odds { get; set; }
    public int Gacha5_ID { get; set; }
    public float Gacha5_Odds { get; set; }
    public int Gacha6_ID { get; set; }
    public float Gacha6_Odds { get; set; }

    //public GachaPartData getGrade1
    //{
    //    get
    //    {
    //        return DataTableMgr.Get<GachaPartTable>(DataTableIds.gachaPart).GetID(Gacha1_ID);
    //    }
    //}
    //public GachaPartData getGrade2
    //{
    //    get
    //    {
    //        return DataTableMgr.Get<GachaPartTable>(DataTableIds.gachaPart).GetID(Gacha2_ID);
    //    }
    //}
    //public GachaPartData getGrade3
    //{
    //    get
    //    {
    //        return DataTableMgr.Get<GachaPartTable>(DataTableIds.gachaPart).GetID(Gacha3_ID);
    //    }
    //}
    //public GachaPartData getGrade4
    //{
    //    get
    //    {
    //        return DataTableMgr.Get<GachaPartTable>(DataTableIds.gachaPart).GetID(Gacha4_ID);
    //    }
    //}
    //public GachaPartData getGrade5
    //{
    //    get
    //    {
    //        return DataTableMgr.Get<GachaPartTable>(DataTableIds.gachaPart).GetID(Gacha5_ID);
    //    }
    //}
    //public GachaPartData getGrade6
    //{
    //    get
    //    {
    //        return DataTableMgr.Get<GachaPartTable>(DataTableIds.gachaPart).GetID(Gacha6_ID);
    //    }
    //}
}

public class GachaPartData
{

    public int GachaID { get; set; }
    public string List { get; set; }
    public int Gacha1_ID { get; set; }
    public float Gacha1_Odds { get; set; }
    public int Gacha2_ID { get; set; }
    public float Gacha2_Odds { get; set; }
    public int Gacha3_ID { get; set; }
    public float Gacha3_Odds { get; set; }
    public int Gacha4_ID { get; set; }
    public float Gacha4_Odds { get; set; }
    public int Gacha5_ID { get; set; }
    public float Gacha5_Odds { get; set; }
    public int Gacha6_ID { get; set; }
    public float Gacha6_Odds { get; set; }

    //public GachaItemData getPart1
    //{
    //    get
    //    {
    //        return DataTableMgr.Get<GachaItemTable>(DataTableIds.gachaItem).GetID(Gacha1_ID);
    //    }
    //}
    //public GachaItemData getPart2
    //{
    //    get
    //    {
    //        return DataTableMgr.Get<GachaItemTable>(DataTableIds.gachaItem).GetID(Gacha2_ID);
    //    }
    //}
    //public GachaItemData getPart3
    //{
    //    get
    //    {
    //        return DataTableMgr.Get<GachaItemTable>(DataTableIds.gachaItem).GetID(Gacha3_ID);
    //    }
    //}
    //public GachaItemData getPart4
    //{
    //    get
    //    {
    //        return DataTableMgr.Get<GachaItemTable>(DataTableIds.gachaItem).GetID(Gacha4_ID);
    //    }
    //}
    //public GachaItemData getPart5
    //{
    //    get
    //    {
    //        return DataTableMgr.Get<GachaItemTable>(DataTableIds.gachaItem).GetID(Gacha5_ID);
    //    }
    //}
    //public GachaItemData getPart6
    //{
    //    get
    //    {
    //        return DataTableMgr.Get<GachaItemTable>(DataTableIds.gachaItem).GetID(Gacha6_ID);
    //    }
    //}
}


public class GachaItemData
{

    public int GachaID { get; set; }
    public string List { get; set; }
    public int Gacha1_ID { get; set; }
    public float Gacha1_Odds { get; set; }
    public int Gacha2_ID { get; set; }
    public float Gacha2_Odds { get; set; }
    public int Gacha3_ID { get; set; }
    public float Gacha3_Odds { get; set; }
    public int Gacha4_ID { get; set; }
    public float Gacha4_Odds { get; set; }
    public int Gacha5_ID { get; set; }
    public float Gacha5_Odds { get; set; }

    //public EquipData getItem1
    //{
    //    get
    //    {
    //        return DataTableMgr.Get<EquipmentTable>(DataTableIds.equipment).GetID(Gacha1_ID);
    //    }
    //}
    //public EquipData getItem2
    //{
    //    get
    //    {
    //        return DataTableMgr.Get<EquipmentTable>(DataTableIds.equipment).GetID(Gacha2_ID);
    //    }
    //}
    //public EquipData getItem3
    //{
    //    get
    //    {
    //        return DataTableMgr.Get<EquipmentTable>(DataTableIds.equipment).GetID(Gacha3_ID);
    //    }
    //}
    //public EquipData getItem4
    //{
    //    get
    //    {
    //        return DataTableMgr.Get<EquipmentTable>(DataTableIds.equipment).GetID(Gacha4_ID);
    //    }
    //}
    //public EquipData getItem5
    //{
    //    get
    //    {
    //        return DataTableMgr.Get<EquipmentTable>(DataTableIds.equipment).GetID(Gacha5_ID);
    //    }
    //}
}

public class GachaTable : DataTable
{

    private Dictionary<int, GachaData> gachaTable = new Dictionary<int, GachaData>();

    public List<GachaData> gachaDatas
    {
        get
        {
            return gachaTable.Values.ToList();
        }
    }

    public GachaData GetID(int id)
    {
        gachaTable.TryGetValue(id, out var data);
        return data;
    }
    public override void Load(string path)
    {
        string fullPath = string.Format(FormatPath, path);
        TextAsset data = Resources.Load<TextAsset>(fullPath);

        using (var reader = new StringReader(data.text))
        using (var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            var records = csvReader.GetRecords<GachaData>();
            foreach (var record in records)
            {
                gachaTable.Add(record.EQ_gachaLv, record);
            }
        }
    }
}
public class GachaGradeTable : DataTable
{

    private Dictionary<int, GachaGradeData> gachaGradeTable = new Dictionary<int, GachaGradeData>();

    public List<GachaGradeData> gachaGradeDatas
    {
        get
        {
            return gachaGradeTable.Values.ToList();
        }
    }

    public GachaGradeData GetID(int id)
    {
        gachaGradeTable.TryGetValue(id, out var data);
        return data;
    }
    public override void Load(string path)
    {
        string fullPath = string.Format(FormatPath, path);
        TextAsset data = Resources.Load<TextAsset>(fullPath);

        using (var reader = new StringReader(data.text))
        using (var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            var records = csvReader.GetRecords<GachaGradeData>();
            foreach (var record in records)
            {
                gachaGradeTable.Add(record.GachaID, record);
            }
        }
    }
}

public class GachaPartTable : DataTable
{

    private Dictionary<int, GachaPartData> gachaPartTable = new Dictionary<int, GachaPartData>();

    public List<GachaPartData> gachaPartDatas
    {
        get
        {
            return gachaPartTable.Values.ToList();
        }
    }

    public GachaPartData GetID(int id)
    {
        gachaPartTable.TryGetValue(id, out var data);
        return data;
    }
    public override void Load(string path)
    {
        string fullPath = string.Format(FormatPath, path);
        TextAsset data = Resources.Load<TextAsset>(fullPath);

        using (var reader = new StringReader(data.text))
        using (var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            var records = csvReader.GetRecords<GachaPartData>();
            foreach (var record in records)
            {
                gachaPartTable.Add(record.GachaID, record);
            }
        }
    }
}

public class GachaItemTable : DataTable
{

    private Dictionary<int, GachaItemData> gachaItemTable = new Dictionary<int, GachaItemData>();

    public List<GachaItemData> gachaItemDatas
    {
        get
        {
            return gachaItemTable.Values.ToList();
        }
    }

    public GachaItemData GetID(int id)
    {
        gachaItemTable.TryGetValue(id, out var data);
        return data;
    }
    public override void Load(string path)
    {
        string fullPath = string.Format(FormatPath, path);
        TextAsset data = Resources.Load<TextAsset>(fullPath);

        using (var reader = new StringReader(data.text))
        using (var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            var records = csvReader.GetRecords<GachaItemData>();
            foreach (var record in records)
            {
                gachaItemTable.Add(record.GachaID, record);
            }
        }
    }
}