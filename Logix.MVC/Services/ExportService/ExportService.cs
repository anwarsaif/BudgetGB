
using Logix.MVC.Services.CsvService;
using Logix.MVC.Services.ExcelService;
using Logix.MVC.Services.HtmlService;
using Logix.MVC.Services.JsonService;
using Logix.MVC.Services.XmlService;
using Logix.MVC.Services.YamlService;
using System.Text;

namespace Logix.MVC.Services.ExportService
{
    public class ExportService : IExportService
    {
        private readonly IExcelService _excelService;
        private readonly ICsvService _csvService;
        private readonly IHtmlService _htmlService;
        private readonly IJsonService _jsonService;
        private readonly IXmlService _xmlService;
        private readonly IYamlService _yamlService;

        public ExportService(IExcelService excelService, ICsvService csvService, IHtmlService htmlService, IJsonService jsonService, IXmlService xmlService, IYamlService yamlService)
        {
            _excelService = excelService;
            _csvService = csvService;
            _htmlService = htmlService;
            _jsonService = jsonService;
            _xmlService = xmlService;
            _yamlService = yamlService;
        }

        public async Task<byte[]> ExportToExcel<T>(IList<T> registers)
        {
            return await _excelService.Write(registers);
        }

        public async Task<byte[]> ExportToCsv<T>(IList<T> registers)
        {
            return await _csvService.Write(registers);
        }

        public async Task<byte[]> ExportToHtml<T>(IList<T> registers)
        {
            return await _htmlService.Write(registers);
        }

        public async Task<byte[]> ExportToJson<T>(IList<T> registers)
        {
            return await _jsonService.Write(registers);
        }

        public async Task<byte[]> ExportToXml<T>(IList<T> registers)
        {
            return await _xmlService.Write(registers);
        }

        public async Task<byte[]> ExportToYaml<T>(IList<T> registers)
        {
            return await _yamlService.Write(registers);
        }
    }
}
