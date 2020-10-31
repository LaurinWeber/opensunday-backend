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

     // GET: api/Categories
    [HttpGet]
    public async Task<ActionResult<IEnumerable<City>>> GetCities()
    {
      return await _context.City.ToListAsync();
    }

//get locations by city NPA
  #region snippet_GetByName
    // GET: api/Locations/5
    [HttpGet("{NPA}")]
    public async Task<ActionResult<IEnumerable<Location>>> GetCategories(int NPA)
    {
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