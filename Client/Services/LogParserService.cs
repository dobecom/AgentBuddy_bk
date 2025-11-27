using Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Services
{
    public class LogParserService : ILogParserService
    {
        public Task<IEnumerable<LogFile>> ParseLogsAsync(IEnumerable<string> filePaths, CancellationToken cancellationToken)
        {
            // TODO: Implement actual log file parsing logic
            var parsedLogs = new List<LogFile>();
            foreach (var path in filePaths)
            {
                parsedLogs.Add(new LogFile
                {
                    FileName = System.IO.Path.GetFileName(path),
                    Timestamp = System.IO.File.GetLastWriteTime(path),
                    Content = "Sample log content."
                });
            }
            return Task.FromResult<IEnumerable<LogFile>>(parsedLogs);
        }
    }
}
