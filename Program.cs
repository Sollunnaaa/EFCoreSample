using EfCoreSample;
using EfCoreSample.Commands;
using EfCoreSample.Models;
using EfCoreSample.Services;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var conn = builder.Configuration
    .GetConnectionString("default");
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite(conn);
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<RecipeService>();
builder.Services.AddProblemDetails();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseExceptionHandler();
app.UseRouting();

app.MapGet("/", () => "HelloWorld");

app.MapPost("/create", async (CreateRecipeCommand cmd, RecipeService service) =>
{

    int id = await service.CreateRecipe(cmd);
    return Results.CreatedAtRoute("view-recipe", new { id });

    //var recipe = await service.CreateRecipe(cmd);

    //if (recipe == 0)
    //{
    //    TypedResults.BadRequest();
    //}
    //else
    //{
    //    TypedResults.Ok(recipe);
    //}
});

app.MapGet("/recipe", async (RecipeService service) => 
{
    return await service.GetRecipes();
 });

app.MapGet("{id}", async (int id,RecipeService service) =>
{
    RecipeViewModel show;
    try
    {
       show =  await service.GetRecipe(id);
    }
    catch
    {
        return TypedResults.BadRequest("No such recipe exist");
    }
    return Results.Ok(show);
}).WithName("view-recipe")
.WithSummary("get-recipe");

app.MapDelete("/{id}", async (int id, RecipeService service) =>
{
    try
    {
        await service.DeleteRecipe(id);
    }
    catch
    {
        return TypedResults.BadRequest("Id Not Found");
    }
    return Results.NoContent();
});

app.Run();
