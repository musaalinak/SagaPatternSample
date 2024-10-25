using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using SagaStateMachine.Service;
using Shared;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args).ConfigureServices((hostContext, services) =>
        {
            services.AddMassTransit(opt =>
            {
                opt.AddSagaStateMachine<LoanStateMachine, LoanStateInstance>().InMemoryRepository();
                opt.UsingRabbitMq((context, config) =>
                {
                    config.Host(hostContext.Configuration.GetConnectionString("RabbitMQLocal"));
                    config.ReceiveEndpoint(Endpoints.LoanStateMachine, e =>
                    {
                        e.ConfigureSaga<LoanStateInstance>(context);
                    });
                });
            });
        });
    }
}