using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using CleanAspire.ClientApp;
using FindRazorSourceFile.WebAssembly;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.UseFindRazorSourceFile();

#if STANDALONE
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
#endif

// register the cookie handler
builder.Services.AddCoreServices(builder.Configuration);
builder.Services.AddHttpClients(builder.Configuration);
builder.Services.AddAuthenticationAndLocalization(builder.Configuration);
var app = builder.Build();

await app.InitializeCultureAsync();
await app.RunAsync();
