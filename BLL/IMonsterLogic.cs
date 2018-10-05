using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public interface IMonsterLogic
    {
        List<Monster> GetMonsters();
        void CreateMonster(Monster monster);
        void EditMonster(Monster monster);
        void DeleteMonster(Monster monster);
        Monster GetMonsterById(int id);
        int GetMonsterValues(Monster monster1, Monster monster2);
    }
}
