using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;


var host = new HostBuilder()
    .ConfigureHostConfiguration(hostConfig =>
    {
        hostConfig
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", true, true)
        .AddEnvironmentVariables();                
    })
    .ConfigureServices((context, services) =>
    {
        services.AddHostedService<MyProcessor>();        
    })
    .ConfigureLogging(logging =>
    {
        logging.ClearProviders();
        logging.AddConsole();
    })
    .UseConsoleLifetime()
    .Build();

host.Run();

public class MyProcessor : BackgroundService
{
    private readonly ILogger<MyProcessor> _logger;
    private readonly IConfiguration _configuration;

    public MyProcessor(IServiceProvider services,
        ILogger<MyProcessor> logger,
        IConfiguration configuration)
    {
        Services = services;
        _logger = logger;
        _configuration = configuration;
    }

    public IServiceProvider Services { get; }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation(
            $"Hosted Service running ({_configuration["VariableTest"]}).");
            await Task.Delay(3000);
        }
    }

    public override async Task StopAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation(
            "Consume Scoped Service Hosted Service is stopping.");

        await base.StopAsync(stoppingToken);
    }
}