using System.Reflection;
using weather.BusinessObjects;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();

var assembly = Assembly.GetEntryAssembly();
var it = typeof(IDependencyScoped);
var types = assembly?.GetTypes()
    .Where(x => it.IsAssignableFrom(x))
    .Where(t => t != it).ToList();

//Load Services
foreach (var type in types
    .Where(x => !x.GetTypeInfo().IsInterface &&
                 !x.GetTypeInfo().IsGenericTypeDefinition))
{
    foreach (var asignable in type.GetInterfaces()
                                  .Where(x => it.IsAssignableFrom(x) &&
                                   x != it &&
                                  !x.GetTypeInfo().IsGenericTypeDefinition))
    {

        builder.Services.AddScoped(asignable, type);
    }
}

//Load Interfaces
var assemblies = assembly?.GetReferencedAssemblies().Where(x => x.FullName.Contains("weather"));

foreach (var assemblyName in assemblies ?? Enumerable.Empty<AssemblyName>())
{
    assembly = Assembly.Load(assemblyName);
    var scopedTypes = assembly.GetTypes()
    .Where(x => it.IsAssignableFrom(x))
    .Where(t => t != it).ToList();

    foreach (var type in scopedTypes
    .Where(x => !x.GetTypeInfo().IsInterface &&
                 !x.GetTypeInfo().IsGenericTypeDefinition))
    {
        foreach (var asignable in type.GetInterfaces()
                                      .Where(x => it.IsAssignableFrom(x) &&
                                       x != it &&
                                      !x.GetTypeInfo().IsGenericTypeDefinition))
        {

            builder.Services.AddScoped(asignable, type);
        }
    }
}


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();
