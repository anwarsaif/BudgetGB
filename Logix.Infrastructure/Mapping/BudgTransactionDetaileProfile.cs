using AutoMapper;
using Logix.Application.DTOs.GB;
using Logix.Domain.GB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Infrastructure.Mapping.GB
{
    public class BudgTransactionDetaileProfile:Profile
    {
        public BudgTransactionDetaileProfile()
        {
            CreateMap<BudgTransactionDetaileDto, BudgTransactionDetaile>().ReverseMap();
            CreateMap<BudgTransactionDetaileEditDto, BudgTransactionDetaile>().ReverseMap();
            CreateMap<BudgTransactionDetaileYearDto, BudgTransactionDetaile>().ReverseMap();
            CreateMap<BudgTransactionDetaileLinksInitialDto, BudgTransactionDetaile>().ReverseMap();
            CreateMap<BudgTransactionDetaileLinksInitialEditDto, BudgTransactionDetaile>().ReverseMap();
            CreateMap<BudgTransactionDetaileDiscountsDto, BudgTransactionDetaile>().ReverseMap();
            CreateMap<BudgTransactionDetaileDiscountsEditDto, BudgTransactionDetaile>().ReverseMap();
            CreateMap<BudgTransactionDetaileLinksMultipleDto, BudgTransactionDetaile>().ReverseMap();
            CreateMap<BudgTransactionDetaileLinksMultipleDto, BudgTransactionDetaileDto>().ReverseMap();
            CreateMap<BudgTransactionDetaileLinksDto, BudgTransactionDetaile>().ReverseMap();
            CreateMap<BudgTransactionDetaileLinksEditDto, BudgTransactionDetaile>().ReverseMap();
            CreateMap<BudgTransactionDetailelinkFinalMultiDto, BudgTransactionDetaileDto>().ReverseMap();
            CreateMap<BudgTransactionDetailelinkFinalMultiDto, BudgTransactionDetaile>().ReverseMap();


        }
    }
    
}
