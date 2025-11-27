using Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Services
{
    public class OneNoteService : IOneNoteService
    {
        public Task ArchiveCaseAsync(Case caseData, CancellationToken cancellationToken)
        {
            // TODO: Implement actual call to Microsoft Graph API to archive case to OneNote
            return Task.CompletedTask;
        }
    }
}
