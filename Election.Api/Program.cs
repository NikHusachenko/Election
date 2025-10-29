using Election.Api.EntityFramework;
using Election.Api.EntityFramework.GenericRepository;
using Election.Api.Hubs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

const string ConnectionStringNotFoundError = "Connection string not found";
const string DefaultConnectionSection = "ConnectionStrings:DefaultConnection";
const string AllowClientPolicyName = "elections.react";

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
IServiceCollection services = builder.Services;

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

string connectionString = builder.Configuration.GetSection(DefaultConnectionSection).Value ?? throw new InvalidOperationException(ConnectionStringNotFoundError);
services.AddDbContext<AppDbContext>(options => options
    .ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning))
    .UseSqlServer(connectionString)
    .LogTo(Console.WriteLine)
);
services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

services.AddCors(options =>
{
    options.AddPolicy(AllowClientPolicyName, config => config
        .WithOrigins("http://localhost:3000", "https://localhost:3000")
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials()
    );
});

services.AddSignalR(options => options.EnableDetailedErrors = true);
services.AddMediatR(options => options.RegisterServicesFromAssembly(typeof(Program).Assembly));
services.AddAutoMapper(options => options.AddMaps([typeof(Program).Assembly]));

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseCors(AllowClientPolicyName);
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapHub<ElectionHub>("/api/hubs/elections");

app.Run();