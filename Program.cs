using PopularMuseumsAPI.Data;
using PopularMuseumsAPI.Services.ImageService;
using PopularMuseumsAPI.Services.MuseumService;
using PopularMuseumsAPI.Services.NewsService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>();
builder.Services.AddScoped<IMuseumService, MuseumService>();
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<INewsService, NewsService>();

builder.Services.AddCors();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().WithExposedHeaders("*"));

app.UseAuthorization();

app.MapControllers();

app.Run();
