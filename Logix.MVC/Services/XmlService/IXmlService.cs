namespace Logix.MVC.Services.XmlService
{
    public interface IXmlService
    {
        //byte[] Write<T>(IList<T> registers);
        Task<byte[]> Write<T>(IList<T> registers);
    }
}
