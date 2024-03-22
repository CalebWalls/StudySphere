using Microsoft.EntityFrameworkCore;
using StudySphere.Contexts;
using StudySphere.Models;
using StudySphere.Services;
using StudySphere.Services.UserServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<UserLoginConfigs>(builder.Configuration.GetSection("UserLogin"));

builder.Services.AddControllers();
builder.Services.AddTransient<ILoginService, LoginService>();
builder.Services.AddTransient<ICreateUserService, CreateUserService>();
builder.Services.AddTransient<IResetPasswordService, ResetPasswordService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<UserContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("MyPolicy",
        builder =>
        {
            builder.WithOrigins("https://studysphereedu.azurewebsites.net")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseCors("MyPolicy"); // Apply the CORS policy

app.UseAuthorization();

app.MapControllers();

app.Run();
