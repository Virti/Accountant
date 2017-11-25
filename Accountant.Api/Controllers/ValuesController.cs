using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Accountant.DataAccess;
using Accountant.Domain.Budget;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Accountant.Api.Controllers
{

    [Route("api/[controller]")]
    [Authorize]
    public class ValuesController : BaseController
    {
        // GET api/values
        [HttpGet]
        public async Task<IEnumerable<OperationCategory>> GetAsync()
        {
            return await BudgetsContext.OperationCategories.ToListAsync();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
