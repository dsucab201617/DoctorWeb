using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorWebWCFServicio.Models.DataAccessObject.Common
{
    public interface IDAO
    {
        void OpenConnection();
        void ExecuteQuery(string query, object parameters = null, ForEachRow doThis = null);
        void ExecuteNoQuery(string query, object parameters = null);
        void ExecuteStoreProcedure(string name, object parameters);
        void ExecuteStoreProcedureWithResult(string name, object parameters = null, ForEachRow doThis = null);
        void CloseConnection();
    }
}
