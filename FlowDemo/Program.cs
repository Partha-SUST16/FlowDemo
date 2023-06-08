using System.Reflection;
using Business.Interfaces;
using Business.Services;
using FlowDemo;
using FlowDemo.EventHandlers;
using MediatR;
using Models;
using Models.Event;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
// builder.Services.AddTransient<INotificationHandler<UpdateTimeTagEvent>, UpdateTimeTagEventHandler>();
// builder.Services.AddTransient<INotificationHandler<SetGeoHashEvent>, SetGeoHashEventHandler>();

ServiceLocator.SetLocatorProvider(builder.Services.BuildServiceProvider());
DomainEvents.Mediator = () => ServiceLocator.Current.GetInstance<IMediator>();
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