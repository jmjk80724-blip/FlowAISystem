using System.Text;
using FlowAISystem.Core.Services;
using FlowAISystem.Core.Services.Interface;
using FlowAISystem.Core.Services.Interfaces;
using FlowAISystem.Data;
using FlowAISystem.WebApp.Components;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.Tokens.Experimental;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);


// ---Database postgre sql ---------
builder.Services.AddDbContext<AppDbContext>(options => 
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

// --Service DI ----
builder.Services.AddScoped<IStudentService,    StudentService>();
builder.Services.AddScoped<ISubjectService,    SubjectService>();
builder.Services.AddScoped<IEnrollmentService, EnrollmentService>();
builder.Services.AddScoped<IScoreService,      ScoreService>();
builder.Services.AddScoped<IPredictionService,  PredictionService>();
builder.Services.AddScoped<IFeedbackService,  FeedbackService>();
builder.Services.AddScoped<IAuthService,  AuthService>();

// ---Security JWT -----
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer( Options =>
{
    Options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer  = true,
        ValidateAudience = true,
        ValidateLifetime =true,
        ValidateIssuerSigningKey =true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])
        )
    };
});
 
builder.Services.AddAuthorization();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddMudServices();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.UseAuthentication();
app.UseAuthorization();

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
