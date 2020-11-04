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
    #region UserController
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly OpenSundayContext _context;

        public UserController(OpenSundayContext context)
        {
            _context = context;
        }
        #endregion


    //check if location has been liked by user
    #region snippet_GetByID
    // GET: api/User/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Like>> GetUser(int id)
    {

      var user_id = User.Claims.First(i => i.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;
      
      //get the list of likes from location
      var likes = await _context.Like.Where(l => (l.FK_Location == (id))).ToListAsync();
      Like like = new Like();
      like = null;
        foreach(var l in likes){
          if(l.FK_User == user_id) {
            like = l; //if exists return null
          }
        }
      
      return like;
    }
    #endregion

        #region snippet_Delete
    // DELETE: api/Likes/5
    //user: dislike a locaion
    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> DeleteUser(int id)
    {

      var user_id = User.Claims.First(i => i.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;

      var likes = await _context.Like.Where(l => (l.FK_Location == (id))).ToListAsync();
      if (likes == null)
      {
        return false;
      }
      else{
        //delete one entry after the other
      foreach(var l in likes){
        if(l.FK_User == user_id){
          _context.Like.Remove(l);
                //save the changes
      await _context.SaveChangesAsync();
      return true;
        }
      }
      return false;
      }
    }
    #endregion
}
}
