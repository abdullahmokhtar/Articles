using Submission.API;
using Submission.API.Endpoints;
using Submission.Application;
using Submission.Persistence;

var builder = WebApplication.CreateBuilder(args);

#region Add Services
builder.Services
    .AddApiServices()
    .AddApplicationServices()
    .AddPersistenceServices(builder.Configuration);
#endregion

var app = builder.Build();

#region Use Services
app.UseSwagger()
    .UseSwaggerUI()
    .UseRouting();

app.MapAllEndpoints();

#endregion
app.Run();
