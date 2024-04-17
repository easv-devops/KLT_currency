using FeatureHubSDK;
using infrastructure;
using infrastructure.repositories;
using Npgsql;
using service.services;

var builder = WebApplication.CreateBuilder(args);

// FeatureLogging.DebugLogger +=(sender,s)=>Console.WriteLine("DEBUG: "+s);
// FeatureLogging.TraceLogger +=(sender,s)=>Console.WriteLine("TRACE: "+s);
// FeatureLogging.InfoLogger +=(sender,s)=>Console.WriteLine("INFO: "+s);
// FeatureLogging.ErrorLogger +=(sender,s)=>Console.WriteLine("ERROR: "+s);
//
// var config = new EdgeFeatureHubConfig("http://featurehub:8085", "7dd9ab45-d431-4783-9de9-d6449144a854/E32Aas14DL1KBFCGQwMm6EPpddXpjONwZQSHYrmo");
// var fh = await config.NewContext().Build();
//         
// Console.WriteLine(fh["HistoryOn"].IsEnabled);




if (builder.Environment.IsDevelopment())
{
    builder.Services.AddNpgsqlDataSource(Utilities.connectionStringDev,
        dataSourceBuilder => dataSourceBuilder.EnableParameterLogging());
}

if (builder.Environment.IsProduction())
{
    builder.Services.AddNpgsqlDataSource(Utilities.connectionStringProd);
}

builder.Services.AddSingleton<CurrencyService>();
builder.Services.AddSingleton<CurrencyRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var frontEndRelativePath = "./../frontend/www";



// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(options => {
    options.SetIsOriginAllowed(origin => true)
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials();
});

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseHttpsRedirection();

app.Run();