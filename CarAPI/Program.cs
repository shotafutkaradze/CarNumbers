using Application.CommandHandlers;
using Application.Queries;
using Application.QueriesHandlers;
using Doamin.DTOs;
using Doamin.Entities;
using Doamin.Exceptions;
using Doamin.Repositories;
using Doamin.Repositories.Interface;
using MediatR;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Globalization;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);





// Add services to the container.

builder.Services.AddMediatR(typeof(GetCarNumberByIdQueryHandler).GetTypeInfo().Assembly);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var connectionString = builder.Configuration.GetConnectionString("CarConnection") ?? throw new InvalidOperationException();

builder.Services.AddDbContext<CarBDContext>(x =>
{
    x.UseSqlServer(connectionString);
});


builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
//builder.Services.AddTransient<ICarCaseRepository, CarCaseRepository>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

IList<CultureInfo> supportedCultures = new List<CultureInfo>
            {
                new CultureInfo("en-US"),
                new CultureInfo("ka-GE"),
            };

builder.Services.Configure<RequestLocalizationOptions>(options => {
    options.DefaultRequestCulture = new RequestCulture("en-US");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});


var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "CarAPI");
});
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.Run();
app.UseDeveloperExceptionPage();
app.UseCustomExceptionMiddleware();
app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);


//app.UseExceptionHandler(appError => {
//    appError.Run(async context =>
//    {
//        context.Response.ContentType = "application/json";
//        var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

//        /*var reader = new StreamReader(context.Request.Body);
//        reader.BaseStream.Seek(0, SeekOrigin.Begin); 
//        var body = await reader.ReadToEndAsync();*/

//        await context.Response.WriteAsJsonAsync(new
//        {
//            Code = context.Response.StatusCode,
//            Message = contextFeature.Error.Message
//        });

//        await using StreamWriter sw =
//            File.CreateText(
//                @$"c:\Logs\CarApi\{Guid.NewGuid():N}.log");
//        await sw.WriteLineAsync($"Code: {context.Response.StatusCode}");
//        await sw.WriteLineAsync($"Error: {contextFeature.Error}");
//        await sw.WriteLineAsync($"Query: {context.Request.QueryString}");
//        await sw.WriteLineAsync($"Path: {context.Request.Path}");
//        await sw.WriteLineAsync($"QueryString: {context.Request.QueryString}");

//    });
//});



