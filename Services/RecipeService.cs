using EfCoreSample.Commands;
using SQLitePCL;
using EfCoreSample.Models;
using Microsoft.EntityFrameworkCore;

namespace EfCoreSample.Services
{
    public class RecipeService
    {
        private readonly AppDbContext _context;

        public RecipeService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<int> CreateRecipe(CreateRecipeCommand cmd)
        {
            var recipe = cmd.ToRecipe();
            _context.Add(recipe);
            await _context.SaveChangesAsync();
            return recipe.RecipeId;
        }

        public async Task<List<RecipeViewModel>> GetRecipes()
        {
            var recipes = await _context.Recipes.Include(c => c.Ingredients)
                .Where(c => !c.IsDeleted)
                .Select(c => new RecipeViewModel(c))
                .ToListAsync();
            return recipes;
        }

        public async Task<RecipeViewModel?> GetRecipe(int id)
        {
            return await _context.Recipes.Include(c => c.Ingredients)
                .Where(c => c.RecipeId == id && !c.IsDeleted)
                .Select(c => new RecipeViewModel(c))
                .SingleOrDefaultAsync();
        }

        public async Task<int> DeleteRecipe(int id)
        {
            var toDelete = await _context.Recipes.SingleOrDefaultAsync(c => c.RecipeId == id);
            toDelete.IsDeleted = true;
            await _context.SaveChangesAsync();
            return toDelete.RecipeId;
            
        }
    }

    public class RecipeDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TimeToCook { get; set; }
        public string Method { get; set; }
        public string Description { get; set; }
        public List<CreateIngredientCommand> Ingredients { get; set; }
        public RecipeDetailViewModel(Recipe recipe)
        {
            //Id = recipe.RecipeId;
            //Name = recipe.Name;
            //TimeToCook = $"Cooking time:  {recipe.TimeToCook.Hours} hours {recipe.TimeToCook.Minutes} minutes";
            //if (recipe.IsVegetarian) Description = "Do not serve to meat eaters";
            //else Description = "For Vegetarians";
            //Ingredients = recipe.Ingredients.Select(c => new CreateIngredientCommand
            //{
            //
            //});
        }
    }

    public class RecipeViewModel
    {
        public int Id { get; set; }
        public  string Name { get; set; }
        public  string TimeToCook { get; set; }
        public int NumberOfIngredients { get; set; }

        public RecipeViewModel(Recipe recipe)
        {
            Id = recipe.RecipeId;
            Name = recipe.Name;
            TimeToCook = $"Cooking time:  {recipe.TimeToCook.Hours} hours {recipe.TimeToCook.Minutes} minutes";
            NumberOfIngredients = recipe.Ingredients.Count;
        }
    }
}
