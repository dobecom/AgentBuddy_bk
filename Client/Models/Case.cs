using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models
{
    public class Case
    {
        public string CaseNumber { get; set; } = string.Empty;
        public string CustomerStatement { get; set; } = string.Empty;
        public string SupportAreaPath { get; set; } = string.Empty;
        public List<Attachment> Attachments { get; set; } = new();
        // Additional properties can be added here as needed.
    }
}
