using AutoMapper;
using BLL.Profiles;
using BLL.Services;
using Core.Models;
using DAL.Abstractions.Interfaces;
using DataAccess;
using DataAccess.Contexts;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MovieTheaterApi.Hubs;
using MovieTheaterApi.JWT;
using NETCore.MailKit.Extensions;
using NETCore.MailKit.Infrastructure.Internal;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var configuration = new ConfigurationBuilder().
    SetBasePath(Directory.GetCurrentDirectory()).
    AddJsonFile("appsettings.json", false).
    Build();

builder.Services.AddCors();
builder.Services.AddSignalR();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    //options.SignIn.RequireConfirmedEmail = true;
})
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<JwtHandler>();
builder.Services.AddSingleton<IConfigurationRoot>(configuration);
builder.Services.AddTransient<IMovieRepository, MovieRepository>();
builder.Services.AddTransient<IGenericRepository<User>, GenericRepository<User>>();
builder.Services.AddTransient<ICommentRepository, CommentRepository>();
builder.Services.AddTransient<IMovieGenreRepository, MovieGenreRepository>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<MovieService>();
builder.Services.AddTransient<UserService>();
builder.Services.AddTransient<CommentService>();
builder.Services.AddTransient<WatchLaterListService>();
builder.Services.AddTransient<FavoriteListService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).
    AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = configuration.GetSection("Jwt")["Issuer"],
            ValidAudience = configuration.GetSection("Jwt")["Audience"],
            IssuerSigningKey = 
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("Jwt")["Key"]))
        };
    });

// AutoMapper configuration
builder.Services.AddSingleton((IMapper)new Mapper(new MapperConfiguration(c =>
{
    c.AddProfile(new UserMapperProfile());
    c.AddProfile(new CommentMapperProfile());
    c.AddProfile(new MovieMapperProfile());
})));

// MailKit configuration
builder.Services.AddMailKit(config =>
{
    var mailKitOptions = configuration.GetSection("Email").Get<MailKitOptions>();
    config.UseMailKit(mailKitOptions);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();
app.UseCors(options =>
{
    options.WithOrigins("http://localhost:4200")
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials();
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<CommentHub>("/hub");

app.Run();
