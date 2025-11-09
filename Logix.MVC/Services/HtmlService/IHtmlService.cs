namespace Logix.MVC.Services.HtmlService
{
    public interface IHtmlService
    {
        //byte[] Write<T>(IList<T> registers);
        Task<byte[]> Write<T>(IList<T> registers);
    }
}
