using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Application.DTOs.GB
{
    public class GbStatisticsDto
    {
        public int DocTypeId { get; set; }
        public string DocTypeName { get; set; }
        public string DocTypeName2 { get; set; }
        public int CreditSum { get; set; }

        public string Icon { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
    }
}
