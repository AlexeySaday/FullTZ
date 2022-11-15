using ServiceC2;

var builder = WebApplication.CreateBuilder(args); 
builder.Services.AddControllers();
builder.Services.AddSingleton<GRPCGetter>();
var app = builder.Build();

app.MapControllers(); 
app.Run();
