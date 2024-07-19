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