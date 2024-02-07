
using BarManagement.Api.Models;
using BarManagement.Models;

namespace BarManagement.Web.Services
{
    public interface IRecipeService
    {
        Task<HttpResponseMessage> DeleteRecipe(int id);
        Task<IEnumerable<Recipe>> GetRecipes();
        Task<Recipe> GetRecipe(int id);
        Task<RecipeObject> GetRecipeDetail(int id);
        Task<RecipeObject> CreateRecipe(RecipeObject recipeObject);
        Task<RecipeObject> UpdateRecipe(RecipeObject recipeObject);
    }
}
