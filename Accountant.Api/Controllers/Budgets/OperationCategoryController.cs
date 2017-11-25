using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accountant.Api.ViewModels.Budgets;
using Accountant.Domain.Budget;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Accountant.Api.Controllers.Budgets {
    [Authorize]
    [Route("api/operations/categories")]
    public class OperationCategoryController : BaseController {
        protected virtual DbSet<OperationCategory> OperationCategories { get => BudgetsContext.OperationCategories; }

        [HttpGet]
        public async Task<IEnumerable<OperationCategoryViewModels.ListItem>> GetAsync()
        {
            return await OperationCategories.OrderBy(c => c.Kind).ThenBy(c => c.Label)
                        .ProjectTo<OperationCategoryViewModels.ListItem>(Mapper.ConfigurationProvider)
                        .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<OperationCategoryViewModels.Details> GetAsync(Guid id){
            var category = await OperationCategories.FindAsync(id);
            return Mapper.Map<OperationCategory, OperationCategoryViewModels.Details>(category);
        }


        // POST api/values
        [HttpPost]
        public void Post([FromBody]OperationCategoryViewModels.New newCategory)
        {
            var categoryToAdd = Mapper.Map<OperationCategoryViewModels.New, OperationCategory>(newCategory);
            categoryToAdd.TenantId = TenantId;            
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(Guid id, [FromBody]OperationCategoryViewModels.Existing updatedCategory)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
        }
    }
}