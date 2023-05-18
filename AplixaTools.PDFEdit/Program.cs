using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;
using AplixaTools.PDFEdit;
using AplixaTools.Shared.Services;
using AplixaTools.PDFEdit.Services;
using Plk.Blazor.DragDrop;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddTransient(serviceProvider =>
    (IJSInProcessRuntime)serviceProvider.GetRequiredService<IJSRuntime>());
builder.Services.AddTransient<DefaultJsInteropService>(serviceProvider =>
    serviceProvider.GetRequiredService<JsInteropService>());
builder.Services.AddTransient<JsInteropService>();
builder.Services.AddBlazorDragDrop();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
