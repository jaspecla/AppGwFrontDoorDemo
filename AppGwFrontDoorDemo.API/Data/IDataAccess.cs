using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace AppGwFrontDoorDemo.API.Data
{
  public interface IDataAccess
  {
    DbCommand GetCommand(DbConnection connection, string commandText, CommandType commandType);
    Task<DbDataReader> GetDataReaderAsync(string commandText, List<DbParameter> parameters, CommandType commandType = CommandType.Text);
  }
}
