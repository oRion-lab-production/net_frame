using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Structures.Trace_Log4net;
using System.Text;
using WebAPIs;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = builder.Configuration;
IHostEnvironment environment = builder.Environment;
IServiceCollection services = builder.Services;

/**
 * Configure Services
 * Add services to the container.
 */

// Add services controller
services.AddControllers();
// Add services http context, access http context in razor pages
services.AddHttpContextAccessor();
// Adds cross-origin resource sharing services
services.AddCors(options => options.AddPolicy("AllowOrigin", builder =>
    builder.WithOrigins(configuration.GetValue<string>("AllowedHostsCORS")).AllowAnyMethod().AllowAnyHeader()));
// Add services MVC model controller as services. Dependency Injection register controllers
services.AddMvc().AddControllersAsServices();
// Add services session to stateless http, session and state management
services.AddSession();

// Authentication for JWT
services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(outputs => {
    outputs.TokenValidationParameters = new TokenValidationParameters() {
        ValidIssuer = configuration["Jwt:Issuer"],
        ValidAudience = configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true
    };
});
services.AddAuthorization();

// Config file AppSettings.json bind
ServiceExtension.ConfigureConfigFiles(services, configuration);
// Custom Dependencies
ServiceExtension.ConfigureCustomDependencies(services, configuration);

/*
 * End Configure Services
 */

await using WebApplication app = builder.Build();

/**
 * Configure the HTTP request pipeline. 
 */

app.Services.GetRequiredService<ILoggerFactory>().AddLog4net("log4net.config", "log4net");

if (app.Environment.IsDevelopment()) {
    app.UseDeveloperExceptionPage();
} else {
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseCors("AllowOrigin");
app.UseSession();
app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints => endpoints.MapControllers());

/*
 * End of Configure 
 */

await app.RunAsync();
