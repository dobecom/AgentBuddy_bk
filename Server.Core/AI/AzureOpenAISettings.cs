namespace Server.Core.AI
{
    public class AzureOpenAISettings
    {
        public const string SectionName = "OpenAI";
        public string ChatDeploymentName { get; set; }
        public string ChatModelId { get; set; }
        public string Endpoint { get; set; }
        public string ApiKey { get; set; }
    }
}
