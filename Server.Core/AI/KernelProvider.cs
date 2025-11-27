using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Server.Core.AI.Plugins;

namespace Server.Core.AI
{
    public static class KernelProvider
    {
        //public static Kernel CreateKernel(IServiceProvider sp)
        //{
        //    var pluginCollection = new KernelPluginCollection();
        //    pluginCollection.AddFromObject(sp.GetRequiredService<ScopingPlugin>());
        //    //pluginCollection.AddFromObject(sp.GetRequiredService<AlarmPlugin>());

        //    var chatService = sp.GetRequiredService<IChatCompletionService>();
        //    return new Kernel(sp, pluginCollection);
        //}

        public static Kernel CreateKernel(IConfiguration configuration)
        {
            var modelId = configuration["AzureOpenAI:ModelId"];
            var endpoint = configuration["AzureOpenAI:Endpoint"];
            var apiKey = configuration["AzureOpenAI:ApiKey"];

            var builder = Kernel.CreateBuilder();
            builder.AddAzureOpenAIChatCompletion(modelId, endpoint, apiKey);
            builder.Services.AddLogging(c => c.AddDebug().SetMinimumLevel(LogLevel.Trace));
            //builder.Plugins.AddFromType<ScopingPlugin>();

            // 예: Plugin 등록, Planner 등도 이 지점에서 수행 가능
            return builder.Build();
        }

        public static void AddKernelServices(this IServiceCollection services, IConfiguration configuration)
        {
            var kernel = CreateKernel(configuration);
            services.AddSingleton(kernel);
        }

    }
}
