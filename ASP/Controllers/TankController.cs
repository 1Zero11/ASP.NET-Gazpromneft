using DataLib;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLib.Models;
using ASP.Data;

namespace Lesson2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TankController : ControllerBase
    {
        private readonly SerializationManager serialManager;
        private readonly DBManager dBManager;
        private readonly SQLTankRepository repository;

        public TankController(SerializationManager smanager, DBManager dbmanager)
        {
            serialManager = smanager;
            dBManager = dbmanager;
            repository = new SQLTankRepository();
        }

        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<Tank> Get()
        {

            //return dBManager.tanks.ToArray();
            return repository.GetItemList();
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public ActionResult<Tank> Get(int id)
        {
            try
            {
                //return dBManager.FindByName(dBManager.tanks, name);
                return repository.GetItem(id);
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
            /*
            dBManager.AddTank(tank);
            serialManager.Serialize(dBManager.factories.ToArray());
            */
            repository.Create(tank);
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public Tank Put(int id, [FromBody] Tank tank)
        {
            //return dBManager.ChangeTank(id, tank);
            repository.Update(tank);
            return repository.GetItem(id);
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            /*
            dBManager.DeleteTank(name);
            serialManager.Serialize(dBManager.factories.ToArray());
            */
            repository.Delete(id);
        }
    }
}
