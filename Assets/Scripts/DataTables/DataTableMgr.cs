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
        //MonsterTable monsterTable = new MonsterTable();
        //monsterTable.Load(DataTableIds.monster);
        //tables.Add(DataTableIds.monster, monsterTable);

        QuestTable questTable = new QuestTable();
        questTable.Load(DataTableIds.quest);
        tables.Add(DataTableIds.quest, questTable);

        StringTable stringTable = new StringTable();
        stringTable.Load(DataTableIds.String);
        tables.Add(DataTableIds.String, stringTable);
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