using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DAL
{
    public interface IDAO
    {
        int Write(SqlParameter[] parameters, string statement);
    }
}
