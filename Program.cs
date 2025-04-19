using MudBlazor.Services;
using BlazorApp9.Components;
using BlazorApp1.DBConnections;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var mongoConfig = builder.Configuration.GetSection("MongoDB");
string connectionString = mongoConfig["mongodb+srv://sandeep200418:sandeep@cluster0.1okjd.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0"]; // Fixed: Set the key to retrieve the connection string
string databaseName = mongoConfig["dbconnection"];
builder.Services.AddScoped(sp => new Mongo("mongodb+srv://sandeep200418:sandeep@cluster0.1okjd.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0", "dbconnection")); // Use connection string and database name from configuration

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://your-api-base-url/") });

// Add services for Blazor components
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
//.AddInteractiveWebAssemblyComponents();

// Add Razor pages support
builder.Services.AddRazorPages();
builder.Services.AddMudServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts(); // Default HSTS value is 30 days; adjust for production
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// Middleware order is important; UseRouting must be before UseEndpoints
app.UseRouting();
app.UseAntiforgery(); // Moved here to ensure it is used correctly

app.MapRazorComponents<App>()
.AddInteractiveServerRenderMode()
    //.AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(BlazorApp9.Client._Imports).Assembly);

// Map controllers if you have any
app.MapControllers();

// Ensure endpoints are defined
app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages(); // This is necessary for Razor pages
});

app.Run();

