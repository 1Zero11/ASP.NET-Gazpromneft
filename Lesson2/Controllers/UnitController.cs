﻿using DataLib;
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
            return DBManager.FindByName(DBManager.units, name).Name;
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
            DBManager.AddUnit(SerializationManager.Deserialise<Unit>(value)[0]);
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
            DBManager.DeleteUnit(name);
            SerializationManager.Serialize();
        }
    }
}
