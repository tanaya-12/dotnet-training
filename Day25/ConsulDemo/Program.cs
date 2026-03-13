using Consul;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Connect to Consul
builder.Services.AddSingleton<IConsulClient, ConsulClient>(p =>
    new ConsulClient(consulConfig =>
    {
        consulConfig.Address = new Uri("http://localhost:8500");
    }));

var app = builder.Build();

app.MapControllers();

// Get Consul client
var consulClient = app.Services.GetRequiredService<IConsulClient>();

// Register Order Service
var orderRegistration = new AgentServiceRegistration()
{
    ID = "order-service-1",
    Name = "order-service",
    Address = "localhost",
    Port = 5003
};

await consulClient.Agent.ServiceRegister(orderRegistration);

// Register Customer Service
var customerRegistration = new AgentServiceRegistration()
{
    ID = "customer-service-1",
    Name = "customer-service",
    Address = "localhost",
    Port = 5200
};

await consulClient.Agent.ServiceRegister(customerRegistration);

app.Run();