using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class DataTableMgr
{
    private static Dictionary<string, DataTable> tables = new Dictionary<string, DataTable>();

    static DataTableMgr()
    {
        QuestTable questTable = new QuestTable();
        questTable.Load(DataTableIds.quest);
        tables.Add(DataTableIds.quest, questTable);

        StringTable stringTable = new StringTable();
        stringTable.Load(DataTableIds.String);
        tables.Add(DataTableIds.String, stringTable);

        UpgradeTable upgradeTable = new UpgradeTable();
        upgradeTable.Load(DataTableIds.upgrade);
        tables.Add(DataTableIds.upgrade, upgradeTable);

        SkillTable skillTable = new SkillTable();
        skillTable.Load(DataTableIds.skill);
        tables.Add(DataTableIds.skill, skillTable);

        SkillDownTable skillDownTable = new SkillDownTable();
        skillDownTable.Load(DataTableIds.skillDown);
        tables.Add(DataTableIds.skillDown, skillDownTable);

        BossTable bossTable = new BossTable();
        bossTable.Load(DataTableIds.boss);
        tables.Add(DataTableIds.boss, bossTable);

        MonsterTable monsterTable = new MonsterTable();
        monsterTable.Load(DataTableIds.monster);
        tables.Add(DataTableIds.monster, monsterTable);

        StageTable stageTable = new StageTable();
        stageTable.Load(DataTableIds.stage);
        tables.Add(DataTableIds.stage, stageTable);

        OptionNumberTable optionNumberTable = new OptionNumberTable();
        optionNumberTable.Load(DataTableIds.optionNumber);
        tables.Add(DataTableIds.optionNumber, optionNumberTable);

        OptionTable optionTable = new OptionTable();
        optionTable.Load(DataTableIds.option);
        tables.Add(DataTableIds.option, optionTable);

        EquipmentTable equipmentTable = new EquipmentTable();
        equipmentTable.Load(DataTableIds.equipment);
        tables.Add(DataTableIds.equipment, equipmentTable);

        GachaTable gachaTable = new GachaTable();
        gachaTable.Load(DataTableIds.gachaLevel);
        tables.Add(DataTableIds.gachaLevel, gachaTable);

        GachaGradeTable gachaGradeTable = new GachaGradeTable();
        gachaGradeTable.Load(DataTableIds.gachaGrade);
        tables.Add(DataTableIds.gachaGrade, gachaGradeTable);

        GachaPartTable gachaPartTable = new GachaPartTable();
        gachaPartTable.Load(DataTableIds.gachaPart);
        tables.Add(DataTableIds.gachaPart, gachaPartTable);

        GachaItemTable gachaItemTable = new GachaItemTable();
        gachaItemTable.Load(DataTableIds.gachaItem);
        tables.Add(DataTableIds.gachaItem, gachaItemTable);

        EquipUpgradeTable equipUpgradeTable = new EquipUpgradeTable();
        equipUpgradeTable.Load(DataTableIds.equipmentUpgrade);
        tables.Add(DataTableIds.equipmentUpgrade, equipUpgradeTable);

        StuffTable stuffTable = new StuffTable();
        stuffTable.Load(DataTableIds.stuff);
        tables.Add(DataTableIds.stuff, stuffTable);

        CombinationUpgradeTable combineTable = new CombinationUpgradeTable();
        combineTable.Load(DataTableIds.cbnUpgrade);
        tables.Add(DataTableIds.cbnUpgrade, combineTable);

        OfflineRewardTable offlineRewardTable = new OfflineRewardTable();
        combineTable.Load(DataTableIds.offlineReward);
        tables.Add(DataTableIds.offlineReward, offlineRewardTable);

    }

    private static DataTable CreateDataTable(string id)
    {
        var tableTypes = new Dictionary<string, Func<DataTable>>
        {
            {"MonsterTable", () => new MonsterTable()},

        };

        if (tableTypes.ContainsKey(id))
        {
            return tableTypes[id]();
        }

        throw new Exception("Unsupported table type");
    }

    public static T Get<T>(string id) where T : DataTable
    {
        if (!tables.ContainsKey(id))
            return null;
        return tables[id] as T;
    }
}