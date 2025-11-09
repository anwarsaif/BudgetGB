using Castle.MicroKernel.SubSystems.Conversion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Application.DTOs.GB
{
    public class BudgExpensesLinksVWDto
    {
       
        public long Id { get; set; }

   
        public long LinkID { get; set; }
   
        public long FinancialNo { get; set; }
      
        public decimal? Amount { get; set; }
        [StringLength(255)]
        public string? LinkCode { get; set; }
       
        public long? AppCode { get; set; }


       
        public long? FacilityId { get; set; }
     
        public long? Finyear { get; set; }
       
        public int? DocTypeId { get; set; }
        
        public string? DocTypeName { get; set; }
       
        public string? DocTypeName2 { get; set; }
      
        public string? AppDate { get; set; }
        
        public decimal? Debit { get; set; }
        
        public string? CustomerCode { get; set; }
      
        public string? CustomerName { get; set; }
       
        public string? CustomerName2 { get; set; }

        public int AccAccountType { get; set; }
        public decimal? RequestAmount { get; set; }
        public long? BranchId { get; set; }
    
        public string? AccountCode { get; set; }
       
        public string? AccountName { get; set; }
    }
}
