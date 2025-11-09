using AutoMapper;
using Logix.Application.DTOs.GB;
using Logix.Domain.GB;

namespace Logix.Infrastructure.Mapping.GB
{
    public class BudgAccountExpensesProfile : Profile
    {
        public BudgAccountExpensesProfile()
        {
            CreateMap<BudgAccountExpensesDto, BudgAccountExpenses>().ReverseMap();
            CreateMap<BudgAccountExpensesEditDto, BudgAccountExpenses>().ReverseMap();



        }
    }

}
