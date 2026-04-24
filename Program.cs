using UniversityGraphAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// 1. Ավելացնում ենք սերվիսները
builder.Services.AddControllers();
builder.Services.AddSingleton<GraphService>();
builder.Services.AddSingleton<DijkstraService>();

// 2. Կարգավորում ենք CORS-ը այնպես, որ ոչ մի արգելք չլինի
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 3. Միացնում ենք Swagger-ը միշտ (նույնիսկ եթե Development mode-ում չես)
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "University Graph API V1");
    c.RoutePrefix = string.Empty; // Սա կբացի Swagger-ը հենց localhost:5072 հասցեով
});

// ԿԱՐԵՎՈՐ: UseCors-ը պետք է լինի UseRouting-ից հետո (եթե կա) և MapControllers-ից առաջ
app.UseCors("AllowAll");

app.MapControllers();

app.Run();