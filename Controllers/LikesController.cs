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

        //get all likes the demanding user has posted
        // GET: api/Likes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Like>>> GetLikes()
        {
            //get userId from jwt
             var user_id = User.Claims.First(i => i.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;

            return await _context.Like.Where(l => (l.FK_User == (user_id))).ToListAsync();
        }

    //get all likes from a specific location
    #region snippet_GetByID_Location
    // GET: api/Likes/5
    [HttpGet("{id}")]
    public async Task<ActionResult<IEnumerable<Like>>> GetLikes(int id)
    {

      //get the list of likes from location
      var likes = await _context.Like.Where(l => (l.FK_Location == (id))).ToListAsync();

      if (likes == null)
      {
        return null;
      }
      return likes;
    }
    #endregion

    #region snippet_Create
    // POST: api/Like (Location Id & isLiked)
    [HttpPost]
    public async Task<ActionResult<Like>> PostLike(Like like)
    {

      //retrieve Userid from Token
      var user_id = User.Claims.First(i => i.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;
      like.FK_User = user_id;
      
      var users = await _context.User.Where(u => (u.Id == (user_id))).ToListAsync();
      //add user to db
      if(!(users.Count >0)){
        User u = new User();
        u.Id = user_id;
        _context.User.Add(u);
        await _context.SaveChangesAsync();
      }

            //Add Like to Db
            _context.Like.Add(like);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLikes), new { FK_User = like.FK_User}, like);
    }
    #endregion

    #region snippet_Delete
    // DELETE: api/Likes/5
    //delete all likes from a given locaiton
    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> DeleteLikes(int id)
    {

      var likes = await _context.Like.Where(l => (l.FK_Location == (id))).ToListAsync();
      if (likes == null)
      {
        return false;
      }
      else{
        //delete one entry after the other
      foreach(var l in likes){
        _context.Like.Remove(l);
      }

      //save the changes
      await _context.SaveChangesAsync();
      return true;

      }

    }
    #endregion

  }
}