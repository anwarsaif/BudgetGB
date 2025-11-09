using Logix.Application.DTOs.ACC;
using Logix.Application.DTOs.GB;
using Logix.Application.DTOs.RPT;
using Logix.Application.Wrapper;
using Logix.Domain.GB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Application.Interfaces.IServices.GB
{
    public interface IGBDashboardService
    {
        Task<IResult<IEnumerable<GbStatisticsDto>>> GetGBStatistics();
        int CheckPage(long screenId);
        Task<IResult<IEnumerable<RptReportDto>>> GetReports(long systemId, string groupId);

        Task<IResult<IEnumerable<BudgTransactionVw>>> GetAllBudgTransactionVw(CancellationToken cancellationToken = default);
    }
}
