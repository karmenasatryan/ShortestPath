using UniversityGraphAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// 1. Ավելացնում ենք սերվիսները
builder.Services.AddControllers();

// Ավելացնում ենք ձեր նախագծի հիմնական սերվիսները որպես Singleton
builder.Services.AddSingleton<GraphService>();
builder.Services.AddSingleton<DijkstraService>();

// 2. Կարգավորում ենք CORS-ը
// Սա թույլ կտա ձեր index.html-ին հարցումներ ուղարկել backend-ին առանց արգելքների
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

// 3. Միացնում ենք Swagger-ը
// Քանի որ դուք ցանկանում եք, որ այն միշտ հասանելի լինի
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "University Graph API V1");
    c.RoutePrefix = string.Empty; // Սա կբացի Swagger-ը հիմնական հասցեով (localhost:5100)
});

// ԿԱՐԵՎՈՐ: UseCors-ը պետք է կանչվի MapControllers-ից առաջ
app.UseCors("AllowAll");

app.MapControllers();

app.Run();