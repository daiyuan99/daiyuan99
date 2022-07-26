using Tweetinvi;
using TweetSampleStreamingService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

#region Get credentials for TweetAPI
var configurationManaqger = builder.Configuration;
var credentials = new AccountCredentials();
credentials.APIKey = configurationManaqger["AccountCredentials:APIKey"];
credentials.KeySecret = configurationManaqger["AccountCredentials:KeySecret"];
credentials.BearToken = configurationManaqger["AccountCredentials:BearToken"];

#endregion

#region DI
builder.Services.AddSingleton<ITwitterClient>(new TwitterClient(credentials.APIKey, credentials.KeySecret, credentials.BearToken));
builder.Services.AddSingleton<IServiceLogger, ServiceLogger>();
builder.Services.AddSingleton<ITweetTrendReport, TweetTrendReport>();
builder.Services.AddSingleton<ISampleStreamingService, SampleStreamingService>();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

