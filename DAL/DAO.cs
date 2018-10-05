using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace DAL
{
    public class DAO : IDAO
    {
        string connectionString = @"Server=GDC-BC-013\SQLEXPRESS;Database = TamerDatabase;Trusted_Connection=True;";

        public int Write(SqlParameter[] parameters, string statement)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(statement, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddRange(parameters);
                    connection.Open();
                    return command.ExecuteNonQuery();
                }
            }
        }
    }
}
