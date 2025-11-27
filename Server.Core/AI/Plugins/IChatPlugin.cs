using System.Collections.Generic;
using System.Threading.Tasks;

namespace Server.Core.AI.Plugins
{
    public interface IChatPlugin
    {
        string Name { get; }
        Task<string> ExecuteAsync(string input, IDictionary<string, object>? parameters = null);
    }
}