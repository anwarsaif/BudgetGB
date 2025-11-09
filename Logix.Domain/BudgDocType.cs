using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Domain.GB
{
    [Table("Budg_Doc_Type")]
    public partial class BudgDocType
    {
        [Key]
        [Column("Doc_Type_ID")]
        public int DocTypeId { get; set; }
        [Column("Doc_Type_Name")]
        [StringLength(50)]
        public string? DocTypeName { get; set; }
        [Column("Doc_Type_Code")]
        [StringLength(50)]
        public string? DocTypeCode { get; set; }
        [Column("Doc_Type_Name2")]
        [StringLength(50)]
        public string? DocTypeName2 { get; set; }
        [Column("Insert_User_ID")]
        public int? InsertUserId { get; set; }
        [Column("Update_User_ID")]
        public int? UpdateUserId { get; set; }
        [Column("Delete_User_ID")]
        public int? DeleteUserId { get; set; }
        [Column("Insert_Date", TypeName = "datetime")]
        public DateTime? InsertDate { get; set; }
        [Column("Update_Date", TypeName = "datetime")]
        public DateTime? UpdateDate { get; set; }
        [Column("Delete_Date", TypeName = "datetime")]
        public DateTime? DeleteDate { get; set; }
        public bool? FlagDelete { get; set; }
        [Column("Sort_No")]
        public int? SortNo { get; set; }
        /// <summary>
        /// هل تقديري ام فعلي
        /// </summary>
        [Column("Type_ID")]
        public int? TypeId { get; set; }
    }
}

