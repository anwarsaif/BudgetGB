using AutoMapper;
using Logix.Application.DTOs.ACC;
using Logix.Application.DTOs.GB;
using Logix.Domain.ACC;
using Logix.Domain.Gb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Infrastructure.Mapping.GB
{
    public class BudgTransactionProfile : Profile
    {
        public BudgTransactionProfile()
        {
            CreateMap<BudgTransactionDto, BudgTransaction>().ReverseMap();
            CreateMap<BudgTransactionEditDto, BudgTransaction>().ReverseMap();
            CreateMap<BudgTransactionEditDto, BudgTransactionDto>().ReverseMap();
            CreateMap<BudgTransactionLinksDto, BudgTransaction>().ReverseMap();
            CreateMap<BudgTransactionLinksEditDto, BudgTransaction>().ReverseMap();
            CreateMap<BudgTransactionLinksInitialDto, BudgTransaction>().ReverseMap();
            CreateMap<BudgTransactionLinksInitialEditDto, BudgTransaction>().ReverseMap();
            CreateMap<BudgTransactionDiscountsDto, BudgTransaction>().ReverseMap();
            CreateMap<BudgTransactionDiscountsEditDto, BudgTransaction>().ReverseMap();
            CreateMap<BudgTransactionLinksInitialmultipleDto, BudgTransaction>().ReverseMap();
            CreateMap<BudgTransactionLinksInitialmultipleEditDto, BudgTransaction>().ReverseMap();
            CreateMap<BudgTransactionlinkFinalMultiDto, BudgTransaction>().ReverseMap();
            CreateMap<BudgTransactionlinkFinalMultiEditDto, BudgTransaction>().ReverseMap();
            CreateMap<BudgTransactionCostsitemsDto, BudgTransaction>().ReverseMap();
            CreateMap<BudgTransactionTransfersDto, BudgTransaction>().ReverseMap();
            CreateMap<BudgTransactionTransfersEditDto, BudgTransaction>().ReverseMap();
            CreateMap<BudgTransactionReinforcementsDto, BudgTransaction>().ReverseMap();
            CreateMap<BudgTransactionReinforcementsEditDto, BudgTransaction>().ReverseMap();
            CreateMap<BudgTransactionInitialCreditsDto, BudgTransaction>().ReverseMap();
            CreateMap<BudgTransactionInitialCreditsEditDto, BudgTransaction>().ReverseMap();
            CreateMap<BudgTransactionInitialYearDto, BudgTransaction>().ReverseMap();
            CreateMap<BudgTransactionInitialCreditsEditDto ,BudgTransactionInitialCreditsDto>().ReverseMap();
            CreateMap<BudgTransactionCostsitemsEditDto, BudgTransaction>().ReverseMap();


        }
    }
}
