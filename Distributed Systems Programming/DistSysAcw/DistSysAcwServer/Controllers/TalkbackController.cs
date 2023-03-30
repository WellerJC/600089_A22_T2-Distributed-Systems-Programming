using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DistSysAcwServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TalkbackController : BaseController
    {
        
        /// <summary>
        /// Constructs a TalkBack controller, taking the UserContext through dependency injection
        /// </summary>
        /// <param name="context">DbContext set as a service in Startup.cs and dependency injected</param>
        public TalkbackController(Models.UserContext dbcontext) : base(dbcontext) { }


        #region TASK1
        //    TODO: add api/talkback/hello response
        [HttpGet("hello")]
        public IActionResult Get()
        {
            return Ok("Hello World");
        }
        #endregion


        #region TASK1
        //    TODO:
        //       add a parameter to get integers from the URI query
        //       sort the integers into ascending order
        //       send the integers back as the api/talkback/sort response
        //       conform to the error handling requirements in the spec
        [HttpGet("sort")]
        public IActionResult Get([FromQuery(Name = "integers")] IEnumerable<string> integerStrings)
        {
            if(integerStrings == null)
            {
                return Ok("[]");
            }
            List<int> integers = new List<int>();
            foreach (string integerString in integerStrings)
            {
                if (int.TryParse(integerString, out int integer))  integers.Add(integer);   
                else  return BadRequest("Invalid input: " + integerString);
            }
            integers.Sort();
            return Ok(integers);
        }
        #endregion
    }
}



       
 

