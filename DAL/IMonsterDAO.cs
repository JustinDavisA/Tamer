using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IMonsterDAO
    {
        List<MonsterDM> GetMonsters();
        void AddMonster(MonsterDM monster);
        void UpdateMonster(MonsterDM monster);
        void RemoveMonster(MonsterDM monster);
        MonsterDM GetMonsterById(int id);
    }
}
