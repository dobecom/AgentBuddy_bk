using Client.Services;
using Client.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Windows;

namespace Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IServiceProvider Services { get; private set; } = null!;

        protected override void OnStartup(StartupEventArgs e)
        {
            var services = new ServiceCollection();

            // Register services
            // Remove AddHttpClient if Microsoft.Extensions.Http is not referenced or not needed
            services.AddSingleton<IOpenAIService, OpenAIService>();
            services.AddSingleton<IGraphEmailService, GraphEmailService>();
            services.AddSingleton<ILogParserService, LogParserService>();
            services.AddSingleton<IOneNoteService, OneNoteService>();

            // Register ViewModels
            services.AddTransient<CaseIntakeViewModel>();
            services.AddTransient<ScopingViewModel>();
            services.AddTransient<LogCollectorViewModel>();
            services.AddTransient<AnalysisViewModel>();
            services.AddTransient<ClosingViewModel>();

            Services = services.BuildServiceProvider();

            base.OnStartup(e);
        }
    }

}
