using Friday.Abstractions;
using Friday.Api.Samples;
using Friday.Core;
using Microsoft.AspNetCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Register Friday and scan current assembly and referenced Friday project
builder.Services.AddFriday(typeof(Program).Assembly);

builder.Services.AddScoped<Abc>();

var app = builder.Build();                                                                       

app.MapGet("/ping", async (IFriday friday, Abc ac) =>
{
    var message = ac.Abc();
    var result = await friday.Send(new PingRequest { Message = "    " });
    return Results.Ok(result);
});

app.UseExceptionHandler(appBuilder =>
{
    appBuilder.Run(async context =>
    {
        var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
        var exception = exceptionHandlerFeature?.Error;
        var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();

        var traceId = context.TraceIdentifier; // For correlation

        context.Response.ContentType = "application/json";

        var problem = new
        {
            traceId,
            status = StatusCodes.Status500InternalServerError,
            error = "InternalServerError",
            data = "",
            message = "An unexpected error occurred. Please try again later."
        };

        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await context.Response.WriteAsJsonAsync(problem);
    });
});
app.Run();
