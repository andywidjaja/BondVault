using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using BondVault.Models;
using Microsoft.AspNetCore.Mvc;

namespace BondVault.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BondsController : ControllerBase
    {
        private readonly IBondRepository _bondRepository;

        public BondsController(IBondRepository bondRepository)
        {
            _bondRepository = bondRepository;
        }

        // GET api/bonds
        [HttpGet]
        public ActionResult<IEnumerable<Bond>> Get()
        {
            var bondList = _bondRepository.LoadAll();

            if (bondList == null)
            {
                return NotFound(bondList);
            }

            return Ok(bondList);
        }

        // GET api/bonds/{id}
        [HttpGet("{id}")]
        public ActionResult<Bond> Get(int id)
        {
            var bond = _bondRepository.Load(id);

            if (bond == null)
            {
                return bond;
            }

            return Ok(bond);
        }

        // POST api/bonds
        [HttpPost]
        public Bond Post([FromBody] Bond bond)
        {
            System.Console.WriteLine("Adding a new bond");
            return _bondRepository.Add(bond);
        }

        // PUT api/bonds/{id}
        [HttpPut("{id}")]
        public Bond Put(int id, [FromBody] Bond bond)
        {
            return _bondRepository.Update(bond.Id, bond);
        }

        // // DELETE api/bonds/{id}
        // [HttpDelete("{id}")]
        // public void Delete(int id)
        // {
        // }
    }
}