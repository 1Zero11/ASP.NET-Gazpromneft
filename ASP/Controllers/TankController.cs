using DataLib;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLib.Models;

namespace Lesson2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TankController : ControllerBase
    {
        private readonly SerializationManager serialManager;
        private readonly DBManager dBManager;
        public TankController(SerializationManager smanager, DBManager dbmanager)
        {
            serialManager = smanager;
            dBManager = dbmanager;
        }

        // GET: api/<ValuesController>
        [HttpGet]
        public IReadOnlyCollection<Tank> Get()
        {

            return dBManager.tanks.ToArray();
        }

        // GET api/<ValuesController>/5
        [HttpGet("{name}")]
        public ActionResult<Tank> Get(string name)
        {
            try
            {
                return dBManager.FindByName(dBManager.tanks, name);
            }
            catch (InvalidOperationException e)
            {
                return NotFound(e.Message);
            }
            
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] Tank tank)
        {
            dBManager.AddTank(tank);
            serialManager.Serialize(dBManager.factories.ToArray());
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public Tank Put(int id, [FromBody] Tank tank)
        {
            return dBManager.ChangeTank(id, tank);

        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{name}")]
        public void Delete(string name)
        {
            dBManager.DeleteTank(name);
            serialManager.Serialize(dBManager.factories.ToArray());
        }
    }
}
