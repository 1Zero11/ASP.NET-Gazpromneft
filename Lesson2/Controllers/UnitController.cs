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
    public class UnitController : ControllerBase
    {

        private readonly SerializationManager serialManager;
        private readonly DBManager dBManager;
        public UnitController(SerializationManager smanager, DBManager dbmanager)
        {
            serialManager = smanager;
            dBManager = dbmanager;
        }

        // GET: api/<ValuesController>
        [HttpGet]
        public Unit[] Get()
        {

            return dBManager.units.ToArray();
        }

        // GET api/<ValuesController>/5
        [HttpGet("{name}")]
        public ActionResult<Unit> Get(string name)
        {
            try
            {
                return dBManager.FindByName(dBManager.units, name);
            }
            catch (InvalidOperationException e)
            {
                return NotFound(e.Message);
            }
            
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] Unit unit)
        {
            dBManager.AddUnit(unit);
            serialManager.Serialize(dBManager.factories.ToArray());
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public Unit Put(int id, [FromBody] Unit unit)
        {
            return dBManager.ChangeUnit(id, unit);
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{name}")]
        public void Delete(string name)
        {
            dBManager.DeleteUnit(name);
            serialManager.Serialize(dBManager.factories.ToArray());
        }
    }
}
