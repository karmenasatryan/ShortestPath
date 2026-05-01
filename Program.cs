using UniversityGraphAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// 1. Ավելացնում ենք սերվիսները
builder.Services.AddControllers();

// Ավելացնում ենք ձեր նախագծի հիմնական սերվիսները որպես Singleton
// Սա թույլ է տալիս PathController-ին "ներարկել" (Inject) այս սերվիսները իր մեջ
builder.Services.AddSingleton<GraphService>();
builder.Services.AddSingleton<DijkstraService>();

// 2. Կարգավորում ենք CORS-ը
// Շատ կարևոր է index.html-ից հարցումներ ստանալու համար
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
// Քանի որ աշխատում եք տեղային (local), Swagger-ը միշտ միացված կլինի
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "University Graph API V1");
    c.RoutePrefix = string.Empty; // Swagger-ը կբացվի localhost:5100 հասցեով
});

// ԿԱՐԵՎՈՐ: UseCors-ը պետք է լինի MapControllers-ից առաջ
app.UseCors("AllowAll");

// Եթե օգտագործում եք HTTPS, ապա սա կարող է պետք գալ, 
// բայց տեղային թեստերի համար հաճախ կարելի է բաց թողնել
// app.UseHttpsRedirection();

app.MapControllers();

app.Run();