using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// RabbitMQ connection
builder.Services.AddSingleton<IConnectionFactory>(sp => new ConnectionFactory
{
    HostName = "localhost",
    UserName = "guest",
    Password = "guest"
});

builder.Services.AddSingleton<IConnection>(sp =>
{
    var connectionFactory = sp.GetRequiredService<IConnectionFactory>();
    return connectionFactory.CreateConnection();
});

builder.Services.AddSingleton<IModel>(sp =>
{
    var connection = sp.GetRequiredService<IConnection>();
    return connection.CreateModel();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
