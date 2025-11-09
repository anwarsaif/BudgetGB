using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logix.Application.Validators;

namespace Logix.Application.DTOs.GB
{
    
    public partial class BudgTransactionVwDto
    {
 
        public long Id { get; set; }
   
        public string? Code { get; set; }

        public string? DateHijri { get; set; }

        public string? DateGregorian { get; set; }

        public string? TTime { get; set; }

        public string? Description { get; set; }

        public long? PeriodId { get; set; }

        public long? FinYear { get; set; }
       
        public long? FacilityId { get; set; }
       
        public long? CcId { get; set; }
     
        public int? DocTypeId { get; set; }
       
        public int? InsertUserId { get; set; }
       
        public int? UpdateUserId { get; set; }
       
        public int? DeleteUserId { get; set; }
       
        public DateTime? InsertDate { get; set; }
     
        public DateTime? UpdateDate { get; set; }
       
        public DateTime? DeleteDate { get; set; }
        public bool? FlagDelete { get; set; }
  
        public int? StatusId { get; set; }
    
        public int? PaymentTypeId { get; set; }
       
        public string? ChequNo { get; set; }
   
        public string? ChequDateHijri { get; set; }
  
        public long? BankId { get; set; }
    
        public string? Bian { get; set; }
      
        public string? DocTypeName { get; set; }
     
        public string? DocTypeName2 { get; set; }
      
        public string? UserFullname { get; set; }
     
        public long? ReferenceNo { get; set; }
    
        public string? StatusName { get; set; }
      
        public int FinYearGregorian { get; set; }
     
        public decimal? Amount { get; set; }

        public string? AmountWrite { get; set; }
     
        public string? BankName { get; set; }
     
        public string? PaymentTypeName { get; set; }
       
        public string? ReferenceCode { get; set; }

        public long? CollectionEmpId { get; set; }
     
        public string? CollectionEmpCode { get; set; }

        public string? CollectionEmpName { get; set; }

        public long? BranchId { get; set; }

        public decimal? ExchangeRate { get; set; }

        public int? CurrencyId { get; set; }

        public int? ReferenceType { get; set; }

        public long? ReferenceId { get; set; }

        public long? ProjectId { get; set; }

        public long? ProjectCode { get; set; }
      
        public string? ProjectName { get; set; }
  
        public long? CustomerId { get; set; }

        public long? CustomerCode { get; set; }
    
        public string? CustomerName { get; set; }

        public int? TypeId { get; set; }
 
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
        public string? Code2 { get; set; }
        public string? AccAccountCode { get; set; }
 
        public string? AccAccountName { get; set; }
    }
}
