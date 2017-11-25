using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accountant.Domain.Budget;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Accountant.Api.Controllers.Budgets {
    [Authorize]
    [Route("api/operations/categories")]
    public class OperationCategoryController : BaseController {
        protected virtual DbSet<OperationCategory> OperationCategories { get => BudgetsContext.OperationCategories; }

        [HttpGet]
        public async Task<IEnumerable<OperationCategory>> GetAsync()
        {
            return await OperationCategories.OrderBy(c => c.Kind).ThenBy(c => c.Label).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<OperationCategory> GetAsync(Guid id){
            return await OperationCategories.FindAsync(id);
        }


        // POST api/values
        [HttpPost]
        public void Post([FromBody]OperationCategory newCategory)
        {
            
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(Guid id, [FromBody]OperationCategory updatedCategory)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
        }
    }
}