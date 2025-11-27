using Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Services
{
    public interface IOneNoteService
    {
        Task ArchiveCaseAsync(Case caseData, CancellationToken cancellationToken);
    }
}
