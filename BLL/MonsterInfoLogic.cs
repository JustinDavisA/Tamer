using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using ApplicationLogger;

namespace BLL
{
    public class MonsterInfoLogic : IMonsterInfoLogic
    {
        private ILoggerIO logger;
        private IMonsterDAO monsterData;
        public List<Monster> beasts = new List<Monster>();
        public MonsterInfoLogic(IMonsterDAO data, ILoggerIO log)
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
            beast.Wins = monsters.Wins;
            beast.Losses = monsters.Losses;
            beast.Health = monsters.Health;
            //beast.Stamina = monsters.Stamina;
            //beast.Strength = monsters.Strength;
            //beast.Agility = monsters.Agility;
            //beast.Constitution = monsters.Constitution;
            return beast;
        }
        public MonsterDM Map(Monster monsters)
        {
            MonsterDM beast = new MonsterDM();
            beast.Id = monsters.Id;
            beast.MonsterName = monsters.MonsterName;
            beast.Wins = monsters.Wins;
            beast.Losses = monsters.Losses;
            beast.Health = monsters.Health;
            //beast.Stamina = monsters.Stamina;
            //beast.Strength = monsters.Strength;
            //beast.Agility = monsters.Agility;
            //beast.Constitution = monsters.Constitution;
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
        public void GetMonsterHealth()
        {

        }
    }
}
