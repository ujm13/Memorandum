using Mapster;
using MapsterMapper;
using Memorandum.Common.Options;
using Memorandum.Repository.infrastructure.MapperRegisters;
using Memorandum.WebApplication.infrastructure.MapperRegisters;
using Memorandum.WebApplication.infrastructure.ServiceCollectionExtensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOptionsDependency()
                .AddDependencyInjection();

#region Mapster
var config= new TypeAdapterConfig();
config.Scan(typeof(WebApplicationMapperRegister).Assembly);
config.Scan(typeof(ServiceMapperRegister).Assembly);
builder.Services.AddSingleton(config);
builder.Services.AddScoped<IMapper, ServiceMapper>();
#endregion

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
