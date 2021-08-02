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
    public class UnitController : ControllerBase
    {

        private readonly SerializationManager serialManager;
        private readonly DBManager dBManager;
        private readonly SQLUnitRepository repository;

        public UnitController(SerializationManager smanager, DBManager dbmanager)
        {
            serialManager = smanager;
            dBManager = dbmanager;
            repository = new SQLUnitRepository();
        }

        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<Unit> Get()
        {

            //return dBManager.units.ToArray();
            return repository.GetItemList();
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public ActionResult<Unit> Get(int id)
        {
            try
            {
                //return dBManager.FindByName(dBManager.units, name);
                return repository.GetItem(id);
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
            /*
            dBManager.AddUnit(unit);
            serialManager.Serialize(dBManager.factories.ToArray());
            */
            repository.Create(unit);
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public Unit Put(int id, [FromBody] Unit unit)
        {
            //return dBManager.ChangeUnit(id, unit);
            repository.Update(unit);
            return repository.GetItem(id);
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            /*
            dBManager.DeleteUnit(name);
            serialManager.Serialize(dBManager.factories.ToArray());
            */
            repository.Delete(id);
        }
    }
}
