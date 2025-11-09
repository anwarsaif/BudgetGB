using AutoMapper;
using Logix.Application.DTOs.GB;
using Logix.Domain.GB;

namespace Logix.Infrastructure.Mapping.GB
{
    public class BudgExpensesLinksProfile : Profile
    {
        public BudgExpensesLinksProfile()
        {
            CreateMap<BudgExpensesLinksDto, BudgExpensesLinks>().ReverseMap();
            CreateMap<BudgExpensesLinksEditDto, BudgExpensesLinks>().ReverseMap();
            CreateMap<BudgExpensesLinksVWDto, BudgExpensesLinksVW>().ReverseMap();



        }
    }

}
