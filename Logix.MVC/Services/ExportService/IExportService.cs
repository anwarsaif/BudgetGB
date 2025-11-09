
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Logix.MVC.Services.ExportService
{
    public interface IExportService
    {
        Task<byte[]> ExportToExcel<T>(IList<T> registers);

        Task<byte[]> ExportToCsv<T>(IList<T> registers);

        Task<byte[]> ExportToHtml<T>(IList<T> registers);

        Task<byte[]> ExportToJson<T>(IList<T> registers);

        Task<byte[]> ExportToXml<T>(IList<T> registers);

        Task<byte[]> ExportToYaml<T>(IList<T> registers);
    }
}
