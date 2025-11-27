using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Server.Core.Interfaces.IServices;

namespace Server.Core.Services
{
    public class ScopingService : IScopingService
    {
        private readonly Kernel _kernel;
        private readonly IChatCompletionService _chatService;
        private readonly ChatHistory _history;
        private readonly ILogger<ScopingService> _logger;

        public ScopingService(Kernel kernel, ILogger<ScopingService> logger)
        {
            _kernel = kernel;
            _chatService = _kernel.GetRequiredService<IChatCompletionService>();
            _history = new ChatHistory();
            _logger = logger;
        }

        public async Task<string> Scope(string userInput)
        {
            _logger.LogInformation("User input: {Input}", userInput);
            _history.AddUserMessage(userInput);

            var result = await _chatService.GetChatMessageContentAsync(
                _history,
                new OpenAIPromptExecutionSettings
                {
                    FunctionChoiceBehavior = FunctionChoiceBehavior.Auto()
                },
                _kernel);

            _history.AddAssistantMessage(result.Content!);
            return result.Content ?? string.Empty;
        }
    }
}
