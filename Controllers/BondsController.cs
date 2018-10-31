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

        // GET api/bonds/{cusip}
        [HttpGet("{cusip}")]
        public ActionResult<Bond> Get(string cusip)
        {
            var bond = _bondRepository.Load(cusip);

            if (bond == null)
            {
                return bond;
            }

            return Ok(bond);
        }

        // POST api/bonds
        [HttpPost]
        public void Post([FromBody] Bond bond)
        {
        }

        // // PUT api/values/5
        // [HttpPut("{id}")]
        // public void Put(int id, [FromBody] string value)
        // {
        // }

        // // DELETE api/values/5
        // [HttpDelete("{id}")]
        // public void Delete(int id)
        // {
        // }
    }
}