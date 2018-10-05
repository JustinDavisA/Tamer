using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationLogger;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class MonsterDAO : IMonsterDAO
    {
        private ILoggerIO logger;
        private IDAO dataWriter;
        string connectionString = @"Server=GDC-BC-013\SQLEXPRESS;Database = TamerDatabase;Trusted_Connection=True;";

        public MonsterDAO(IDAO data, ILoggerIO log)
        {
            dataWriter = data;
            logger = log;
        }
        public List<MonsterDM> ReadMonsters(SqlParameter[] parameters, string statement)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(statement, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    connection.Open();
                    SqlDataReader data = command.ExecuteReader();
                    List<MonsterDM> monsters = new List<MonsterDM>();
                    while (data.Read())
                    {
                        MonsterDM monster = new MonsterDM();
                        monster.Id = Convert.ToInt32(data["Id"]);
                        monster.MonsterName = data["MonsterName"].ToString();
                        monster.MonsterHealth = Convert.ToInt32(data["MonsterHealth"]);
                        monster.AttackName = data["AttackName"].ToString();
                        monster.AttackDamage = Convert.ToInt32(data["AttackDamage"]);
                        monsters.Add(monster);
                    }
                    return monsters;
                }
            }
        }
        public List<MonsterDM> GetMonsters()
        {
            try
            {
                logger.LogError("Event", "The user was able to get the monsters", "Class: MonsterDAO & Method Name: GetMonsters");
                return ReadMonsters(null, "GetMonsters");
            }
            catch (Exception e)
            {
                logger.LogError("Error", "The user was not able to get the monsters", "Class: MonsterDAO & Method Name: GetMonsters");
                return null;
            }
        }
        public void AddMonster(MonsterDM monster)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MonsterName",monster.MonsterName)
                ,new SqlParameter("@MonsterHealth",monster.MonsterHealth)
                ,new SqlParameter("@AttackName",monster.AttackName)
                ,new SqlParameter("@AttackDamage",monster.AttackDamage)
            };
            try
            {
                dataWriter.Write(parameters, "CreateMonster");
                logger.LogError("Event", "The user was able to add a monster", "Class: MonsterDAO & Method Name: AddMonster");

            }
            catch (Exception e)
            {
                logger.LogError("Error", "The user was not able to add a monster", "Class: MonsterDAO & Method Name: AddMonster");
            }
        }
        public void UpdateMonster(MonsterDM monster)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Id",monster.Id)
                ,new SqlParameter("@MonsterName",monster.MonsterName)
                ,new SqlParameter("@MonsterHealth",monster.MonsterHealth)
                ,new SqlParameter("@AttackName",monster.AttackName)
                ,new SqlParameter("@AttackDamage",monster.AttackDamage)
            };
            try
            {
                dataWriter.Write(parameters, "UpdateMonster");
                logger.LogError("Event", "The user was able to update a monster", "Class: MonsterDAO & Method Name: UpdateMonster");

            }
            catch (Exception e)
            {
                logger.LogError("Error", "The user was not able to update a monster", "Class: monsterDAO & Method Name: Updatemonster");
            }
        }
        public void RemoveMonster(MonsterDM monster)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Id",monster.Id)
            };
            try
            {
                dataWriter.Write(parameters, "DeleteMonster");
                logger.LogError("Event", "The user was able to remove a monster", "Class: MonsterDAO & Method Name: RemoveMonster");
            }
            catch (Exception e)
            {
                logger.LogError("Error", "The user was not able to remove a monster", "Class: monsterDAO & Method Name: Removemonster");
            }
        }
        public MonsterDM GetMonsterById(int id)
        {
            SqlParameter[] parameters = new SqlParameter[]
                 {
                     new SqlParameter("@Id",id)
                 };
            return ReadMonsters(parameters, "GetMonsterById")[0];
        }
    }
}
