using System.Reflection;
using FluentMigrator.Runner;
using WebApplication9.Domain;
using WebApplication9.Repositories;
using WebApplication9.Repositories.Interfaces;
using WebApplication9.Services;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Database");

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ConnectionDatabase>(_ => new ConnectionDatabase(connectionString));

builder.Services.AddTransient<IStoreRepository, StoreRepository>();
builder.Services.AddTransient<StoreService>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();

builder.Services.AddFluentMigratorCore().ConfigureRunner(rb => rb.AddPostgres().WithGlobalConnectionString(connectionString).ScanIn(Assembly.GetExecutingAssembly()).For.Migrations()).AddLogging(rb => rb.AddFluentMigratorConsole()).BuildServiceProvider(false);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
var service = app.Services.CreateScope().ServiceProvider;
var runner = service.GetRequiredService<IMigrationRunner>();
runner.MigrateUp();

app.MapControllers();

app.Run();