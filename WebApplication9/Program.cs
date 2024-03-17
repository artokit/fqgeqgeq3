using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using FluentMigrator.Runner;
using Microsoft.IdentityModel.Tokens;
using WebApplication9.Common;
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
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<UserService>();
builder.Services.AddTransient<AuthService>();
builder.Services.AddFluentMigratorCore()
    .ConfigureRunner(rb => rb.AddPostgres().WithGlobalConnectionString(connectionString).ScanIn(Assembly.GetExecutingAssembly()).For.Migrations())
    .AddLogging(rb => rb.AddFluentMigratorConsole());

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = AuthOptions.Issuer,
        ValidAudience = AuthOptions.Audience,
        ValidateLifetime = true,
        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
        ValidateIssuerSigningKey = true
    };
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();

var service = app.Services.CreateScope().ServiceProvider;
var runner = service.GetRequiredService<IMigrationRunner>();
runner.MigrateUp();

app.Run();