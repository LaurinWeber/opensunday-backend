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
    #region CategoriesController
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly OpenSundayContext _context;

        public CategoriesController(OpenSundayContext context)
        {
            _context = context;
        }
        #endregion

     // GET: api/Categories
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
    {
      //return all categories from DB
      return await _context.Category.ToListAsync();
    }

    //get locations by categoryname
  #region snippet_GetByName
    // GET: api/Categories/Restaurant
    [HttpGet("{categoryName}")]
    public async Task<ActionResult<IEnumerable<Location>>> GetCategories(string categoryName)
    {

      //get id from Category
      var categories = await _context.Category.Where(cat => (cat.Name == (categoryName))).ToListAsync();
      int catId = 0;
      foreach(var cat in categories){
        catId = cat.Id;
      }

      //get all locations that from the category
      var locations = await _context.Location.Where(location => (location.FK_Category == catId)).ToListAsync();
  
      if (locations == null)
      {
        return NotFound();
      }

      return locations;
    }
    #endregion
  }
}