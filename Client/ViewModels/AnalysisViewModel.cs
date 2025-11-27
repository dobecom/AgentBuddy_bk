using Client.Helpers;
using Client.Models;
using Client.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Client.ViewModels
{
    public class AnalysisViewModel
    {
        private readonly IOpenAIService _openAIService;
        private readonly CancellationTokenSource _cts = new();

        public Case? CurrentCase { get; set; }
        public ObservableCollection<LogFile> LogFiles { get; } = new();
        public string AnalysisResult { get; private set; } = string.Empty;

        public ICommand RunAnalysisCommand { get; }
        public ICommand DraftResponseCommand { get; }

        public AnalysisViewModel(IOpenAIService openAIService)
        {
            _openAIService = openAIService;
            RunAnalysisCommand = new AsyncRelayCommand(RunAnalysisAsync, () => CurrentCase is not null && LogFiles.Count > 0);
            DraftResponseCommand = new RelayCommand(DraftResponse, () => !string.IsNullOrEmpty(AnalysisResult));
        }

        private async Task RunAnalysisAsync()
        {
            if (CurrentCase is null) return;
            AnalysisResult = await _openAIService.AnalyzeCaseAsync(CurrentCase, LogFiles, _cts.Token);
            // Notify property changed if implementing INotifyPropertyChanged
        }

        private void DraftResponse()
        {
            // Draft customer response logic
        }
    }
}
