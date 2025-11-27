using Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Services
{
    public interface IOpenAIService
    {
        Task<string> CategorizeCaseAsync(Case caseData, CancellationToken cancellationToken);
        Task<string> AnalyzeCaseAsync(Case caseData, IEnumerable<LogFile> logs, CancellationToken cancellationToken);
    }
}
