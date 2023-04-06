using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DistSysAcwServer.Controllers;

namespace DistSysAcwServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        public UserController(Models.UserContext dbcontext) : base(dbcontext) { }


        #region TASK1
        //    TODO: add api/talkback/hello response
        [HttpGet("hello")]
        public string Get()
        {
            return "Hello World";
        }
        #endregion
    }
}
