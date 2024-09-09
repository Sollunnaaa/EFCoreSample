namespace EfCoreSample.Models
{
    public class Ingredient
    {
        public int IngredientId { get; set; }
        public int RecipeId { get; set; }
        public required string Name { get; set; }
        public decimal Quantity { get; set; }
        public required string Unit { get; set; }
    }

    public class Recipe
    {
        public int RecipeId { get; set; }
        public required string Name { get; set; }
        public TimeSpan TimeToCook { get; set; }
        public bool IsDeleted { get; set; }
        public required string Method { get; set; }
        public bool IsVegetarian { get; set; }
        public required ICollection<Ingredient> Ingredients { get; set; }
    }
}
