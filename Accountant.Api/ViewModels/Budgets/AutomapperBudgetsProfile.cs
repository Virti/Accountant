using Accountant.Domain.Budget;
using AutoMapper;

namespace Accountant.Api.ViewModels.Budgets {
    public class AutomapperBudgetsProfile : Profile {
        public AutomapperBudgetsProfile()
        {
            CreateMap<OperationCategoryViewModels.ListItem, OperationCategory>();
            CreateMap<OperationCategoryViewModels.Details, OperationCategory>();

            CreateMap<OperationCategory, OperationCategoryViewModels.New>().ReverseMap();
            CreateMap<OperationCategory, OperationCategoryViewModels.Existing>().ReverseMap();
        }
    }
}