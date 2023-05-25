using Business.Interfaces;
using Business.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IDbService, DbService>();
builder.Services.AddSingleton<ITimeService, TimeService>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapControllers();
app.Run();