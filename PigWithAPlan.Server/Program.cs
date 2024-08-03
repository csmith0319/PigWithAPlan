using System.Text;
using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PigWithAPlan.Server.Data;
using PigWithAPlan.Server.Interfaces;
using PigWithAPlan.Server.Repositories;
using PigWithAPlan.Server.Services;

var builder = WebApplication.CreateBuilder(args);

Env.Load("../.env");

builder.Configuration.AddEnvironmentVariables();

var jwtKey = builder.Configuration["JWT_KEY"] ?? throw new ArgumentNullException("JWT_KEY", "JWT Key must be provided in the configuration.");
var issuer = builder.Configuration["Jwt:Issuer"] ?? throw new ArgumentNullException("Jwt:Issuer", "JWT Issuer must be provided in the configuration.");
var audience = builder.Configuration["Jwt:Audience"] ?? throw new ArgumentNullException("Jwt:Audience", "JWT Audience must be provided in the configuration.");

var key = Encoding.ASCII.GetBytes(jwtKey);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true,
        ValidIssuer = issuer,
        ValidAudience = audience,
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
})
.AddGoogle(options =>
{
    options.ClientId = builder.Configuration["GOOGLE_CLIENT_ID"]!;
    options.ClientSecret = builder.Configuration["GOOGLE_SECRET"]!;
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration["CONNECTION_STRING"]));

builder.Services.AddScoped<IBudgetService, BudgetService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICategoryGroupService, CategoryGroupService>();
builder.Services.AddScoped<ICategoryGroupRepository, CategoryGroupRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors(options =>
{
    options.SetIsOriginAllowed(origin => true);
    options.AllowAnyHeader();
    options.AllowAnyMethod();
    options.AllowCredentials();
});

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
