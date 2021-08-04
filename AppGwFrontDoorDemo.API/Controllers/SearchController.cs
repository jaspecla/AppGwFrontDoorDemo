using AppGwFrontDoorDemo.API.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppGwFrontDoorDemo.API.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class SearchController : Controller
  {

    private ColorDataAccess ColorData { get; set; }

    public SearchController(ColorDataAccess colorData)
    {
      ColorData = colorData;
    }

    [HttpGet("{searchString}", Name = "Search")]
    public async Task<IActionResult> Index(string searchString)
    {
      var colors = await ColorData.SearchColorsAsync(searchString);

      return Ok(colors);

    }
  }
}
