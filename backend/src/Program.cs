using ContactListAPI.Data;
using ContactListAPI.Middlewares;
using ContactListAPI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ContactListDbContext>(
    options => options.UseNpgsql(connectionString));

builder.Services.AddControllers();

builder.Services.AddAuthentication("Identity")
    .AddCookie("Identity", options =>
    {
        options.Cookie.Name = "Identity";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
        options.Events.OnRedirectToLogin = context =>
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return Task.CompletedTask;
        };
        options.Events.OnRedirectToAccessDenied = context =>
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            return Task.CompletedTask;
        };
        options.LoginPath = "/authentication/login";
        options.LogoutPath = "/authentication/logout";
    });
builder.Services.AddAuthorization();

builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<SubcategoryService>();
builder.Services.AddScoped<ContactService>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers().RequireAuthorization();

app.Run();
