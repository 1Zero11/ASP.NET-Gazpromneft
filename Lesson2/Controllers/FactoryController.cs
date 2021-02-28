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

        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            string[] output = TextManager.Information();

            return output;
        }

        // GET api/<ValuesController>/5
        [HttpGet("{name}")]
        public string Get(string name)
        {
            return DBManager.FindByName(DBManager.factories, name).Description;
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
            DBManager.AddFactory(SerializationManager.Deserialise<Factory>(value)[0]);
            SerializationManager.Serialize();
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{name}")]
        public void Delete(string name)
        {
            DBManager.DeleteFactory(name);
            SerializationManager.Serialize();
        }
    }
}
