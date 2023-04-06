using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DistSysAcwServer.Controllers;
using DistSysAcwServer.DataAccess;
using DistSysAcwServer.Models;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Microsoft.Extensions.Primitives;

namespace DistSysAcwServer.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : BaseController
    {
        readonly MyUserCRUD _myUserCRUDAccess = new MyUserCRUD();
        public UserController(Models.UserContext dbcontext) : base(dbcontext) 
        {
           
        }
        
        [HttpGet("new")]
        public IActionResult Name([FromQuery(Name = "username")]string userName)
        {
            if (userName == null) return StatusCode(200,"False - User Does Not Exist! Did you mean to do a POST to create a new user?");

            bool user = _myUserCRUDAccess.CheckUser(userName);

            if (user == true) return StatusCode(200, "True - User Does Exist! Did you mean to do a POST to create a new user?");           
            else return StatusCode(200, "False - User Does Not Exist! Did you mean to do a POST to create a new user?");
            
            

        }

        [HttpPost("new")]
        public IActionResult Post([FromBody]string userName)
        {
            if (userName == "") { return StatusCode(400, "Oops. Make sure your body contains a string with your username and your Content-Type is Content-Type:application/json"); }
            bool user = _myUserCRUDAccess.CheckUser(userName);

            
            if (user == false) return StatusCode(200,_myUserCRUDAccess.Create(userName)) ;
            else return StatusCode(403, "Oops. This username is already in use.Please try again with a new username.");

        }

        [HttpDelete("removeuser")]
        [Authorize(Roles = "Admin, User")]
        public IActionResult Delete([FromQuery(Name = "username")]string userName)
        {
            string apiKey = HttpContext.Request.Headers["Api-Key"].FirstOrDefault();
            
            if(_myUserCRUDAccess.CheckApi(apiKey) == true)
              {
                  if (_myUserCRUDAccess.CheckApiUser(apiKey, userName) == true) {  _myUserCRUDAccess.DeleteUser(apiKey);  return StatusCode(200, true); }
                  else   return StatusCode(200, false); 
             }
             else  return StatusCode(200, false); 
             
            
        }

        [HttpPost("changerole")]
        [Authorize(Roles = "Admin")]
        public IActionResult ChangeRole([FromBody] string body)
        {
            string apiKey = HttpContext.Request.Headers["Api-Key"].FirstOrDefault();
            string[] bodyArray = body.Split(' ');
            string username = bodyArray[1].Replace(",", ""); string role = bodyArray[3];

            if (_myUserCRUDAccess.CheckUser(username) == true) {
                if (role == "Admin" || role == "User") {  _myUserCRUDAccess.ChangeRole(username, role); return StatusCode(200, "DONE"); }
                
                return StatusCode(400, "NOT DONE: Role does not exist");
            }
            else return StatusCode(400, "NOT DONE: Username does not exist");  
        }
    } 
}
