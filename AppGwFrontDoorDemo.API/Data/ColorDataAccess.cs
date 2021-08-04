using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppGwFrontDoorDemo.API.Data
{
  public class ColorDataAccess
  {
    private IDataAccess DataAccess { get; set; }

    public ColorDataAccess(IDataAccess dataAccess)
    {
      DataAccess = dataAccess;

    }

    public async Task<IEnumerable<string>> SearchColorsAsync(string searchString)
    {
      // PURPOSELY SETTING UP SQL INJECTION ATTACK
      var query = $"SELECT Color FROM Colors WHERE Color LIKE '%{searchString}%'";

      var reader = await DataAccess.GetDataReaderAsync(query, null);

      var colors = new List<string>();

      while (await reader.ReadAsync())
      {
        var color = reader["Color"].ToString();
        colors.Add(color);
      }

      return colors;

    }

  }
}
