using Azure.Core;
using Azure.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace AppGwFrontDoorDemo.API.Data
{
  public class SqlDataAccess : IDataAccess
  {
    protected string ConnectionString { get; set; }

    public SqlDataAccess(IConfiguration configuration)
    {
      this.ConnectionString = configuration["ConnectionString"];
    }

    protected async Task<SqlConnection> GetConnectionAsync()
    {
      SqlConnection connection = new SqlConnection(this.ConnectionString);

      if (connection.State != ConnectionState.Open)
      {
        await connection.OpenAsync();
      }
      return connection;
    }

    public DbCommand GetCommand(DbConnection connection, string commandText, CommandType commandType)
    {
      SqlCommand command = new SqlCommand(commandText, connection as SqlConnection);
      command.CommandType = commandType;
      return command;
    }

    public async Task<DbDataReader> GetDataReaderAsync(string commandText, List<DbParameter> parameters, CommandType commandType = CommandType.Text)
    {
      DbDataReader ds;

      try
      {
        DbConnection connection = await this.GetConnectionAsync();
        {
          DbCommand cmd = this.GetCommand(connection, commandText, commandType);
          if (parameters != null && parameters.Count > 0)
          {
            cmd.Parameters.AddRange(parameters.ToArray());
          }

          ds = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection);
        }
      }
      catch (Exception ex)
      {
        //LogException("Failed to GetDataReader for " + procedureName, ex, parameters);
        throw;
      }

      return ds;
    }
  }
}
