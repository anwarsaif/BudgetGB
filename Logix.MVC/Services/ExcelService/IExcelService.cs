using System.Reflection;

namespace Logix.MVC.Services.ExcelService
{
    public interface IExcelService
    {
        Task<byte[]> Write<T>(IList<T> registers);
    }
}
