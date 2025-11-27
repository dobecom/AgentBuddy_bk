using Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Services
{
    public interface ILogParserService
    {
        Task<IEnumerable<LogFile>> ParseLogsAsync(IEnumerable<string> filePaths, CancellationToken cancellationToken);
    }
}
