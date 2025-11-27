namespace Server.Core.Interfaces.IServices
{
    public interface IScopingService
    {
        Task<string> Scope(string userInput);
    }
}
