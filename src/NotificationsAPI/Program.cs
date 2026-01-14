
using MassTransit;
using Microsoft.OpenApi.Models;
using NotificationsAPI.Consumers;

var builder = WebApplication.CreateBuilder(args);

var rabbitHost = builder.Configuration["RabbitMQ:Host"] ?? "rabbitmq";
var rabbitUser = builder.Configuration["RabbitMQ:User"] ?? "guest";
var rabbitPass = builder.Configuration["RabbitMQ:Pass"] ?? "guest";

builder.Services.AddMassTransit(x => {
    x.AddConsumer<UserCreatedConsumer>();
    x.AddConsumer<PaymentProcessedConsumer>();
    x.UsingRabbitMq((context, cfg) => {
        cfg.Host(rabbitHost, "/", h => { h.Username(rabbitUser); h.Password(rabbitPass); });
        cfg.ReceiveEndpoint("notifications-user-created", e => {
            e.ConfigureConsumer<UserCreatedConsumer>(context);
        });
        cfg.ReceiveEndpoint("notifications-payment-processed", e => {
            e.ConfigureConsumer<PaymentProcessedConsumer>(context);
        });
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "NotificationsAPI", Version = "v1" });
});

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.Run();
