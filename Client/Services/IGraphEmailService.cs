using Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Services
{
    public interface IGraphEmailService
    {
        Task<IEnumerable<Case>> GetNewCasesFromEmailAsync(CancellationToken cancellationToken);
        Task SendEmailAsync(string to, string subject, string body, IEnumerable<Attachment> attachments, CancellationToken cancellationToken);
    }
}
