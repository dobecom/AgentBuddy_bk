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
    public class LogCollectorViewModel
    {
        private readonly ILogParserService _logParserService;
        private readonly CancellationTokenSource _cts = new();

        public ObservableCollection<LogFile> LogFiles { get; } = new();
        public bool IsUploading { get; private set; }

        public ICommand AddFilesCommand { get; }
        public ICommand UploadFilesCommand { get; }

        public LogCollectorViewModel(ILogParserService logParserService)
        {
            _logParserService = logParserService;
            AddFilesCommand = new AsyncRelayCommand(AddFilesAsync);
            UploadFilesCommand = new AsyncRelayCommand(UploadFilesAsync, () => LogFiles.Count > 0 && !IsUploading);
        }

        private async Task AddFilesAsync()
        {
            // File dialog or drag-and-drop logic here
            // Example: var files = await _logParserService.ParseLogsAsync(filePaths, _cts.Token);
            // LogFiles.AddRange(files);
        }

        private async Task UploadFilesAsync()
        {
            IsUploading = true;
            // Upload logic here
            IsUploading = false;
        }
    }
}
