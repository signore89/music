using Microsoft.EntityFrameworkCore;
using Music.Data.Repositories;
using Music.Data.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

var connection = builder.Configuration.GetConnectionString("MusicDbConnection");
builder.Services.AddDbContext<MusicDbContext>(options =>
    options.UseNpgsql(connection));

builder.Services.AddSingleton<IAlbumRepository, AlbumRepository>();
builder.Services.AddSingleton<IArtistRepository, ArtistRepository>();
builder.Services.AddSingleton<ISongRepository, SongRepository>();
builder.Services.AddControllersWithViews();

var app = builder.Build();



app.Run();
