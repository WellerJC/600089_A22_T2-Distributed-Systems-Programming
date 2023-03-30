using Microsoft.AspNetCore.Mvc;
using MyFirstAPI.DataAccess;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyFirstAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        readonly MyDataCRUD _myDataCRUDAccess;

        public DataController(MyDataCRUD myDataCRUDAccess)
        {
            _myDataCRUDAccess = myDataCRUDAccess;
        }
        // GET: api/<DataController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return _myDataCRUDAccess.Read();
        }

        // GET api/<DataController>/5
        [HttpGet("{id:int}")]
        public string Get(int id)
        {    
            return "Your number is " + _myDataCRUDAccess.Read()[id];
        }

        [HttpGet("{id}")]
        public string Get(string id)
        {
            return "Data not found";
        }
        [HttpGet("[action]")]
        [ActionName("Name")]
        public string Name([FromQuery]string name)
        {
            return "Your Name is " + name;
        }
        [HttpGet("[action]")]
        [ActionName("Create")]
        public void CreateData()
        {
            _myDataCRUDAccess.Create();
        }
        // POST api/<DataController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<DataController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<DataController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
