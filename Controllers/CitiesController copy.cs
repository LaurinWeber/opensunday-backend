using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenSundayApi.Models;


namespace OpenSundayApi.Controllers
{
    #region CitiesController
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly OpenSundayContext _context;

        public CitiesController(OpenSundayContext context)
        {
            _context = context;
        }
        #endregion

     // GET: api/Cities
    [HttpGet]
    public async Task<ActionResult<IEnumerable<City>>> GetCities()
    {
      //return all cities from db
      return await _context.City.ToListAsync();
    }

    //get locations by city NPA
  #region snippet_GetByName
    // GET: api/Cities/3704
    [HttpGet("{NPA}")]
    public async Task<ActionResult<IEnumerable<Location>>> GetCategories(int NPA)
    {
      //get list of locations from specified NPA
      var locations = await _context.Location.Where(location => (location.FK_City == NPA)).ToListAsync();
  
      if (locations == null)
      {
        
        return NotFound();
      }

      return locations;
    }
    #endregion
  }
}