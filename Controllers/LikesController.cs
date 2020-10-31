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
    public class LikesController : ControllerBase
    {
        private readonly OpenSundayContext _context;

        public LikesController(OpenSundayContext context)
        {
            _context = context;
        }
        #endregion

        // GET: api/Likes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Like>>> GetLikes()
        {
            // Add creator ID based on the Auth0 User ID found in the JWT token
             var user_id = User.Claims.First(i => i.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;

            return await _context.Like.Where(l => (l.FK_User == (user_id))).ToListAsync();
        }

                // GET: api/Likes
    #region snippet_GetByID
    // GET: api/Locations/5
    [HttpGet("{id}")]
    public async Task<ActionResult<IEnumerable<Like>>> GetLikes(long id)
    {
      var likes = await _context.Like.Where(l => (l.FK_Location == (id))).ToListAsync();

      if (likes == null)
      {
        
        return NotFound();
      }

      return likes;
    }
    #endregion

    #region snippet_Create
    // POST: api/Locations
    [HttpPost]
    public async Task<ActionResult<Like>> PostLike(Like like)
    {
            // Add creator ID based on the Auth0 User ID found in the JWT token
             var user_id = User.Claims.First(i => i.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;
            like.FK_User = user_id;

            _context.Like.Add(like);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLikes), new { FK_User = like.FK_User}, like);
    }
    #endregion

    }
}