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
  #region UsersController
  [Route("api/[controller]")]
  [ApiController]
  public class UsersController : ControllerBase
  {
    private readonly OpenSundayContext _context;

    public UsersController(OpenSundayContext context)
    {
      _context = context;
    }
    #endregion

    // GET: api/Users
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
      return await _context.Users.ToListAsync();
    }

    #region snippet_GetByID
    // GET: api/Users/5
    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(long id)
    {
      var User = await _context.Users.FindAsync(id);

      if (User == null)
      {
        return NotFound();
      }

      return User;
    }
    #endregion

    #region snippet_Update
    // PUT: api/User/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutUser(long id, User User)
    {
      if (id != User.Id)
      {
        return BadRequest();
      }

      _context.Entry(User).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!UserExists(id))
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
    /*
    // POST: api/Users
    [HttpPost]
    public async Task<ActionResult<User>> PostUser(User User)
    {
      // Add creator ID based on the Auth0 User ID found in the JWT token
      User.Creator = User.Claims.First(i => i.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;

      _context.Users.Add(User);
      await _context.SaveChangesAsync();

      return CreatedAtAction(nameof(GetUser), new { id = User.Id }, User);
    }
    */
    #endregion

    #region snippet_Delete
    // DELETE: api/Users/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<User>> DeleteUser(long id)
    {
      var User = await _context.Users.FindAsync(id);
      if (User == null)
      {
        return NotFound();
      }

      _context.Users.Remove(User);
      await _context.SaveChangesAsync();

      return User;
    }
    #endregion

    private bool UserExists(long id)
    {
      return _context.Users.Any(e => e.Id == id);
    }
  }
}