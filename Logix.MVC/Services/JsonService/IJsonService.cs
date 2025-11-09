namespace Logix.MVC.Services.JsonService
{
    public interface IJsonService
    {
        //byte[] Write<T>(IList<T> registers);
        Task<byte[]> Write<T>(IList<T> registers);
    }
}
