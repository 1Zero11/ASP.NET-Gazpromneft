using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLib;
using DataLib.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Lesson2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FactoryController : ControllerBase
    {
        private readonly SerializationManager serialManager;
        private readonly DBManager dBManager;
        public FactoryController(SerializationManager smanager, DBManager dbmanager)
        {
            serialManager = smanager;
            dBManager = dbmanager;
        }

        // GET: api/<ValuesController>
        [HttpGet]
        public ActionResult<Factory[]> Get()
        {
            //TextManager manager = new TextManager(dBManager);
            //string[] output = manager.Information();

            try
            {
                return dBManager.factories.ToArray();
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        // GET api/<ValuesController>/5
        [HttpGet("{name}")]
        public ActionResult<Factory> Get(string name)
        {
            try
            {
                return dBManager.FindByName(dBManager.factories, name);
            }
            catch (InvalidOperationException e)
            {
                return NotFound(e.Message);
            }
            
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] Factory factory)
        {
            //DBManager.AddFactory(SerializationManager.Deserialise<Factory>(value)[0]);
            dBManager.AddFactory(factory);
            serialManager.Serialize(dBManager.factories.ToArray());
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public Factory Put(int id, [FromBody] Factory factory)
        {
            return dBManager.ChangeFactory(id, factory);
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{name}")]
        public void Delete(string name)
        {
            dBManager.DeleteFactory(name);
            serialManager.Serialize(dBManager.factories.ToArray());
        }
    }
}
