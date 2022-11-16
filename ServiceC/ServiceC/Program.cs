using ServiceC2;

var builder = WebApplication.CreateBuilder(args); 
builder.Services.AddControllers();
builder.Services.AddSingleton<GRPCGetter>();
    var app = builder.Build();

app.MapControllers();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
