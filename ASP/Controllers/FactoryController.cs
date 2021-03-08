using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLib;
using DataLib.Models;
using ASP.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Lesson2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FactoryController : ControllerBase
    {
        private readonly SerializationManager serialManager;
        private readonly DBManager dBManager;
        private readonly SQLFactoryRepository repository;

        public FactoryController(SerializationManager smanager, DBManager dbmanager)
        {
            serialManager = smanager;
            dBManager = dbmanager;
            repository = new SQLFactoryRepository();
        }

        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<Factory> Get()
        {
            //TextManager manager = new TextManager(dBManager);
            //string[] output = manager.Information();

            

                //return dBManager.factories.ToArray();
            return repository.GetItemList();

        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public ActionResult<Factory> Get(int id)
        {
            try
            {
                //return dBManager.FindById(dBManager.factories, id);
                return repository.GetBook(id);

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
            /*
            dBManager.AddFactory(factory);
            serialManager.Serialize(dBManager.factories.ToArray());
            */
            repository.Create(factory);
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public Factory Put(int id, [FromBody] Factory factory)
        {
            //return dBManager.ChangeFactory(id, factory);
            repository.Update(factory);
            return repository.GetBook(id);
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            /*
            dBManager.DeleteFactory(name);
            serialManager.Serialize(dBManager.factories.ToArray());
            */
            repository.Delete(id);
        }
    }
}
