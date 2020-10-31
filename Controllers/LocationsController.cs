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
  #region LocationsController
  [Route("api/[controller]")]
  [ApiController]
  public class LocationsController : ControllerBase
  {
    private readonly OpenSundayContext _context;

    public LocationsController(OpenSundayContext context)
    {
      _context = context;
    }
    #endregion

    // GET: api/Locations
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Location>>> GetLocations()
    {
      return await _context.Location.ToListAsync();
    }

    #region snippet_GetByID
    // GET: api/Locations/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Location>> GetLocation(long id)
    {
      var location = await _context.Location.FindAsync(id);

      if (location == null)
      {
        
        return NotFound();
      }

      return location;
    }
    #endregion

    #region snippet_Update
    // PUT: api/Location/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutLocation(long id, Location location)
    {
      if (id != location.Id)
      {
        return BadRequest();
      }

      _context.Entry(location).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!LocationExists(id))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }

      return NoContent();
    }
    #endregion


    #region snippet_Create
    // POST: api/Locations
    [HttpPost]
    public async Task<ActionResult<Location>> PostLocation(LocationCityCat l)
    {
      //check if city exists
      var city = await _context.City.FindAsync(l.NPA);

      //add city
      if(city == null){
      City c = new City();
      c.NPA = l.NPA;
      c.Name = l.CityName;

        _context.City.Add(c);
        await _context.SaveChangesAsync();
      }

      //get id from Category
      var categories = await _context.Category.Where(cat => (cat.Name == (l.CategoryName))).ToListAsync();
      int catId = 0;
      foreach(var cat in categories){
        catId = cat.Id;
      }


      Location location = new Location();
      location.Id = l.Id;
      location.Name = l.Name;
      // Add creator ID based on the Auth0 User ID found in the JWT token
      location.Creator = User.Claims.First(i => i.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;
      location.Latitude = l.Latitude;
      location.Longitude = l.Longitude;
      location.Address = l.Address;
      location.Telephone = l.Telephone;
      location.OpeningTime =l.OpeningTime;
      location.ClosingTime = l.ClosingTime;
      location.FK_Category = catId;
      location.FK_City = l.NPA;

      //check if location exists if not do not add (by lat, long and name)
      var locations = await _context.Location.Where(loc => (loc.Name == location.Name)).ToListAsync();
  
      if (locations != null)
      {
        foreach(var lc in locations){
          if((lc.Longitude == location.Longitude) && (lc.Latitude == location.Latitude)){
            return null;
          }
        }
      }
      _context.Location.Add(location);
      await _context.SaveChangesAsync();

      return CreatedAtAction(nameof(GetLocation), new { id = location.Id }, location);
    }
    #endregion

    #region snippet_Delete
    // DELETE: api/Locations/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<Location>> DeleteLocation(long id)
    {
      var location = await _context.Location.FindAsync(id);
      if (location == null)
      {
        return NotFound();
      }

      _context.Location.Remove(location);
      await _context.SaveChangesAsync();

      return location;
    }
    #endregion

    private bool LocationExists(long id)
    {
      return _context.Location.Any(e => e.Id == id);
    }
  }
}