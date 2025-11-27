using Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Services
{
    public class GraphEmailService : IGraphEmailService
    {
        public Task<IEnumerable<Case>> GetNewCasesFromEmailAsync(CancellationToken cancellationToken)
        {
            // TODO: Implement actual call to Microsoft Graph API to fetch new cases from Outlook
            var sampleCases = new List<Case>
            {
                new Case
                {
                    CaseNumber = "CASE-001",
                    CustomerStatement = "Sample customer statement.",
                    SupportAreaPath = "Browsers > Edge",
                    Attachments = new List<Attachment>()
                }
            };
            return Task.FromResult<IEnumerable<Case>>(sampleCases);
        }

        public Task SendEmailAsync(string to, string subject, string body, IEnumerable<Attachment> attachments, CancellationToken cancellationToken)
        {
            // TODO: Implement actual call to Microsoft Graph API to send email
            return Task.CompletedTask;
        }
    }
}
