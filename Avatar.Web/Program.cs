using Avatar.Web.Extensions;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Flurl.Http;
using Flurl.Http.Configuration;

var builder = WebApplication.CreateBuilder(args);


// register flurl
builder.Services.AddSingleton<IFlurlClientFactory, PerBaseUrlFlurlClientFactory>();
builder.Services.AddControllers();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor().AddCircuitOptions(opts => {
    opts.DetailedErrors = true;
});
builder.Services.AddHttpContextAccessor();

// blazorise
builder.Services.AddBlazorise(options => { options.Immediate = true; })
    .AddBootstrapProviders()
    .AddFontAwesomeIcons();

// add config
Avatar.Web.Config.Config config = new Avatar.Web.Config.Config();
builder.Configuration.GetSection("AppConfig").Bind(config);
builder.Services.AddSingleton(config);

// Register Flurl with the API URL
builder.Services.AddSingleton<IFlurlClient>(_ => new FlurlClient(config.ApiUrl));


//Extensions
builder.Services.AppExtendServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();