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
    public class ScopingViewModel
    {
        private readonly IOpenAIService _openAIService;
        private readonly IGraphEmailService _emailService;
        private readonly CancellationTokenSource _cts = new();

        public Case? CurrentCase { get; set; }
        public string SuggestedCaseType { get; private set; } = string.Empty;
        public string FollowUpDraft { get; set; } = string.Empty;

        public ICommand CategorizeCaseCommand { get; }
        public ICommand SendFollowUpCommand { get; }

        public ScopingViewModel(IOpenAIService openAIService, IGraphEmailService emailService)
        {
            _openAIService = openAIService;
            _emailService = emailService;
            CategorizeCaseCommand = new AsyncRelayCommand(CategorizeCaseAsync, () => CurrentCase is not null);
            SendFollowUpCommand = new AsyncRelayCommand(SendFollowUpAsync, () => !string.IsNullOrWhiteSpace(FollowUpDraft));
        }

        private async Task CategorizeCaseAsync()
        {
            if (CurrentCase is null) return;
            SuggestedCaseType = await _openAIService.CategorizeCaseAsync(CurrentCase, _cts.Token);
            // Notify property changed if implementing INotifyPropertyChanged
        }

        private async Task SendFollowUpAsync()
        {
            if (CurrentCase is null) return;
            await _emailService.SendEmailAsync(
                to: "", // Set recipient
                subject: "Follow-up on your case",
                body: FollowUpDraft,
                attachments: [],
                cancellationToken: _cts.Token
            );
        }
    }
}