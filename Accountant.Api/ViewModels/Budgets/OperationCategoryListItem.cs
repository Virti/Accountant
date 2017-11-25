using System;
using System.ComponentModel.DataAnnotations;
using Accountant.Domain.Budget;

namespace Accountant.Api.ViewModels.Budgets
{
    public static class OperationCategoryViewModels
    {

        public class ListItem
        {
            public Guid Id { get; set; }
            public string Label { get; set; }
            public OperationKind Kind { get; set; }
        }

        public class Details
        {
            public Guid Id { get; set; }
            public string Label { get; set; }
            public string Description { get; set; }
            public OperationKind Kind { get; set; }
        }


        public class New
        {
            [Required]
            public string Label { get; set; }
            public string Description { get; set; }
            [Required]
            public OperationKind Kind { get; set; }
        }

        public class Existing : New {}
    }
}