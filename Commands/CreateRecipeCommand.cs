using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using EfCoreSample.Models;

namespace EfCoreSample.Commands
{
    public class RecipeCommandBase
    {
        [Required, StringLength(100)]
        public required string Name { get; set; }

        [Range(0, 23), DisplayName("Time to cook (hrs)")]
        public int TimeToCookHrs { get; set; }

        [Range(0,59), DisplayName("Time to cook (mins)")]
        public int TimeToCookMins { get; set; }

        [Required]
        public required string Method { get; set; }

        [DisplayName("Vegetarian?")]
        public bool IsVegetarian { get; set; }


    }
    public class CreateRecipeCommand : RecipeCommandBase
    {
        public List<CreateIngredientCommand> Ingredients { get; set; } = new();
        public Recipe ToRecipe()
        {
            return new Recipe
            {
                Name = Name,
                TimeToCook = new TimeSpan(TimeToCookHrs, TimeToCookMins,0),
                Method = Method,
                IsVegetarian = IsVegetarian,
                Ingredients = Ingredients.Select(c => c.ToIngredient()).ToList()
            };
        }
    }

    public class CreateIngredientCommand
    {
        [Required, StringLength((100))]
        public required string Name { get; set; }

        [Range(0, 1000)]
        public decimal Quantity { get; set; }

        [Required, StringLength(50)]
        public required string Unit { get; set; }

        public Ingredient ToIngredient()
        {
            return new Ingredient
            {
                Name = Name,
                Quantity = Quantity,
                Unit = Unit
            };
        }
    }
}