using System.Reflection;
using FluentMigrator;
using FluentMigrator.Runner;
using WebApplication9.Domain;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Database");

builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ConnectionDatabase>(_ => new ConnectionDatabase(connectionString));

var app = builder.Build();
builder.Services.AddFluentMigratorCore().ConfigureRunner(rb => rb.AddPostgres().WithGlobalConnectionString(connectionString).ScanIn(Assembly.GetExecutingAssembly()).For.Migrations()).AddLogging(rb => rb.AddFluentMigratorConsole()).BuildServiceProvider(false);

app.UseSwagger();
app.UseSwaggerUI();
var service = app.Services.CreateScope().ServiceProvider;
var runner = service.GetRequiredService<IMigrationRunner>();
runner.MigrateUp();

app.Run();