using DistSysAcwServer.DataAccess;
using Microsoft.AspNetCore.Mvc;

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
        public string Hello([FromHeader]string ApiKey) 
        {
            return "Hello";
        }
    }
}
