using BLL.Services;
using Core.Models;
using DAL.Abstractions.Interfaces;
using DataAccess.Contexts;
using DataAccess.FIleWriters;
using DataAccess.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MovieTheater;
using MovieTheater.Components;

var configuration = new ConfigurationBuilder().
    SetBasePath(Directory.GetCurrentDirectory()).
    AddJsonFile("appsettings.json",false).
    Build();

ServiceCollection services = new ServiceCollection();

services.AddSingleton<IConfigurationRoot>(configuration);
services.AddSingleton<JsonContext<Movie>>();
services.AddSingleton<JsonContext<User>>();
services.AddSingleton<FileWriter<Movie>>();
services.AddTransient<IGenericRepository<Movie>, GenericRepository<Movie>>();
services.AddTransient<IGenericRepository<User>, GenericRepository<User>>();
services.AddTransient<MovieService>();
services.AddTransient<UserService>();
services.AddTransient<StartUp>();

var builder = services.BuildServiceProvider();

builder.GetService<StartUp>().Do();

