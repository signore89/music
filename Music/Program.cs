using Microsoft.EntityFrameworkCore;
using Music.Data.Repositories;
using Music.Data.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

var connection = builder.Configuration.GetConnectionString("MusicDbConnection");
builder.Services.AddDbContext<MusicDbContext>(options =>
    options.UseNpgsql(connection));

builder.Services.AddScoped<IAlbumRepository, AlbumRepository>();
builder.Services.AddScoped<IArtistRepository, ArtistRepository>();
builder.Services.AddScoped<ISongRepository, SongRepository>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

//app.UseHttpsRedirection();
//app.UseStaticFiles();
app.MapDefaultControllerRoute();

app.Run();
