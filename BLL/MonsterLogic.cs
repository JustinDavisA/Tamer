using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using ApplicationLogger;

namespace BLL
{
    public class MonsterLogic : IMonsterLogic
    {
        private ILoggerIO logger;
        private IMonsterDAO monsterData;
        public List<Monster> beasts = new List<Monster>();
        public MonsterLogic(IMonsterDAO data, ILoggerIO log)
        {
            logger = log;
            monsterData = data;
        }

        public List<Monster> GetMonsters()
        {
            try
            {
                ReadMonsters();
                logger.LogError("Event", "The user was able to get the monsters", "Class: MonsterLogic & Method Name:GetMonsters");
                return beasts;
            }
            catch (Exception e)
            {
                logger.LogError("Error", "The user was not able to get the monster", "Class: MonsterLogic & Method Name:GetMonsters");
                return null;
            }
        }
        public void ReadMonsters()
        {
            List<MonsterDM> monsters = monsterData.GetMonsters();
            foreach (MonsterDM monster in monsters)
            {
                beasts.Add(Map(monster));
            }
        }
        public Monster GetMonsterById(int id)
        {
            try
            {
                logger.LogError("Event", "The user was able to get the monster by ID", "Class: MonsterLogic & Method Name:GetMonsterById");
                return Map(monsterData.GetMonsterById(id));
            }
            catch (Exception e)
            {
                logger.LogError("Event", "The user was not able to get the monster by ID", "Class: MonsterLogic & Method Name:GetMonsterById");
                return null;
            }
        }
        public Monster Map(MonsterDM monsters)
        {
            Monster beast = new Monster();
            beast.Id = monsters.Id;
            beast.MonsterName = monsters.MonsterName;
            beast.MonsterHealth = monsters.MonsterHealth;
            beast.AttackName = monsters.AttackName;
            beast.AttackDamage = monsters.AttackDamage;
            return beast;
        }
        public MonsterDM Map(Monster monsters)
        {
            MonsterDM beast = new MonsterDM();
            beast.Id = monsters.Id;
            beast.MonsterName = monsters.MonsterName;
            beast.MonsterHealth = monsters.MonsterHealth;
            beast.AttackName = monsters.AttackName;
            beast.AttackDamage = monsters.AttackDamage;
            return beast;
        }
        public void CreateMonster(Monster monster)
        {
            try
            {
                monsterData.AddMonster(Map(monster));
                logger.LogError("Event", "The user was able to get the monster", "Class: monsterLogic & Method Name:Createmonster");
            }
            catch (Exception e)
            {
                logger.LogError("Error", "The user was not able to get the monster", "Class: monsterLogic & Method Name:Createmonster");
            }
        }
        public void EditMonster(Monster monster)
        {
            try
            {
                monsterData.UpdateMonster(Map(monster));
                logger.LogError("Event", "The user was able to edit the monster", "Class: monsterLogic & Method Name:Editmonster");
            }
            catch (Exception e)
            {
                logger.LogError("Error", "The user was not able to edit the monster", "Class: monsterLogic & Method Name:Editmonster");
            }
        }
        public void DeleteMonster(Monster monster) //remove monster from fake database (List)
        {
            try
            {
                monsterData.RemoveMonster(Map(monster));
                logger.LogError("Event", "The user was able to delete the monster", "Class: monsterLogic & Method Name:Deletemonster");
            }
            catch (Exception e)
            {
                logger.LogError("Error", "The user was not able to delete the monster", "Class: monsterLogic & Method Name:Deletemonster");
            }
        }
        public int GetMonsterValues(Monster monster1, Monster monster2)
        {
            // add monster stats together to get a total value
            int monsterOneValue = monster1.MonsterHealth + monster1.AttackDamage;
            int monsterTwoValue = monster2.MonsterHealth + monster2.AttackDamage;
            if(monsterOneValue > monsterTwoValue)
            {
                return monster1.Id;
            }
            else if(monsterOneValue < monsterTwoValue)
            {
                return monster2.Id;
            }
            else
            {
                return 0;
            }
        }
    }
}
