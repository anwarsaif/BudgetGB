namespace Logix.MVC.Services.YamlService
{
    public interface IYamlService
    {
        //byte[] Write<T>(IList<T> registers);
        Task<byte[]> Write<T>(IList<T> registers);
    }
}
