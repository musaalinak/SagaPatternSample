using Customer.API.Consumer;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(opt =>
{
    opt.AddConsumer<CreditApprovedEventConsumer>();
    opt.AddConsumer<CreditNotApprovedEventConsumer>();
    opt.AddConsumer<LoanApprovedEventConsumer>();
    opt.AddConsumer<LoanFaildEventConsumer>();

    opt.UsingRabbitMq((context, config) =>
    {
        config.Host(builder.Configuration.GetConnectionString("RabbitMQLocal"));
        config.ReceiveEndpoint(x => x.ConfigureConsumer<CreditApprovedEventConsumer>(context));
        config.ReceiveEndpoint(x => x.ConfigureConsumer<CreditNotApprovedEventConsumer>(context));
        config.ReceiveEndpoint(x => x.ConfigureConsumer<LoanApprovedEventConsumer>(context));
        config.ReceiveEndpoint(x => x.ConfigureConsumer<LoanFaildEventConsumer>(context));
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
