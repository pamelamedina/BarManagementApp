using BarManagement.Models;

namespace BarManagement.Api.Models
{
    public interface IBarRepository
    {
        Task<int> DeleteRecipe(int recipeId);
        Task<IEnumerable<Recipe>> GetRecipes();

        Task<Recipe> GetRecipe(int recipeId);

        Task<string> GetRecipeDetail(int recipeId);

        Task<string> CreateRecipe(RecipeObject r);

        Task<RecipeObject> UpdateRecipe(RecipeObject r);

    }
}
