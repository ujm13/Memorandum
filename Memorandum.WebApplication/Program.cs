using Asp.Versioning;
using Mapster;
using MapsterMapper;
using Memorandum.Common.Options;
using Memorandum.Repository.infrastructure.MapperRegisters;
using Memorandum.WebApplication.infrastructure;
using Memorandum.WebApplication.infrastructure.Extensions;
using Memorandum.WebApplication.infrastructure.MapperRegisters;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = "Memorandum",
            Version = "v1",
        });

    var basePath = AppContext.BaseDirectory;
    var xmlFiles = Directory.EnumerateFiles(basePath, "*.xml", SearchOption.TopDirectoryOnly);//��xml��
    foreach (var xmlFile in xmlFiles)
    {
        c.IncludeXmlComments(xmlFile);//swaggerŪxml��
    }

    c.DocumentFilter<SwaggerEnumDocumentFilter>();
});

builder.Services.AddOptionsDependency()
                .AddDependencyInjection();

builder.Services.AddHealthChecks();

#region Version
builder.Services.AddApiVersioning(option =>
{
    option.ReportApiVersions = true;
    option.AssumeDefaultVersionWhenUnspecified = true; //���ﶵ�N�Ω�b�S�����������p�U���ѽШD
    option.DefaultApiVersion = new ApiVersion(1, 0); //�w�]������
    option.ApiVersionSelector = new CurrentImplementationApiVersionSelector(option);
    }).AddApiExplorer(
    options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    });

#endregion

#region Mapster
var config = new TypeAdapterConfig();
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
app.MapHealthChecks("/health");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
