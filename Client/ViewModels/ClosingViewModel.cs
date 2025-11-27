using Client.Helpers;
using Client.Models;
using Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Client.ViewModels
{
    public class ClosingViewModel
    {
        private readonly IGraphEmailService _emailService;
        private readonly IOneNoteService _oneNoteService;
        private readonly CancellationTokenSource _cts = new();

        public Case? CurrentCase { get; set; }
        public string CaseSummary { get; set; } = string.Empty;
        public string ClosingEmailDraft { get; set; } = string.Empty;

        public ICommand SendClosingEmailCommand { get; }
        public ICommand SyncOneNoteCommand { get; }

        public ClosingViewModel(IGraphEmailService emailService, IOneNoteService oneNoteService)
        {
            _emailService = emailService;
            _oneNoteService = oneNoteService;
            SendClosingEmailCommand = new AsyncRelayCommand(SendClosingEmailAsync, () => !string.IsNullOrWhiteSpace(ClosingEmailDraft));
            SyncOneNoteCommand = new AsyncRelayCommand(SyncOneNoteAsync, () => CurrentCase is not null);
        }

        private async Task SendClosingEmailAsync()
        {
            if (CurrentCase is null) return;
            await _emailService.SendEmailAsync(
                to: "", // Set recipient
                subject: "Case Resolved",
                body: ClosingEmailDraft,
                attachments: [],
                cancellationToken: _cts.Token
            );
        }

        private async Task SyncOneNoteAsync()
        {
            if (CurrentCase is null) return;
            await _oneNoteService.ArchiveCaseAsync(CurrentCase, _cts.Token);
        }
    }
}
