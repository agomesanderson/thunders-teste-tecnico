using Newtonsoft.Json.Converters;
using Thunders.TechTest.ApiService;
using Thunders.TechTest.ApiService.Configurations;
using Thunders.TechTest.ApiService.Infra.Database;
using Thunders.TechTest.ApiService.Infra.Database.Extensions;
using Thunders.TechTest.ApiService.Infra.Database.Interfaces;
using Thunders.TechTest.OutOfBox.Database;
using Thunders.TechTest.OutOfBox.Queues;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.Converters.Add(new StringEnumConverter());
    });

var features = Features.BindFromConfiguration(builder.Configuration);

// Add services to the container.
builder.Services.AddProblemDetails();

if (features.UseMessageBroker)
{
    builder.Services.AddBus(builder.Configuration, new SubscriptionBuilder());
    builder.Services.AddScoped<IMessageSender, RebusMessageSender>();
}

if (features.UseEntityFramework)
{
    builder.Services.AddSqlServerDbContext<TollDbContext>(builder.Configuration);
    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
}

builder.Services.AddDocs();
builder.Services.AddVersioning();
builder.Services.AddServices();
builder.Services.AddRepositories();

var app = builder.Build();

app.UseDocs();

app.MigrateDatabase<TollDbContext>();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

app.MapDefaultEndpoints();

app.MapControllers();

app.Run();
