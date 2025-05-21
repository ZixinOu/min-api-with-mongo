using MongoDB.Driver;
 
 
var builder = WebApplication.CreateBuilder(args);
 
var movieDatabaseConfigSection = builder.Configuration.GetSection("DatabaseSettings");
builder.Services.Configure<DatabaseSettings>(movieDatabaseConfigSection);
builder.Services.AddSingleton<IMovieService, MongoMovieService>(); 

var app = builder.Build();
 
app.MapGet("/", () => "Minimal API nach Arbeitsauftrag 2");
 
// docker run --name mongodb -d -p 27017:27017 -v data:/data/db -e MONGO_INITDB_ROOT_USERNAME=gbs -e MONGO_INITDB_ROOT_PASSWORD=geheim mongo
app.MapGet("/check", (IMovieService movieService) =>
{
    return movieService.Check();
});
 
app.MapPost("/api/movies", (Movie movie,IMovieService movieService) =>
{
    movieService.Create(movie);
});
 
app.MapGet("api/movies", (IMovieService movieService) =>
{
    return movieService.Get();
});
 
app.MapGet("api/movies/{id}", (string id, IMovieService movieService) =>
{
    movieService.Get(id);
});
 
app.MapPut("/api/movies/{id}", (string id, Movie movie, IMovieService movieService) =>
{
    movieService.Update(id, movie);
});
 
app.MapDelete("api/movies/{id}", (string id, IMovieService movieService) =>
{
    movieService.Remove(id);
});

app.Run();
