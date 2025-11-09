using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Application.DTOs.GB
{
    public class BudgTransactionDetailesVwDto
    {
      
        public long Id { get; set; }
     
        public long? TId { get; set; }
  
        public long? AccAccountId { get; set; }
    
        public decimal? Debit { get; set; }
  
        public decimal? Credit { get; set; }
      
        public int? ReferenceTypeId { get; set; }

        public long? ReferenceNo { get; set; }
        
        public string? Description { get; set; }

        
        public string? AccAccountName { get; set; }
    
        public string? AccAccountCode { get; set; }
       
        public long? AccGroupId { get; set; }
      
        public string? AccAccountName2 { get; set; }
      
        public long? CcId { get; set; }
     
        public string? CostCenterCode { get; set; }
     
        public string? CostCenterName { get; set; }
      
        public string? DateGregorian { get; set; }
        public long? Code { get; set; }
  
        public string? Name { get; set; }
  
        public decimal? ExchangeRate { get; set; }
   
        public int? CurrencyId { get; set; }
     
        public string? ReferenceTypeName { get; set; }
        public int? ParentId { get; set; }
        
        public long? Cc2Id { get; set; }
       
        public long? Cc3Id { get; set; }
    
        public string? ReferenceCode { get; set; }
       
        public decimal? Rate { get; set; }

        public int? TypeId { get; set; }


        public long? PeriodId { get; set; }


 
        public long? FacilityId { get; set; }
        public long? Finyear { get; set; }
        public int? DocTypeId { get; set; }
     
        public string? DocTypeName { get; set; }
  
        public string? DocTypeName2 { get; set; }

        public string? MCode { get; set; }

        public string? CustomerCode { get; set; }

        public string? CustomerName { get; set; }

        public string? CustomerName2 { get; set; }


        public int AccAccountType { get; set; }
    }
}
