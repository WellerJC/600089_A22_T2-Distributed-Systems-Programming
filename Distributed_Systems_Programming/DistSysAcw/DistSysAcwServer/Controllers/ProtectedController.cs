using DistSysAcwServer.DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Text;
using System.Security.Cryptography;
using static System.Net.Mime.MediaTypeNames;

namespace DistSysAcwServer.Controllers
{
    [Route("api/protected")]
    [ApiController]

    public class ProtectedController : BaseController
    {

        readonly MyUserCRUD _myUserCRUDAccess = new MyUserCRUD();
        public ProtectedController(Models.UserContext dbcontext) : base(dbcontext)
        {
        }

        [HttpGet("hello")]
        [Authorize(Roles = "Admin, User")]
        public IActionResult Hello()
        {
            string apiKey = HttpContext.Request.Headers["Api-Key"].FirstOrDefault();
            string username = _myUserCRUDAccess.ReturnUserName(apiKey);
            return StatusCode(200, "Hello " + username);
        }

        [HttpGet("sha1")]
        [Authorize(Roles = "Admin, User")]
        public IActionResult SHA1Translator([FromQuery(Name = "message")] string message)
        {
            if(message == null || message == "") { return StatusCode(400, "BAD REQUEST"); }

            message = message.ToLower();

            using (SHA1 sha1 = SHA1.Create())
            {
                byte[] messageBytes = Encoding.UTF8.GetBytes(message);
                byte[] hashBytes = sha1.ComputeHash(messageBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }
                return StatusCode(200,sb.ToString().ToUpper());
            }
            
        }

        [HttpGet("sha256")]
        [Authorize(Roles = "Admin, User")]
        public IActionResult SHA256Translator([FromQuery(Name = "message")] string message)
        {
            if (message == null || message == "") { return StatusCode(400, "BAD REQUEST"); }

            message = message.ToLower();

            using (var sha256 = SHA256.Create())
            {
                byte[] messageBytes = Encoding.UTF8.GetBytes(message);
                byte[] hashedBytes = sha256.ComputeHash(messageBytes);

                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashedBytes)
                {
                    sb.Append(b.ToString("x2"));
                }
                return StatusCode(200,sb.ToString().ToUpper());
            }
            return StatusCode(200, "SHA256");
        }

      
    }
}
