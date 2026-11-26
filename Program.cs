// נותנות גישה למחלקות שלנו (SurveyDbContext) ול-UseSqlServer
using DAL;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

// רישום מסד הנתונים ל-IOC (Dependency Injection).
// AddDbContext אומר: כל מי שיבקש SurveyDbContext יקבל אחד מוכן.
builder.Services.AddDbContext<SurveyDbContext>(options =>
    // UseSqlServer אומר שמסד הנתונים הוא SQL Server.
    // GetConnectionString("DefaultConnection") קורא את הכתובת מ-appsettings.json.
    // השם חייב להיות זהה לשם שכתבת שם: DefaultConnection.
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
