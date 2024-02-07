using BarManagement.Api.Models;
using BarManagement.Models;


namespace BarManagement.Web.Services
{
    public class RecipeService : IRecipeService
    {

        private readonly HttpClient httpClient;

        public RecipeService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<HttpResponseMessage>   DeleteRecipe(int id)
        {

            return await httpClient.DeleteAsync($"api/Bar/{id}");

        }


        public async Task<RecipeObject> CreateRecipe(RecipeObject  recipeObject)       
        {

            var postResponse = await httpClient.PostAsJsonAsync<RecipeObject>("api/Bar", recipeObject);
            return await postResponse.Content.ReadFromJsonAsync<RecipeObject>();
        }


        public async Task<IEnumerable<Recipe>> GetRecipes()
		{
			return await httpClient.GetFromJsonAsync<Recipe[]>("api/Bar");
		}



		public async Task<Recipe> GetRecipe(int id)
        {
            return await httpClient.GetFromJsonAsync<Recipe>($"api/Bar/{id}");
        }

        public async Task<RecipeObject> GetRecipeDetail(int id)
        {
            return await httpClient.GetFromJsonAsync<RecipeObject>($"api/Bar/Detail/{id}");
        }


        public async Task<RecipeObject> UpdateRecipe(RecipeObject recipeObject)
        {
            var putResponse = await httpClient.PutAsJsonAsync<RecipeObject>("api/Bar", recipeObject);
            return await putResponse.Content.ReadFromJsonAsync<RecipeObject>();
        }

    }
}
