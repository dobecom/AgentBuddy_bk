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
    public class CaseIntakeViewModel
    {
        private readonly IGraphEmailService _emailService;
        private readonly CancellationTokenSource _cts = new();

        public ObservableCollection<Case> NewCases { get; } = new();
        public Case? SelectedCase { get; set; }

        public ICommand RefreshCasesCommand { get; }
        public ICommand OpenCaseCommand { get; }

        public CaseIntakeViewModel(IGraphEmailService emailService)
        {
            _emailService = emailService;
            RefreshCasesCommand = new AsyncRelayCommand(RefreshCasesAsync);
            OpenCaseCommand = new RelayCommand(OpenCase, () => SelectedCase is not null);
        }

        private async Task RefreshCasesAsync()
        {
            var cases = await _emailService.GetNewCasesFromEmailAsync(_cts.Token);
            NewCases.Clear();
            foreach (var c in cases)
                NewCases.Add(c);
        }

        private void OpenCase()
        {
            // Open in browser or internal preview
        }
    }
}
