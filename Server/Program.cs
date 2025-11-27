using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Server.Application.Interfaces;
using Server.Application.Services;
using Server.Config;
using Server.Infrastructure.Data;
using Server.Infrastructure.Repositories;
using Server.Plugins;
using Server.Providers;

var builder = WebApplication.CreateBuilder(args);

// Register DbContext
builder.Services.AddScoped<ICaseRepository, CaseRepository>();
builder.Services.AddDbContext<ServerDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSqlConnection")));

// Services
builder.Services.AddScoped<ICaseService, CaseService>();



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Controllers
builder.Services.AddControllers();

// ✅ Repositories
builder.Services.AddScoped<ICaseRepository, CaseRepository>();


// ✅ Semantic Kernel
builder.Services.AddOptions<AzureOpenAISettings>()
    .Bind(builder.Configuration.GetSection(AzureOpenAISettings.SectionName))
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services.AddSingleton<IChatCompletionService>(sp =>
{
    var options = sp.GetRequiredService<IOptions<AzureOpenAISettings>>().Value;
    return new OpenAIChatCompletionService(options.ChatModelId, options.ApiKey);
});

// ✅ Plugin 등록
builder.Services.AddSingleton<ScopingPlugin>();

// ✅ Kernel 생성자 등록
builder.Services.AddTransient<Kernel>(sp => KernelProvider.CreateKernel(sp));

// Middleware 설정
var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
    c.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseCors("AllowAll");
app.Run();
